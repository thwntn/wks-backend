namespace Wks.Mains;

///
/// Summary:
///     A static class for implementing a lock mechanism to ensure thread safety.
///     It uses a ConcurrentDictionary to store locks for different keys.
///     The Run method is used to execute an action within a lock, ensuring that only one thread can access it at a time.
///
public static class Lock
{
    private const int NONE = 1;

    ///
    /// Summary:
    ///     A ConcurrentDictionary to store locks for different keys.
    ///     It is used to ensure that only one thread can access a specific key at a time.
    ///
    private static readonly ConcurrentDictionary<string, SemaphoreSlim> _state =
        new();

    ///
    /// Summary:
    ///     A static method to execute an action within a lock, ensuring that only one thread can access it at a time.
    ///
    /// Type parameters:
    ///   T:
    ///     The type of the return value of the action.
    ///
    /// Parameters:
    ///   key:
    ///     The key used to identify the lock.
    ///
    ///   action:
    ///     The action to execute within the lock.
    ///
    /// Returns:
    ///     The return value of the action.
    ///
    public static async Task<TFormat> Run<TFormat>(
        string key,
        Func<Task<TFormat>> action
    )
    {
        SemaphoreSlim semaphoreSlim = _state.GetOrAdd(
            key,
            _ => new SemaphoreSlim(NONE, NONE)
        );
        await semaphoreSlim.WaitAsync();

        try
        {
            /// Logic:
            ///     Executes the action within the lock.
            ///     This ensures that only one thread can access the key at a time.
            ///
            return await action();
        }
        finally
        {
            semaphoreSlim.Release();

            /// Logic:
            ///     Removes the lock from the ConcurrentDictionary when the last thread releases it.
            ///     This ensures that only one thread can access the key at a time.
            ///
            if (semaphoreSlim.CurrentCount == 1)
                _state.TryRemove(key, out _);
        }
    }
}
