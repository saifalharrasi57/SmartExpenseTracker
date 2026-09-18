namespace Domain.Entities;

public class Expense
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Optional / Nullable properties
    public string? Notes { get; set; }
    public string? ReceiptImageUrl { get; set; }
    public bool IsParsedFromReceipt { get; set; }

    // Foreign Key & Navigation Property
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}