using AutoMapper;
using MediatR;
using MicroServiceProject.OrderService.App.DTOs;
using MicroServiceProject.OrderService.Repositories.Abstract;

namespace MicroServiceProject.OrderService.App.Features.Queries.Orders.Handlers
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrderByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);
            if (order == null)
            {
                return null; // Sipariş bulunamadı
            }

            var orderDto = _mapper.Map<OrderDto>(order);
            var orderItems = await _unitOfWork.OrderItems.FindAsync(oi => oi.OrderId == order.OrderId);
            orderDto.OrderItems = _mapper.Map<List<OrderItemDto>>(orderItems);

            return orderDto;
        }
    }
}
