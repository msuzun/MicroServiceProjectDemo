using MicroServiceProject.OrderService.Context;
using MicroServiceProject.OrderService.Models;
using MicroServiceProject.OrderService.Repositories.Abstract;

namespace MicroServiceProject.OrderService.Repositories.Concreate
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderDbContext _context;
        private IGenericRepository<Order> _orders;
        private IGenericRepository<OrderItem> _orderItems;

        public UnitOfWork(OrderDbContext context)
        {
            _context = context;
        }
        public IGenericRepository<Order> Orders =>
             _orders ??= new GenericRepository<Order>(_context,_context.Orders);

        public IGenericRepository<OrderItem> OrderItems =>
            _orderItems ??= new GenericRepository<OrderItem>(_context,_context.OrderItems);

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
