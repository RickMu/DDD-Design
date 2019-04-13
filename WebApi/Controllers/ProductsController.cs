using System.Threading.Tasks;
using Application.ProductHandlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController: ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{productId}")]
        public async Task<ActionResult> GetProduct(string productId)
        {
            var value = await _mediator.Send(new GetProductQuery {Id = productId});
            return Ok(value);
        }

        [HttpPost]
        //Ideally should be productDTO
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{productId}")]
        public async Task<ActionResult<bool>> DeleteProduct(string productId)
        {
            var result = await _mediator.Send(new DeleteProductQuery {Id = productId});
            return result;
        }
    }
}