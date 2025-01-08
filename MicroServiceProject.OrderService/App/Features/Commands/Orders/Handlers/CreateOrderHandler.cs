using MediatR;
using MicroServiceProject.OrderService.App.Validators;
using MicroServiceProject.OrderService.Models;
using MicroServiceProject.OrderService.Repositories.Abstract;

namespace MicroServiceProject.OrderService.App.Features.Commands.Orders.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductValidationService _productValidationService;
        private readonly IProductStockValidator _productStockValidator;
        public CreateOrderHandler(IUnitOfWork unitOfWork, IProductValidationService productValidationService, IProductStockValidator productStockValidator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _productValidationService = productValidationService ?? throw new ArgumentNullException(nameof(productValidationService));
            _productStockValidator = productStockValidator ?? throw new ArgumentNullException(nameof(productStockValidator));
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            foreach (var item in request.OrderItems)
            {
                var productExists = await _productValidationService.ValidateProductExists(item.ProductId);
                if (!productExists)
                {
                    throw new Exception($"Product with ID {item.ProductId} does not exist.");
                }

                var stock = await _productStockValidator.GetProductStock(item.ProductId);
                if (stock < item.Quantity)
                {
                    throw new Exception($"Insufficient stock for product with ID {item.ProductId}.");
                }
            }

            var order = new Order
            {
                CustomerId = request.CustomerId,
                OrderDate = DateTime.UtcNow,
                OrderStatus = "Pending",
                OrderItems = request.OrderItems.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    TotalPrice = i.TotalPrice
                }).ToList()
            };

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.SaveAsync();

            return order.OrderId;
        }
    }
}
