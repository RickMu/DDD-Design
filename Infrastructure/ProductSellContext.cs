using System.Threading;
using System.Threading.Tasks;
using Domain.Common.Repository;
using Domain.Customer;
using Domain.ProductAttributes;
using Domain.Products;
using Domain.ProductSells;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ProductSellContext: DbContext, IUnitOfWork
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        
        public DbSet<ProductSellContext> ProductSell { get; set; }
        public DbSet<ProductCombination> ProductCombinations { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<SellSignup> SellSignups { get; set; }
        public DbSet<SelectedAttribute> SelectedAttributes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await base.SaveChangesAsync();
            return true;
        }
    }
}