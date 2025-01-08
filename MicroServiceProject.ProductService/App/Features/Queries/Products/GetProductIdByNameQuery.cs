using MediatR;

namespace MicroServiceProject.ProductService.App.Features.Queries.Products
{
    public class GetProductIdByNameQuery : IRequest<int?>
    {
        public string Name { get; set; }

        public GetProductIdByNameQuery(string name)
        {
            Name = name;
        }
    }
}
