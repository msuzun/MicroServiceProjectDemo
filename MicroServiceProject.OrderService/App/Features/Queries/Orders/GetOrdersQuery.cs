using MediatR;
using MicroServiceProject.OrderService.App.DTOs;

namespace MicroServiceProject.OrderService.App.Features.Queries.Orders
{
    public class GetOrdersQuery : IRequest<IEnumerable<OrderDto>>
    {
    }
}
