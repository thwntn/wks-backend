namespace Wks.Handlers;

public class RemoveExpenseTransactionHandler(
    IRemoveExpenseTransaction _removeExpenseTransaction,
    IRealtimeTransactionPort _realtimeTransactionPort,
    IUnitOfWork _unitOfWork,
    ICreateActionPublisher _createAction
) : IRemoveExpenseTransactionHandler
{
    public async Task<string> ExecuteAsync(Guid profileId, List<Guid> ids)
    {
        List<ExpenseTransaction> expenseTransactions = await _removeExpenseTransaction.Execute(
            profileId,
            ids
        );
        await _unitOfWork.SyncAsync();
        await _realtimeTransactionPort.ExecuteAsync(profileId);

        /// Summary:
        ///     After removing the expense transactions, we publish an action to notify other parts of the system about the deletion
        ///    This approach allows us to decouple the deletion of expense transactions from other parts of the system, making the system more modular and reusable.
        await _createAction.Execute(
            new([
                new(
                    profileId,
                    nameof(ActionNames.EXPENSE_TRANSACTION_DELETE),
                    expenseTransactions.Serialize()
                ),
            ])
        );
        return string.Empty;
    }
}
