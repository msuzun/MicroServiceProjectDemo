using MicroServiceProject.ProductService.App.Features.Commands.Products;
using MicroServiceProject.ProductService.App.Features.Commands.Products.Handlers;
using MicroServiceProject.ProductService.Models;
using MicroServiceProject.ProductService.Repositories.Abstract;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServiceProject.ProductService.Test.Commands
{
    public class CreateProductCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly CreateProductHandler _handler;

        public CreateProductCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new CreateProductHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldCreateProduct()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = "Test Product",
                Price = 100,
                Stock = 100
            };

            var product = new Product
            {
                ProductId = 1,
                Name = command.Name,
                Price = command.Price,
                Stock = command.Stock
            };

            _unitOfWorkMock.Setup(u => u.Products.AddAsync(It.IsAny<Product>()))
                           .Returns(Task.CompletedTask)
                           .Callback<Product>(p => p.ProductId = product.ProductId);

            _unitOfWorkMock.Setup(u => u.SaveAsync())
                           .ReturnsAsync(1); // Task<int> döndürmek için ReturnsAsync kullanın

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _unitOfWorkMock.Verify(u => u.Products.AddAsync(It.IsAny<Product>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
            Assert.True(result > 0);
            Assert.Equal(product.ProductId, result);
        }
    }
}
