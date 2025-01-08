using AutoMapper;
using MediatR;
using MicroServiceProject.OrderService.App.DTOs;
using MicroServiceProject.OrderService.App.Features.Queries.Orders;
using MicroServiceProject.OrderService.Repositories.Abstract;

namespace MicroServiceProject.OrderService.App.Features.Queries.Orders.Handlers
{
    public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrdersHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Orders.GetAllAsync();
            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

            foreach (var orderDto in orderDtos)
            {
                var orderItems = await _unitOfWork.OrderItems.FindAsync(oi => oi.OrderId == orderDto.OrderId);
                orderDto.OrderItems = _mapper.Map<List<OrderItemDto>>(orderItems);
            }

            return orderDtos;
        }
    }
}
