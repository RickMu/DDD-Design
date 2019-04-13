using MediatR;

namespace Application.ProductHandlers
{
    public class DeleteProductQuery: IRequest<bool>
    {
        public string Id { get; set; }
    }
}