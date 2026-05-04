using CoreBankingMiniApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreBankingMiniApi.Data;

public class BankingDbContext : DbContext
{
    public BankingDbContext(DbContextOptions<BankingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<BankTransaction> Transactions => Set<BankTransaction>();
    public DbSet<CreditSimulation> CreditSimulations => Set<CreditSimulation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.DocumentNumber)
            .IsUnique();

        modelBuilder.Entity<Account>()
            .HasIndex(a => a.AccountNumber)
            .IsUnique();

        modelBuilder.Entity<Account>()
            .Property(a => a.Balance)
            .HasPrecision(18, 2);

        modelBuilder.Entity<BankTransaction>()
            .Property(t => t.Amount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<CreditSimulation>()
            .Property(c => c.RequestedAmount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<CreditSimulation>()
            .Property(c => c.InterestRate)
            .HasPrecision(18, 4);

        modelBuilder.Entity<CreditSimulation>()
            .Property(c => c.TotalAmount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<CreditSimulation>()
            .Property(c => c.MonthlyPayment)
            .HasPrecision(18, 2);
    }
}
