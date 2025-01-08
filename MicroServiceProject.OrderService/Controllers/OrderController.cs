using MediatR;
using MicroServiceProject.OrderService.App.Features.Commands.Orders;
using MicroServiceProject.OrderService.App.Features.Queries;
using MicroServiceProject.OrderService.App.Features.Queries.Orders;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace MicroServiceProject.OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderCommand command)
        {
            var orderId = await _mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id = orderId }, null); // json döndrülmeli
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _mediator.Send(new GetOrdersQuery());
            return Ok(orders);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery(id));

            if (order == null)
            {
                return NotFound("Order not found.");
            }

            return Ok(order); // json döndrülmeli
        }
        [HttpGet("product/{productId}/exists")]
        public async Task<IActionResult> CheckProductInOrders(int productId)
        {
            var isProductInOrders = await _mediator.Send(new CheckProductInOrdersQuery(productId));
            return Ok(isProductInOrders); // true veya false döner
        }
        [HttpGet("test-exception")]
        public IActionResult TestException()
        {
            throw new Exception("This is a test exception for logging."); // json döndrülmeli
        }
    }
}
