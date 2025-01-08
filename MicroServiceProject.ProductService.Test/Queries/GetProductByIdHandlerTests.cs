using AutoMapper;
using MicroServiceProject.ProductService.App.DTOs;
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
    public class GetProductByIdHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetProductByIdHandler _handler;

        public GetProductByIdHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetProductByIdHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ValidId_ShouldReturnProduct()
        {
            // Arrange
            var command = new GetProductByIdQuery(1);
            var existingProduct = new Product { ProductId = 1, Name = "Test Product" };
            var productDto = new GetProductByIdDto {  Name = "Test Product" };

            _unitOfWorkMock.Setup(u => u.Products.GetByIdAsync(command.ProductId))
                           .ReturnsAsync(existingProduct);

            _mapperMock.Setup(m => m.Map<GetProductByIdDto>(existingProduct))
                       .Returns(productDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _unitOfWorkMock.Verify(u => u.Products.GetByIdAsync(command.ProductId), Times.Once);
            _mapperMock.Verify(m => m.Map<GetProductByIdDto>(existingProduct), Times.Once);
            Assert.Equal(productDto, result);
        }

    }
}
