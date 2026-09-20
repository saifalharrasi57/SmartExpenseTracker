using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) 
        : base(options)
    {
    }
      
      // Set() calls EF Core's DbContext.Set() method.
     // It returns a DbSet instance for querying and saving Category entities,
    // while avoiding C# nullability compiler warnings (CS8618) without needing '= null!'

      public DbSet<Category> Categories => Set<Category>();
      public DbSet<Expense> Expenses => Set<Expense>();
      public DbSet<Subscription> Subscriptions => Set<Subscription>();

      
     // Configures database schema rules, entity primary/foreign keys, property constraints,
      /// and relational behaviors (such as delete constraints and default SQL values) 
      /// using EF Core Fluent API.
      /// 
      /// Provides the builder API used to configure shape, relationships, and table mappings for entity types.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- CATEGORY CONFIGURATION ---
         var category = modelBuilder.Entity<Category>();
        // configuring the primary key 
        category.HasKey(c => c.Id);
        // configuring the Name attributes 
        category.Property(c => c.Name)
                .IsRequired()
               .HasMaxLength(100);
      

        // --- EXPENSE CONFIGURATION ---
        var expenses= modelBuilder.Entity<Expense>();
        expenses.HasKey(c => c.Id);
        expenses.Property(e => e.Amount) // ex.999,999,999,999,999.99
            .HasPrecision(18, 2);

        expenses.Property(e => e.Notes)
            .HasMaxLength(500);
        
       
        expenses. Property(e => e.ReceiptImageUrl)
                   .HasMaxLength(2048);
        // "If an INSERT statement does not explicitly provide a value for CreatedAt, the database engine MUST enforce this rule and generate GETUTCDATE()."
        expenses.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // 1-to-Many: 1 Category has Many Expenses
            //Plain English Translation
            // "Every Expense entity has ONE Category navigation property, while a Category can have MANY Expenses. Link them together using CategoryId as the foreign key in the Expense table, and RESTRICT/BLOCK the deletion of any Category that still has expenses linked to it."
            expenses.HasOne(e => e.Category)
                  .WithMany(c => c.Expenses)
                  .HasForeignKey(e => e.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);

        // --- SUBSCRIPTION CONFIGURATION ---
        var subs=  modelBuilder.Entity<Subscription>();
            subs.HasKey(s => s.Id);

            subs.Property(s => s.Cost)
                  .HasPrecision(18, 2);

            subs.Property(s => s.ServiceName)
                  .IsRequired()
                  .HasMaxLength(150);

            subs.Property(s => s.BillingCycle)
                  .IsRequired()
                  .HasMaxLength(50);

            // 1-to-Many: 1 Category has Many Subscriptions
            subs.HasOne(s => s.Category)
                  .WithMany(c =>c.Subscriptions)
                  .HasForeignKey(s => s.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);

        // --- DATA SEEDING ---
        modelBuilder.Entity<Category>().HasData(
            new Category { Id= 1, Name = "Food & Dining", IsSystemDefault = true },
            new Category { Id = 2, Name = "Subscriptions", IsSystemDefault = true },
            new Category { Id = 3, Name = "Transportation", IsSystemDefault = true },
            new Category { Id = 4, Name = "Utilities", IsSystemDefault = true }
        );
    }
}