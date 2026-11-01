using Hangfire.Client;
using Hangfire.Common;
using Hangfire.Server;

namespace Wks.Mains;

///
/// Summary:
///     Preserves the caller trace in Hangfire job parameters and restores it during execution.
///     Writes job creation and execution events through Logger.Write, including retry executions.
///
/// Logic:
///     Creation stores the trace with the persisted job; execution reads it into a disposable scope.
///     Execution logging records the job identifier, operation name, duration, and exception type.
///     The execution scope is disposed after logging so the previous trace is restored.
///
public class HangfireJobLoggingFilter
    : JobFilterAttribute,
        IClientFilter,
        IServerFilter
{
    ///
    /// Summary:
    ///     Uses the recurring registration ID, such as RedirectDailyTraffic, as the operation.
    ///     One-off jobs fall back to their declaring type so shared method names remain distinct.
    ///
    private static string Operation(PerformContext context)
    {
        string RecurringJobId,
            operation = context.BackgroundJob.Job.Type.Name;
        string recurringJobId = context.GetJobParameter<string>(
            nameof(RecurringJobId)
        );
        if (recurringJobId.Justifiable())
            operation = recurringJobId;

        return operation;
    }

    ///
    /// Summary:
    ///     Captures the caller trace before Hangfire persists a background job.
    ///
    /// Logic:
    ///     Generates an identifier when no ambient trace is available.
    ///     Stores it in creation context items and a persisted job parameter.
    ///
    /// Parameters:
    ///   context:
    ///     The creation context used to attach trace metadata to the new job.
    ///
    public void OnCreating(CreatingContext context)
    {
        /// Summary:
        ///     Reuses the caller trace so HTTP, queue, and job logs remain correlated.
        ///     Jobs created outside a trace scope receive a new version 7 GUID.
        string traceId = Logger.TraceId;
        if (traceId.Justifiable() is false)
            traceId = string.Concat(Guid.CreateVersion7());

        /// Summary:
        ///     Items carry the trace to OnCreated within this creation lifecycle.
        ///     The persisted parameter carries it to a worker, including later retries
        ///     or executions in a separate process that does not share the caller scope.
        context.Items[Logger.TRACE_HEADER] = traceId;
        context.SetJobParameter(Logger.TRACE_HEADER, traceId);
    }

    ///
    /// Summary:
    ///     Logs successful creation of a persisted Hangfire job.
    ///
    /// Logic:
    ///     Returns when Hangfire did not create a background job.
    ///     Restores the creation trace temporarily and writes the Created event with the job identifier.
    ///
    /// Parameters:
    ///   context:
    ///     The creation result containing the persisted job and captured context items.
    ///
    public void OnCreated(CreatedContext context)
    {
        /// Summary:
        ///     Skips the creation log when Hangfire did not persist a job.
        ///     There is no job identifier to report in that case.
        if (context.BackgroundJob.Justifiable() is false)
            return;

        /// Summary:
        ///     Opens the captured trace only for this creation event.
        ///     Disposing the scope restores the caller trace when this method returns.
        using IDisposable scope = Logger.BeginScope(
            (string)context.Items[Logger.TRACE_HEADER]
        );

        /// Summary:
        ///     Records enqueue completion with the service index and persisted job ID.
        ///     Created means the job is stored, not that its business operation has completed.
        Logger.Write(
            new LoggingSchema.Job
            {
                LogIndexName = Runtime.LogIndexName.Env(),
                Source = nameof(LogEventEnum.Hangfire),
                Operation = nameof(LogEventEnum.Enqueue),
                Status = nameof(LogEventEnum.Created),
                JobId = context.BackgroundJob.Id,
            }
        );
    }

    ///
    /// Summary:
    ///     Restores the persisted trace before the Hangfire job method runs.
    ///
    /// Logic:
    ///     Stores the disposable scope and stopwatch in execution context items.
    ///     Writes Started with the job identifier and registered operation name.
    ///
    /// Parameters:
    ///   context:
    ///     The execution context containing job parameters and per-execution items.
    ///
    public void OnPerforming(PerformingContext context)
    {
        /// Summary:
        ///     Restores the trace saved when the job was created.
        ///     Keeps the scope in execution items so it remains active until OnPerformed.
        context.Items[nameof(LogContextEnum.TraceScope)] = Logger.BeginScope(
            context.GetJobParameter<string>(Logger.TRACE_HEADER)
        );

        /// Summary:
        ///     Starts a timer for this execution attempt, including a retry attempt.
        ///     Queue waiting time before execution is not included in DurationMs.
        context.Items[nameof(LogContextEnum.Stopwatch)] = Stopwatch.StartNew();

        /// Summary:
        ///     Records the operation and job ID before business processing starts.
        ///     The same job ID and trace connect this event to its completion or failure.
        Logger.Write(
            new LoggingSchema.Job
            {
                LogIndexName = Runtime.LogIndexName.Env(),
                Source = nameof(LogEventEnum.Hangfire),
                Operation = Operation(context),
                Status = nameof(LogEventEnum.Started),
                JobId = context.BackgroundJob.Id,
            }
        );
    }

    ///
    /// Summary:
    ///     Records the outcome of a Hangfire job execution and closes its trace scope.
    ///
    /// Logic:
    ///     Writes Completed at Information level or Failed at Error level based on the execution exception.
    ///     Includes elapsed milliseconds, the root exception type, and full exception details.
    ///     Disposes the stored scope in finally, restoring the trace even if outcome logging fails.
    ///
    /// Parameters:
    ///   context:
    ///     The completed execution context containing the job, exception, and execution items.
    ///
    public void OnPerformed(PerformedContext context)
    {
        try
        {
            /// Summary:
            ///     Retrieves the timer started for this attempt and assumes success initially.
            ///     Error fields remain unset for successful execution and are omitted
            ///     by the shared serializer because it ignores null values.
            Stopwatch watch = (Stopwatch)
                context.Items[nameof(LogContextEnum.Stopwatch)];
            string status = nameof(LogEventEnum.Completed);
            string level = nameof(LogLevel.Information);
            string errorType = default;
            string exceptionMessage = default;
            string stackTrace = default;

            if (context.Exception.Justifiable())
            {
                /// Summary:
                ///     Extracts the deepest inner exception instead of reporting only
                ///     Hangfire's JobPerformanceException wrapper as the cause.
                ///     Error and ExceptionMessage identify that underlying failure.
                Exception error = context.Exception.GetBaseException();
                status = nameof(LogEventEnum.Failed);
                level = nameof(LogLevel.Error);
                errorType = error.GetType().Name;
                exceptionMessage = error.Message;

                /// Summary:
                ///     Keeps the full exception text, including wrapper and inner exceptions,
                ///     so Grafana shows the call stacks needed to locate the failing code.
                stackTrace = context.Exception.ToString();
            }

            /// Summary:
            ///     Writes one outcome event for this attempt using the active job trace.
            ///     LogIndexName groups it with the same service's HTTP and infrastructure logs.
            ///     Failed describes this attempt; Hangfire may still schedule another retry.
            Logger.Write(
                new LoggingSchema.Job
                {
                    LogIndexName = Runtime.LogIndexName.Env(),
                    Source = nameof(LogEventEnum.Hangfire),
                    Operation = Operation(context),
                    Status = status,
                    JobId = context.BackgroundJob.Id,
                    DurationMs = watch.Elapsed.TotalMilliseconds,
                    Error = errorType,
                    ExceptionMessage = exceptionMessage,
                    StackTrace = stackTrace,
                },
                level
            );
        }
        finally
        {
            /// Summary:
            ///     Always restores the trace that was active before this job attempt.
            ///     Cleanup runs even when constructing the outcome log throws,
            ///     preventing this job's trace from leaking into subsequent scoped work.
            (
                (IDisposable)context.Items[nameof(LogContextEnum.TraceScope)]
            ).Dispose();
        }
    }
}
