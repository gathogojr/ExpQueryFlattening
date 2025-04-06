using System;
using ExpQueryFlattening.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
#if NETCOREAPP3_1
using Microsoft.Extensions.Logging.Console;
#endif

namespace ExpQueryFlattening.EfCore.Data
{
    internal class SalesDbContext : DbContext
    {
        private readonly IConfigurationRoot configuration;
#if NETCOREAPP3_1
        public static readonly ILoggerFactory LoggerFactory =
            Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information)
                    .AddConsole();
            });
#endif

        public SalesDbContext()
        {
            this.configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("SalesDbContext"))
#if NETCOREAPP3_1
                .UseLoggerFactory(LoggerFactory)
#else
                .LogTo(Console.WriteLine, LogLevel.Information)
#endif
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>()
                .Property(d => d.Amount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Product>()
                .Property(d => d.TaxRate)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Sale>()
                .HasOne(d => d.Product)
                .WithMany(d => d.Sales)
                .HasForeignKey("ProductId");

            modelBuilder.Entity<Sale>()
                .HasOne(d => d.Customer)
                .WithMany(d => d.Sales)
                .HasForeignKey("CustomerId");

            modelBuilder.Entity<Product>()
                .HasOne(d => d.Category)
                .WithMany(d => d.Products)
                .HasForeignKey("CategoryId");
        }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Sale> Sales { get; set; } = null!;
    }
}
