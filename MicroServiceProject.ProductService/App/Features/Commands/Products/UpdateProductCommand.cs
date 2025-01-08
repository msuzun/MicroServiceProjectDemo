using MediatR;

namespace MicroServiceProject.ProductService.App.Features.Commands.Products;

public class UpdateProductCommand : IRequest<bool>
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
