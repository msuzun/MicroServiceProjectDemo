using MediatR;
using MicroServiceProject.OrderService.App.DTOs;

namespace MicroServiceProject.OrderService.App.Features.Queries.Orders
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public int OrderId { get; set; }

        public GetOrderByIdQuery(int orderId)
        {
            OrderId = orderId;
        }
    }
}
