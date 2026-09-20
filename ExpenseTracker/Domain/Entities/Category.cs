using System.Collections;

namespace Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsSystemDefault { get; set; }

     //Navigation properties for 1-to-Many relationships
     public List<Expense> Expenses { get; set; } = new();
     public List<Subscription> Subscriptions { get; set; } = new();
}