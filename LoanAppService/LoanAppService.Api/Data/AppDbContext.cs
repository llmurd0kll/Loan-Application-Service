using LoanAppService.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace LoanAppService.Api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Loan> Loans => Set<Loan>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan>()
                .HasIndex(l => l.Number)
                .IsUnique();

            modelBuilder.Entity<Loan>()
                .Property(l => l.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Loan>()
                .Property(l => l.InterestValue)
                .HasPrecision(5, 2);
        }
    }
}
