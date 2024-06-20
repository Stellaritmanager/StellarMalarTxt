using Microsoft.EntityFrameworkCore;
using StellarBillingSystem.Models;
namespace StellarBillingSystem.Context
{
    public class BillingContext : DbContext
    {
        public BillingContext() { }

        public BillingContext(DbContextOptions options) : base(options)
        {
        }

        //LogTable
        public DbSet<LogsModel> SBLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=StellarBilling;Integrated Security=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogsModel>().HasKey(i => new { i.LogID });

        }
    }
}
