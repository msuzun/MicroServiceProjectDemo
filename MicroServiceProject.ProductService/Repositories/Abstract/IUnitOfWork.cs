using MicroServiceProject.ProductService.Models;

namespace MicroServiceProject.ProductService.Repositories.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Product> Products { get; }
        Task<int> SaveAsync();
    }
}
