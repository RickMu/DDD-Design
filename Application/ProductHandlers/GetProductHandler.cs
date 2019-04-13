using System.Threading;
using System.Threading.Tasks;
using Domain.Products;
using MediatR;

namespace Application.ProductHandlers
{
    public class GetProductHandler: IRequestHandler<GetProductQuery, Product>
    {
        private readonly IProductRepository _productRepository;

        public GetProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.FindById(request.Id);
        }
    }
}