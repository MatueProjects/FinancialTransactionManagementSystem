using FinancialTransactions.Application.DTOs;

namespace FinancialTransactions.Application.Interfaces;

public interface ITransactionService
{
    Task<IReadOnlyList<TransactionDto>> GetActiveAsync(CancellationToken cancellationToken = default);

    Task<TransactionDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<TransactionDto> CreateAsync(CreateTransactionDto dto, CancellationToken cancellationToken = default);

    Task<TransactionDto?> UpdateAsync(Guid id, UpdateTransactionDto dto, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
