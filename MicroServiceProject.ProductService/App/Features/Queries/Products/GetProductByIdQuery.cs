using MediatR;
using MicroServiceProject.ProductService.App.DTOs;

namespace MicroServiceProject.ProductService.App.Features.Queries.Products;

public class GetProductByIdQuery : IRequest<GetProductByIdDto>
{
    public int ProductId { get; set; }

    public GetProductByIdQuery(int productId)
    {
        ProductId = productId;
    }
}
