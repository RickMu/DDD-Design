using System.Threading;
using System.Threading.Tasks;
using Domain.Common.Domain;
using Domain.Products;
using MediatR;

namespace Application.ProductHandlers
{
    public class DeleteProductHandler: IRequestHandler<DeleteProductQuery, bool>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public async Task<bool> Handle(DeleteProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindById(request.Id);

            if (product.CanBeDeleted())
            {
                _productRepository.Remove(product);
                await _productRepository.UnitOfWork.SaveEntitiesAsync();
                return true;
            }
            return false;
        }
    }
}