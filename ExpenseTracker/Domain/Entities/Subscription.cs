namespace Domain.Entities;

public class Subscription
{
    public int Id { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public DateTime NextBillingDate { get; set; }
    public string BillingCycle { get; set; } = string.Empty; // e.g., "Monthly", "Yearly"
    public bool IsActive { get; set; } = true;

    // Trial-related attributes
    public bool IsTrial { get; set; }
    public DateTime? TrialEndDate { get; set; } // Optional: null if not a trial

    // Notification attributes
    public int NotifyDaysBefore { get; set; } = 3; // Default reminder threshold
    public DateTime? LastNotifiedAt { get; set; } // Optional: null until first alert sent

    // Foreign Key & Navigation Property
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}