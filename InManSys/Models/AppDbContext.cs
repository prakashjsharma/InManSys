using Microsoft.EntityFrameworkCore;

namespace InManSys.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Decimal precision to avoid truncation warning
            modelBuilder.Entity<Product>()
                .Property(p => p.UnitPrice)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}
