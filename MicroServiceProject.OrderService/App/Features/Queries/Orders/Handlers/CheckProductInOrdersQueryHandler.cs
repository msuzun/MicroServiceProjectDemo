using MediatR;
using MicroServiceProject.OrderService.Repositories.Abstract;

namespace MicroServiceProject.OrderService.App.Features.Queries.Orders.Handlers
{
    public class CheckProductInOrdersQueryHandler : IRequestHandler<CheckProductInOrdersQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckProductInOrdersQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CheckProductInOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.OrderItems
               .AnyAsync(oi => oi.ProductId == request.ProductId);
        }
    }
}
