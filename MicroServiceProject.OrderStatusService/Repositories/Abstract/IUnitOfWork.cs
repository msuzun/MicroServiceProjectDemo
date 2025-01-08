using MicroServiceProject.OrderStatusService.Models;

namespace MicroServiceProject.OrderStatusService.Repositories.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Order> Orders { get; }
        Task SaveAsync();
    }
}
