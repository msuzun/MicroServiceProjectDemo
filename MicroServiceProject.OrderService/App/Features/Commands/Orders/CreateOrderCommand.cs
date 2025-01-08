using MediatR;
using MicroServiceProject.OrderService.App.DTOs;

namespace MicroServiceProject.OrderService.App.Features.Commands.Orders
{
    public class CreateOrderCommand : IRequest<int>
    {
        public int CustomerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
