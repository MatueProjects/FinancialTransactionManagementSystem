namespace FinancialTransactions.Application.DTOs;

public sealed class TransactionStatusDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
}
