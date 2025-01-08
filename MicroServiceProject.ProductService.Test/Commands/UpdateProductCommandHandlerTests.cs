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
    public class UpdateProductCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly UpdateProductHandler _handler;

        public UpdateProductCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new UpdateProductHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldUpdateProduct()
        {
            // Arrange
            var command = new UpdateProductCommand
            {
                ProductId = 1,
                Name = "Updated Product",
                Price = 120,
                Stock = 15
            };

            var existingProduct = new Product { ProductId = 1, Name = "Old Product", Price = 100, Stock = 10 };

            _unitOfWorkMock.Setup(u => u.Products.GetByIdAsync(command.ProductId))
                           .ReturnsAsync(existingProduct);

            _unitOfWorkMock.Setup(u => u.SaveAsync())
                           .ReturnsAsync(1); // Task<int> döndürmek için ReturnsAsync kullanın

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _unitOfWorkMock.Verify(u => u.Products.GetByIdAsync(command.ProductId), Times.Once);
            _unitOfWorkMock.Verify(u => u.Products.Update(It.IsAny<Product>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
            Assert.True(result); // Güncelleme başarılı mı?
        }
    }
}
