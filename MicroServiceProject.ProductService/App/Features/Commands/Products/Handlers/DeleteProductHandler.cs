using MediatR;
using MicroServiceProject.ProductService.App.Features.Commands.Products;
using MicroServiceProject.ProductService.App.Validators;
using MicroServiceProject.ProductService.Repositories.Abstract;

namespace MicroServiceProject.ProductService.App.Features.Commands.Products.Handlers;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICheckProductInOrders _checkProductInOrders;

    public DeleteProductHandler(IUnitOfWork unitOfWork, ICheckProductInOrders checkProductInOrders)
    {
        _unitOfWork = unitOfWork;
        _checkProductInOrders = checkProductInOrders;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
        if (product == null)
        {
            return false; // Ürün bulunamadı
        }
        // Ürün siparişlerde var mı kontrol et
        var isProductInOrders = await _checkProductInOrders.IsProductInOrders(request.ProductId);
        if (isProductInOrders)
        {
            throw new Exception($"Product with ID {request.ProductId} cannot be deleted as it is associated with one or more orders.");
        }
        _unitOfWork.Products.Delete(product);
        await _unitOfWork.SaveAsync();

        return true; // Silme işlemi başarılı

    }
}
