using MediatR;
using MicroServiceProject.ProductService.App.Features.Commands.Products;
using MicroServiceProject.ProductService.Repositories.Abstract;

namespace MicroServiceProject.ProductService.App.Features.Commands.Products.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
            if (product == null)
            {
                return false; // Ürün bulunamadı
            }
            product.Name = request.Name;
            product.Price = request.Price;
            product.Stock = request.Stock;

            _unitOfWork.Products.Update(product);
            await _unitOfWork.SaveAsync();

            return true; // Güncelleme başarılı
        }
    }
}
