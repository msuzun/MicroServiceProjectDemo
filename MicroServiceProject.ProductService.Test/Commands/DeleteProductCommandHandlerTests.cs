using MicroServiceProject.ProductService.App.Features.Commands.Products;
using MicroServiceProject.ProductService.App.Features.Commands.Products.Handlers;
using MicroServiceProject.ProductService.App.Validators;
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
    public class DeleteProductCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ICheckProductInOrders> _checkProductInOrdersMock;
        private readonly DeleteProductHandler _handler;

        public DeleteProductCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _checkProductInOrdersMock = new Mock<ICheckProductInOrders>();
            _handler = new DeleteProductHandler(_unitOfWorkMock.Object, _checkProductInOrdersMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldDeleteProduct()
        {
            // Arrange
            var command = new DeleteProductCommand(1);
            var existingProduct = new Product { ProductId = 1, Name = "Test Product" };

            _unitOfWorkMock.Setup(u => u.Products.GetByIdAsync(command.ProductId))
                           .ReturnsAsync(existingProduct);

            _unitOfWorkMock.Setup(u => u.Products.Delete(existingProduct));
            _unitOfWorkMock.Setup(u => u.SaveAsync())
                           .ReturnsAsync(1);

            _checkProductInOrdersMock.Setup(c => c.IsProductInOrders(command.ProductId))
                                     .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _unitOfWorkMock.Verify(u => u.Products.GetByIdAsync(command.ProductId), Times.Once);
            _unitOfWorkMock.Verify(u => u.Products.Delete(existingProduct), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
            _checkProductInOrdersMock.Verify(c => c.IsProductInOrders(command.ProductId), Times.Once);
            Assert.True(result); // Silme işlemi başarılı mı?
        }

        [Fact]
        public async Task Handle_ProductInOrders_ShouldThrowException()
        {
            // Arrange
            var command = new DeleteProductCommand(1);
            var existingProduct = new Product { ProductId = 1, Name = "Test Product" };

            _unitOfWorkMock.Setup(u => u.Products.GetByIdAsync(command.ProductId))
                           .ReturnsAsync(existingProduct);

            _checkProductInOrdersMock.Setup(c => c.IsProductInOrders(command.ProductId))
                                     .ReturnsAsync(true);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal($"Product with ID {command.ProductId} cannot be deleted as it is associated with one or more orders.", exception.Message);

            _unitOfWorkMock.Verify(u => u.Products.GetByIdAsync(command.ProductId), Times.Once);
            _checkProductInOrdersMock.Verify(c => c.IsProductInOrders(command.ProductId), Times.Once);
            _unitOfWorkMock.Verify(u => u.Products.Delete(It.IsAny<Product>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Never);
        }
    }
}
