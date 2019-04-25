using System.Linq;
using System.Threading.Tasks;
using Domain.Common.Repository;
using Domain.Products;
using Domain.ProductSells;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ProductSellRepository: IProductSellRepository
    {
        private readonly ProductSellContext _context;

        public ProductSellRepository(ProductSellContext context)
        {
            _context = context;
        }
        
        public async Task<ProductSell> FindById(string id)
        {
            return await _context.ProductSell
                .Where(x => x.Identity.Equals(id))
                .Include(x => x.Signups)
                .Include(x => x.Combinations)
                .ThenInclude(comb => comb.SelectedAttributes)
                .SingleOrDefaultAsync();
        }

        public ProductSell Add(ProductSell entity)
        {
            return _context.ProductSell.Add(entity).Entity;
        }

        public void Update(ProductSell entity)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(ProductSell entity)
        {
            throw new System.NotImplementedException();
        }

        public IUnitOfWork UnitOfWork => _context;
    }
}