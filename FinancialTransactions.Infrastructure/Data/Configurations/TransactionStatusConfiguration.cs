using FinancialTransactions.Application.Constants;
using FinancialTransactions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialTransactions.Infrastructure.Data.Configurations;

public sealed class TransactionStatusConfiguration : IEntityTypeConfiguration<TransactionStatus>
{
    public void Configure(EntityTypeBuilder<TransactionStatus> builder)
    {
        builder.ToTable("TransactionStatuses");

        builder.HasKey(status => status.Id);

        builder.Property(status => status.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasData(
            new TransactionStatus { Id = TransactionStatusIds.Active, Name = "Active" },
            new TransactionStatus { Id = TransactionStatusIds.Inactive, Name = "Inactive" },
            new TransactionStatus { Id = TransactionStatusIds.Pending, Name = "Pending" },
            new TransactionStatus { Id = TransactionStatusIds.Completed, Name = "Completed" },
            new TransactionStatus { Id = TransactionStatusIds.Cancelled, Name = "Cancelled" }
        );
    }
}
