namespace FinancialTransactions.Application.DTOs;

public sealed class TransactionDto
{
    public Guid Id { get; set; }

    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }

    public string TransactionType { get; set; } = string.Empty;

    public Guid StatusId { get; set; }

    public string StatusName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

}
