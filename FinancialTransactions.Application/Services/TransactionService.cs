using FinancialTransactions.Application.Constants;
using FinancialTransactions.Application.DTOs;
using FinancialTransactions.Application.Interfaces;
using FinancialTransactions.Application.Mapping;
using Microsoft.EntityFrameworkCore;

namespace FinancialTransactions.Application.Services;

public sealed class TransactionService : ITransactionService
{
    private readonly IAppDbContext _context;

    public TransactionService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<TransactionDto>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        var transactions = await _context.Transactions
            .AsNoTracking()
            .Include(transaction => transaction.Status)
            .Where(transaction => transaction.StatusId == TransactionStatusIds.Active)
            .OrderByDescending(transaction => transaction.TransactionDate)
            .ToListAsync(cancellationToken);

        return transactions.Select(transaction => transaction.ToDto()).ToList();
    }

    public async Task<TransactionDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var transaction = await _context.Transactions
            .AsNoTracking()
            .Include(transactionEntity => transactionEntity.Status)
            .FirstOrDefaultAsync(transactionEntity => transactionEntity.Id == id, cancellationToken);

        return transaction?.ToDto();
    }

    public async Task<TransactionDto> CreateAsync(CreateTransactionDto dto, CancellationToken cancellationToken = default)
    {
        var transaction = dto.ToEntity();
        transaction.Id = Guid.NewGuid();
        transaction.StatusId = TransactionStatusIds.Active;

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(cancellationToken);

        transaction.Status = await _context.TransactionStatuses
            .AsNoTracking()
            .FirstOrDefaultAsync(status => status.Id == TransactionStatusIds.Active, cancellationToken);

        return transaction.ToDto();
    }

    public async Task<TransactionDto?> UpdateAsync(Guid id, UpdateTransactionDto dto, CancellationToken cancellationToken = default)
    {
        var transaction = await _context.Transactions
            .Include(transactionEntity => transactionEntity.Status)
            .FirstOrDefaultAsync(transactionEntity => transactionEntity.Id == id, cancellationToken);

        if (transaction is null)
        {
            return null;
        }

        dto.ToEntity(transaction);
        await _context.SaveChangesAsync(cancellationToken);

        return transaction.ToDto();
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(transactionEntity => transactionEntity.Id == id, cancellationToken);

        if (transaction is null)
        {
            return false;
        }

        transaction.StatusId = TransactionStatusIds.Inactive;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

}
