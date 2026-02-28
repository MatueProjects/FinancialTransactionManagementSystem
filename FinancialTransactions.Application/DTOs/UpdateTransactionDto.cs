namespace FinancialTransactions.Application.DTOs;

public sealed class UpdateTransactionDto
{
    public DateTime TransactionDate { get; set; }

    public string TransactionType { get; set; } = string.Empty;

}
