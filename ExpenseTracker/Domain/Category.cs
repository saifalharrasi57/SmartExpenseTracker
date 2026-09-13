namespace Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsSystemDefault { get; set; }

    // Navigation properties for 1-to-Many relationships
    // public ICollection Expenses { get; set; } = new List();
    // public ICollection Subscriptions { get; set; } = new List();
}