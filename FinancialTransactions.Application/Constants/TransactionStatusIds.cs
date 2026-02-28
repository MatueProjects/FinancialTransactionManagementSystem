namespace FinancialTransactions.Application.Constants;

public static class TransactionStatusIds
{
    public static readonly Guid Active = Guid.Parse("c1e0e4b0-9e56-4f1d-9f8c-1e65a27a3e10");
    public static readonly Guid Inactive = Guid.Parse("93fbdabc-204c-4b61-9a12-2f21fb3d3a09");
    public static readonly Guid Pending = Guid.Parse("5a2f9e5d-1ddf-4a7f-9c9a-f3dbdb2e08e0");
    public static readonly Guid Completed = Guid.Parse("1c0f5f5d-5949-4b66-9d1a-d4b2d86c8271");
    public static readonly Guid Cancelled = Guid.Parse("0f5d30be-0ef1-4c5d-a1b8-90c76cc85bbf");
}
