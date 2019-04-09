using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.ProductAttributes;
using Domain.ProductAttributes.Factory;
using Domain.Products;
using MediatR;

namespace Application.ProductHandlers
{
    public class CreateProductHandler: IRequestHandler<CreateProductCommand, string>
    {
        private IProductRepository _repository;
        private IProductAttributeFactory _factory;

        public CreateProductHandler(IProductRepository repository, IProductAttributeFactory factory)
        {
            _repository = repository;
            _factory = factory;
        }
           
        public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productAttributes = request.attributes.Select(x =>
                _factory.Create(x.Name, (AttributeType)x.AttributeType, x.AttributeOptions.Select(s => new AttributeOption(s)).ToArray())).ToList();
            var product = new Product(request.BasePrice, productAttributes, null);
            _repository.Add(product);
            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return product.Identity;
        }
    }
}