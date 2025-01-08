using MicroServiceProject.OrderService.App.DTOs;
using MicroServiceProject.OrderService.App.Features.Commands.Orders;
using MicroServiceProject.OrderService.App.Features.Commands.Orders.Handlers;
using MicroServiceProject.OrderService.App.Validators;
using MicroServiceProject.OrderService.Models;
using MicroServiceProject.OrderService.Repositories.Abstract;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServiceProject.OrderService.Test.Commands
{
    public class CreateOrderHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IProductValidationService> _productValidationServiceMock;
        private readonly Mock<IProductStockValidator> _productStockValidatorMock;
        private readonly CreateOrderHandler _handler;

        public CreateOrderHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _productValidationServiceMock = new Mock<IProductValidationService>();
            _productStockValidatorMock = new Mock<IProductStockValidator>();
            _handler = new CreateOrderHandler(_unitOfWorkMock.Object, _productValidationServiceMock.Object, _productStockValidatorMock.Object);
        }

        [Fact]
        public async Task Handle_ValidOrder_ShouldReturnOrderId()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                CustomerId = 1,
                OrderItems = new List<OrderItemDto>
        {
            new OrderItemDto { ProductId = 1, Quantity = 2, TotalPrice = 100 },
            new OrderItemDto { ProductId = 2, Quantity = 1, TotalPrice = 50 }
        }
            };

            var order = new Order
            {
                OrderId = 1,
                CustomerId = 1,
                OrderDate = DateTime.UtcNow,
                OrderStatus = "Pending"
            };

            _productStockValidatorMock.Setup(v => v.GetProductStock(It.IsAny<int>())).ReturnsAsync(10);
            _productValidationServiceMock.Setup(v => v.ValidateProductExists(It.IsAny<int>())).ReturnsAsync(true);

            // Mock AddAsync to set the OrderId
            _unitOfWorkMock.Setup(u => u.Orders.AddAsync(It.IsAny<Order>()))
                .Callback<Order>(o => o.OrderId = 1) // OrderId'yi elle set ediyoruz
                .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(u => u.SaveAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _unitOfWorkMock.Verify(u => u.Orders.AddAsync(It.IsAny<Order>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
            Assert.Equal(1, result); // Beklenen ve gerçek OrderId eşleşir
        }

        [Fact]
        public async Task Handle_InsufficientStock_ShouldThrowException()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                CustomerId = 1,
                OrderItems = new List<OrderItemDto>
            {
                new OrderItemDto { ProductId = 1, Quantity = 20, TotalPrice = 100 }
            }
            };

            _productStockValidatorMock.Setup(v => v.GetProductStock(It.IsAny<int>())).ReturnsAsync(10);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_InvalidProduct_ShouldThrowException()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                CustomerId = 1,
                OrderItems = new List<OrderItemDto>
            {
                new OrderItemDto { ProductId = 1, Quantity = 2, TotalPrice = 100 }
            }
            };

            _productStockValidatorMock.Setup(v => v.GetProductStock(It.IsAny<int>())).ReturnsAsync(10);
            _productValidationServiceMock.Setup(v => v.ValidateProductExists(It.IsAny<int>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }

    }
}
