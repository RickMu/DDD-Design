using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.ProductHandlers;
using Domain.Products;
using Domain.ProductSells;
using Domain.ProductSells.Factory;
using MediatR;

namespace Application.ProductSellHandlers
{
    public class CreateProductSellHandler: IRequestHandler<CreateProductSellCommand, CreateProductSellResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCombinationFactory _combinationFactory;

        public CreateProductSellHandler(IProductRepository productRepository, IProductCombinationFactory _combinationFactory)
        {
            _productRepository = productRepository;
            this._combinationFactory = _combinationFactory;
        }
        public async Task<CreateProductSellResponse> Handle(CreateProductSellCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindById(request.ProductId);

            var productCombinations = request.ProductCombination.Select(x => _combinationFactory.Create(
                    product,
                    new ProductPrice(x.Price, x.Discount, x.LowestPrice),
                    x.SpecificAttributes
                )).ToList();
            
            var productSell = new ProductSell((request.StartTime, request.EndTime), productCombinations, null);
            product.AddProductSell(productSell);
            
            await _productRepository.UnitOfWork.SaveEntitiesAsync();
            return productSell.Identity;
        }
    }
}