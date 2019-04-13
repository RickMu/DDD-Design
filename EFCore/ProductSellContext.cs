using System.Threading;
using System.Threading.Tasks;
using Domain.Common.Repository;
using Domain.Customer;
using Domain.ProductAttributes;
using Domain.ProductAttributes.Factory;
using Domain.Products;
using Domain.ProductSells;
using Infrastructure.EFConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ProductSellContext: DbContext, IUnitOfWork
    {
//        const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=DiscountCommerce.ProductSellContext;Trusted_Connection=True;";

        public ProductSellContext() : base() { }

        public ProductSellContext(DbContextOptions<ProductSellContext> options) : base(options) { }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseSqlServer(connectionString);
//        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<AttributeOption> AttributeOptions { get; set; }
        
        public DbSet<ProductSell> ProductSell { get; set; }
        public DbSet<ProductCombination> ProductCombinations { get; set; }
        public DbSet<SellSignup> SellSignups { get; set; }
        public DbSet<SelectedAttribute> SelectedAttributes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductAttributeWithDiscreteValue>();
            modelBuilder.Entity<ProductAttributeWithContinousValue>();
            
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductAttributeConfiguration());
            modelBuilder.ApplyConfiguration(new AttributeOptionConfiguration());

            modelBuilder.ApplyConfiguration(new ProductSellConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCombinationConfiguration());
            modelBuilder.ApplyConfiguration(new SelectedAttributeConfiguration());
            modelBuilder.ApplyConfiguration(new SellSignupConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await base.SaveChangesAsync();
            return true;
        }
    }
}