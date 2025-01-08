using MediatR;
using MicroServiceProject.ProductService.App.DTOs;

namespace MicroServiceProject.ProductService.App.Features.Queries.Products
{
    public class GetProductsQuery : IRequest<IEnumerable<GetProductsDto>>
    {
    }
}
