using FinancialTransactions.Application.DTOs;

namespace FinancialTransactions.Application.Interfaces;

public interface ITransactionStatusService
{
    Task<IReadOnlyList<TransactionStatusDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
