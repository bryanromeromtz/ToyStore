using Microsoft.EntityFrameworkCore;
using ToyStore.Models;

namespace ToyStore.Data
{
    public class ToyStoreDbContext : DbContext
    {
        public ToyStoreDbContext(DbContextOptions<ToyStoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Capitan America", Company = "Marvel", Price = 75.50M, AgeRestriction = 4 },
                new Product { Id = 2, Name = "Mario Bros", Company = "Nintendo", Price = 99.99M, AgeRestriction = 18, Description = "Vamos a la aventura" },
                new Product { Id = 3, Name = "Barbie", Company = "Mattel", Price = 25.99M, AgeRestriction = 12, Description = "Muñeca fashion." }
            );
        }
    }
}
