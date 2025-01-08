using MediatR;
using MicroServiceProject.OrderStatusService.Repositories.Abstract;

namespace MicroServiceProject.OrderStatusService.App.Features.Commands.OrderStatusService.Handler
{
    public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatusCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateOrderStatusHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);
            if (order == null)
            {
                return false; // Sipariş bulunamadı
            }

            order.OrderStatus = request.OrderStatus;
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.SaveAsync();

            return true; // Güncelleme başarılı
        }
    }
}
