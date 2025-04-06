using System;
using System.Data.Entity;
using ExpQueryFlattening.Models;

namespace ExpQueryFlattening.Ef.V6.Data
{
    internal class SalesDbContext : DbContext
    {
        public SalesDbContext() : base("name=SalesDbContext")
        {
            this.Database.Log = Console.WriteLine;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>()
                .HasRequired(d => d.Product)
                .WithMany(d => d.Sales)
                .Map(d => d.MapKey("ProductId"));
            
            modelBuilder.Entity<Sale>()
                .HasRequired(d => d.Customer)
                .WithMany(d => d.Sales)
                .Map(d => d.MapKey("CustomerId"));
            
            modelBuilder.Entity<Product>()
                .HasRequired(d => d.Category)
                .WithMany(d => d.Products)
                .Map(d => d.MapKey("CategoryId"));
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
    }
}
