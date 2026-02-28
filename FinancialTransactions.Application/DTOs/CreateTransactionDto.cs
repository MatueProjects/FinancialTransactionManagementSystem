namespace FinancialTransactions.Application.DTOs;

public sealed class CreateTransactionDto
{
    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }

    public string TransactionType { get; set; } = string.Empty;
}
