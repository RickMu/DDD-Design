using System.Threading.Tasks;
using Application.ProductSellHandlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class ProductSellsController: ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductSellsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateProductSell([FromBody] CreateProductSellCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}