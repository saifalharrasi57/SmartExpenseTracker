namespace Domain.Entities;

public class Expense
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string MerchantName { get; set; } = string.Empty;
    public DateTime TransactionDate { get; set; }

    // Foreign Key
    public int CategoryId { get; set; }

    // Navigation property
    public Category Category { get; set; } = null!;
}