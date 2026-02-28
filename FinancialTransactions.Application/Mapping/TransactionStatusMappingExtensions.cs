using FinancialTransactions.Application.DTOs;
using FinancialTransactions.Domain.Entities;

namespace FinancialTransactions.Application.Mapping;

public static class TransactionStatusMappingExtensions
{
    public static TransactionStatusDto ToDto(this TransactionStatus status)
    {
        return new TransactionStatusDto
        {
            Id = status.Id,
            Name = status.Name
        };
    }
}
