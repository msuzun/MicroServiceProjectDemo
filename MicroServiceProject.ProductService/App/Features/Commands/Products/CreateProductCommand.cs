using MediatR;

namespace MicroServiceProject.ProductService.App.Features.Commands.Products;

public class CreateProductCommand : IRequest<int>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
