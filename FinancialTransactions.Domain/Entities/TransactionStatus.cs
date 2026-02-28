namespace FinancialTransactions.Domain.Entities;

public class TransactionStatus
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
