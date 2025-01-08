using AutoMapper;
using MediatR;
using MicroServiceProject.OrderStatusService.App.DTOs;
using MicroServiceProject.OrderStatusService.App.Features.Commands.OrderStatusService;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceProject.OrderStatusService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderStatusController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public OrderStatusController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, OrderStatusDto dto)
        {
            
            var updateCommand = _mapper.Map<UpdateOrderStatusCommand>(dto);
            updateCommand.OrderId = id;

            var result = await _mediator.Send(updateCommand);
            if (!result)
            {
                return NotFound("Order not found.");
            }

            return Ok(); // Güncelleme başarılı
        }
        [HttpGet("test-exception")]
        public IActionResult TestException()
        {
            throw new Exception("This is a test exception for logging.");
        }
    }
}
