using FinancialTransactions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialTransactions.Infrastructure.Data.Configurations;

public sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");

        builder.HasKey(transaction => transaction.Id);

        builder.Property(transaction => transaction.Amount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(transaction => transaction.TransactionDate)
            .IsRequired();

        builder.Property(transaction => transaction.TransactionType)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(transaction => transaction.Status)
            .WithMany(status => status.Transactions)
            .HasForeignKey(transaction => transaction.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
