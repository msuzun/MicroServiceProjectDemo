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
    public class GetProductsHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetProductsHandler _handler;

        public GetProductsHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetProductsHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnProducts()
        {
            // Arrange
            var command = new GetProductsQuery();
            var existingProducts = new List<Product>
            {
                new Product { ProductId = 1, Name = "Test Product 1" },
                new Product { ProductId = 2, Name = "Test Product 2" }
            };
            var productDtos = new List<GetProductsDto>
            {
                new GetProductsDto { Name = "Test Product 1" },
                new GetProductsDto { Name = "Test Product 2" }
            };

            _unitOfWorkMock.Setup(u => u.Products.GetAllAsync())
                           .ReturnsAsync(existingProducts);

            _mapperMock.Setup(m => m.Map<IEnumerable<GetProductsDto>>(existingProducts))
                       .Returns(productDtos);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _unitOfWorkMock.Verify(u => u.Products.GetAllAsync(), Times.Once);
            _mapperMock.Verify(m => m.Map<IEnumerable<GetProductsDto>>(existingProducts), Times.Once);
            Assert.Equal(productDtos, result);
        }

    }
}
