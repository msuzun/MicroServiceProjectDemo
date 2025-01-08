using MediatR;

namespace MicroServiceProject.OrderService.App.Features.Queries.Orders
{
    public class CheckProductInOrdersQuery : IRequest<bool>
    {
        public int ProductId { get; }

        public CheckProductInOrdersQuery(int productId)
        {
            ProductId = productId;
        }
    }
}
