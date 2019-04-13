using Domain.Products;
using MediatR;

namespace Application.ProductHandlers
{
    public class GetProductQuery: IRequest<Product>
    {
        public string Id { get; set; }
    }
}