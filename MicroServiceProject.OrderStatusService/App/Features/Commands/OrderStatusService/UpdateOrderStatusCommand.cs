using MediatR;

namespace MicroServiceProject.OrderStatusService.App.Features.Commands.OrderStatusService
{
    public class UpdateOrderStatusCommand : IRequest<bool>
    {
        public int OrderId { get; set; }
        public string OrderStatus { get; set; }
    }
}
