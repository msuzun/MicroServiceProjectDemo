using MediatR;
using MicroServiceProject.ProductService.Repositories.Abstract;

namespace MicroServiceProject.ProductService.App.Features.Queries.Products.Handlers
{
    public class GetProductIdByNameQueryHandler : IRequestHandler<GetProductIdByNameQuery, int?>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetProductIdByNameQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int?> Handle(GetProductIdByNameQuery request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.FindAsync(p => p.Name == request.Name);
            return product?.ProductId; // Ürün bulunamazsa null döner
        }
    }
}
