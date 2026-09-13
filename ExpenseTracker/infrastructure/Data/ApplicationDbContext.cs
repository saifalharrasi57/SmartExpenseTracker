using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) 
        : base(options)
    {
    }

    public DbSet Categories => Set();
    public DbSet Expenses => Set();
    public DbSet Subscriptions => Set();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- CATEGORY CONFIGURATION ---
        modelBuilder.Entity(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Name)
                  .IsRequired()
                  .HasMaxLength(100);
        });

        // --- EXPENSE CONFIGURATION ---
        modelBuilder.Entity(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Amount)
                  .HasPrecision(18, 2);

            entity.Property(e => e.MerchantName)
                  .IsRequired()
                  .HasMaxLength(150);

            entity.HasOne(e => e.Category)
                  .WithMany(c => c.Expenses)
                  .HasForeignKey(e => e.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // --- SUBSCRIPTION CONFIGURATION ---
        modelBuilder.Entity(entity =>
        {
            entity.HasKey(s => s.Id);

            entity.Property(s => s.Cost)
                  .HasPrecision(18, 2);

            entity.Property(s => s.ServiceName)
                  .IsRequired()
                  .HasMaxLength(150);

            entity.HasOne(s => s.Category)
                  .WithMany(c => c.Subscriptions)
                  .HasForeignKey(s => s.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // --- DATA SEEDING ---
        modelBuilder.Entity().HasData(
            new Category { Id = 1, Name = "Food & Dining", IsSystemDefault = true },
            new Category { Id = 2, Name = "Subscriptions", IsSystemDefault = true },
            new Category { Id = 3, Name = "Transportation", IsSystemDefault = true },
            new Category { Id = 4, Name = "Utilities", IsSystemDefault = true }
        );
    }
}