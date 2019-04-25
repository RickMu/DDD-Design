using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.ProductHandlers;
using Domain.Products;
using Domain.ProductSells;
using MediatR;

namespace Application.ProductSellHandlers
{
    public class CreateProductSellHandler: IRequestHandler<CreateProductSellCommand, CreateProductSellResponse>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductSellHandler(IProductSellRepository productSellRepository, IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<CreateProductSellResponse> Handle(CreateProductSellCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindById(request.ProductId);

            var productCombinations = request.ProductCombination.Select(x =>
                new ProductCombination(new ProductPrice(x.Price, x.Discount, x.LowestPrice), 
                    x.SpecificAttributes )).ToList();
            
            var productSell = new ProductSell((request.StartTime, request.EndTime), productCombinations, null);
            product.AddProductSell(productSell);
            
            await _productRepository.UnitOfWork.SaveEntitiesAsync();
            return productSell.Identity;
        }
    }
}