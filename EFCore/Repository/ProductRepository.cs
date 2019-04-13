using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Domain.Common.Domain;
using Domain.Common.Repository;
using Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly ProductSellContext _dbContext;

        public ProductRepository(ProductSellContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Product> FindById(string id)
        {
            return await _dbContext.Products
                .Where(e => e.Identity.Equals(id))
                .Include(e => e.Attributes)
                .ThenInclude(attr => attr.AttributeOptions)
                .SingleOrDefaultAsync();
        }

        public Product Add(Product entity)
        {
            return _dbContext.Products.Add(entity).Entity;
        }

        public void Update(Product entity)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(Product entity)
        {
            _dbContext.Remove(entity);
        }

        public IUnitOfWork UnitOfWork => _dbContext;
    }
}