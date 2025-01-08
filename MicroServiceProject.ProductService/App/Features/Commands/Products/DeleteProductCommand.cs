using MediatR;

namespace MicroServiceProject.ProductService.App.Features.Commands.Products;

public class DeleteProductCommand : IRequest<bool>
{
    public int ProductId { get; set; }
    public DeleteProductCommand(int productId)
    {
        ProductId = productId;
    }
}
