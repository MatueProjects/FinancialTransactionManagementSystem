namespace FinancialTransactions.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }

    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }

    public string TransactionType { get; set; } = string.Empty;

    public Guid StatusId { get; set; }

    public TransactionStatus? Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

}
