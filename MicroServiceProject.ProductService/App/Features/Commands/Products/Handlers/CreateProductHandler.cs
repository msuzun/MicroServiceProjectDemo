using MediatR;
using MicroServiceProject.ProductService.App.Features.Commands.Products;
using MicroServiceProject.ProductService.Models;
using MicroServiceProject.ProductService.Repositories.Abstract;

namespace MicroServiceProject.ProductService.App.Features.Commands.Products.Handlers;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock
        };

        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.SaveAsync();

        return product.ProductId;
    }
}
