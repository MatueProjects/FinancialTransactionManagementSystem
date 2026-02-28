using FinancialTransactions.Application.DTOs;
using FinancialTransactions.Application.Interfaces;
using FinancialTransactions.Application.Mapping;
using Microsoft.EntityFrameworkCore;

namespace FinancialTransactions.Application.Services;

public sealed class TransactionStatusService : ITransactionStatusService
{
    private readonly IAppDbContext _context;

    public TransactionStatusService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<TransactionStatusDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var statuses = await _context.TransactionStatuses
            .AsNoTracking()
            .OrderBy(status => status.Name)
            .ToListAsync(cancellationToken);

        return statuses.Select(status => status.ToDto()).ToList();
    }
}
