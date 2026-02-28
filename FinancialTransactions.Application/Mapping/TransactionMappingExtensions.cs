using FinancialTransactions.Application.DTOs;
using FinancialTransactions.Domain.Entities;

namespace FinancialTransactions.Application.Mapping;

public static class TransactionMappingExtensions
{
    public static TransactionDto ToDto(this Transaction transaction)
    {
        return new TransactionDto
        {
            Id = transaction.Id,
            Amount = transaction.Amount,
            TransactionDate = transaction.TransactionDate,
            TransactionType = transaction.TransactionType,
            StatusId = transaction.StatusId,
            StatusName = transaction.Status?.Name ?? string.Empty,
            CreatedAt = transaction.CreatedAt,
            UpdatedAt = transaction.UpdatedAt
        };
    }

    public static Transaction ToEntity(this CreateTransactionDto dto)
    {
        return new Transaction
        {
            Amount = dto.Amount,
            TransactionDate = dto.TransactionDate,
            TransactionType = dto.TransactionType
        };
    }
}
