using MediatR;
using MicroServiceProject.CustomerService.App.Features.Commands.Customer;
using MicroServiceProject.CustomerService.App.Features.Queries.Customer;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceProject.CustomerService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerCommand command)
        {
            var customerId = await _mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id = customerId }, null);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _mediator.Send(new GetCustomersQuery());
            return Ok(customers);
        }
        [HttpGet("test-exception")]
        public IActionResult TestException()
        {
            throw new Exception("This is a test exception for logging.");
        }
    }
}
