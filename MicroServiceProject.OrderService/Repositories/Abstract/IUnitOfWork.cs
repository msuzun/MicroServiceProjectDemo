using MicroServiceProject.OrderService.Models;

namespace MicroServiceProject.OrderService.Repositories.Abstract
{
    public interface IUnitOfWork
    {
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<OrderItem> OrderItems { get; }
        Task<int> SaveAsync();
    }
}
