using Microsoft.EntityFrameworkCore;

namespace WebAPIBasic.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        //public DbSet<Product> Products { get; set; }
        // other DbSet properties for other entities

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Products>().ToTable("Product").HasKey("ProductId");
        }
    }
}
