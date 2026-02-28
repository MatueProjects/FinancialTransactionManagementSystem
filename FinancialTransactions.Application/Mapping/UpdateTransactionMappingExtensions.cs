using FinancialTransactions.Application.DTOs;
using FinancialTransactions.Domain.Entities;

namespace FinancialTransactions.Application.Mapping;

public static class UpdateTransactionMappingExtensions
{
    public static Transaction ToEntity(this UpdateTransactionDto dto, Transaction transaction)
    {
        transaction.TransactionDate = dto.TransactionDate;
        transaction.TransactionType = dto.TransactionType;

        return transaction;
    }
}
