using FinancialTransactions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FinancialTransactions.Application.Interfaces;

public interface IAppDbContext
{
    DbSet<Transaction> Transactions { get; }

    DbSet<TransactionStatus> TransactionStatuses { get; }

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
