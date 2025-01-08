using AutoMapper;
using MicroServiceProject.ProductService.App.Features.Queries.Products.Handlers;
using MicroServiceProject.ProductService.App.Features.Queries.Products;
using MicroServiceProject.ProductService.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroServiceProject.ProductService.Repositories.Abstract;

namespace MicroServiceProject.ProductService.Test.Queries
{
    public class GetProductIdByNameQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly GetProductIdByNameQueryHandler _handler;

        public GetProductIdByNameQueryHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new GetProductIdByNameQueryHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ValidName_ShouldReturnProductId()
        {
            // Arrange
            var command = new GetProductIdByNameQuery("Test Product");
            var existingProduct = new Product { ProductId = 1, Name = "Test Product" };

            _unitOfWorkMock.Setup(u => u.Products.FindAsync(p => p.Name == command.Name))
                           .ReturnsAsync(existingProduct);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _unitOfWorkMock.Verify(u => u.Products.FindAsync(p => p.Name == command.Name), Times.Once);
        }

    }
}
