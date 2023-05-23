using Microsoft.EntityFrameworkCore;
using VodafoneCashApi.Models;

namespace VodafoneCashApi.Data
{
    public class data : DbContext
    {
        public data(DbContextOptions<data> options) : base(options) { }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<Transactions>()
            .HasKey(c => c.TransactionId)
            .HasName("PK_Transactions");
            

            modelBuilder.Entity<Numbers>()
            .HasMany(c=>c.Transactions)
            .WithOne()
            .HasForeignKey(e => e.NumberId);
            
        }

        public DbSet<Numbers> Numbers { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
    }
}

