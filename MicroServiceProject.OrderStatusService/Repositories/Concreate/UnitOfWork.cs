using MicroServiceProject.OrderStatusService.Context;
using MicroServiceProject.OrderStatusService.Models;
using MicroServiceProject.OrderStatusService.Repositories.Abstract;

namespace MicroServiceProject.OrderStatusService.Repositories.Concreate
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderStatusDbContext _context;
        private IGenericRepository<Order> _orders;
        public UnitOfWork(OrderStatusDbContext context)
        {
            _context = context;
        }
        public IGenericRepository<Order> Orders =>
           _orders ??= new GenericRepository<Order>(_context);


        public void Dispose() => _context.Dispose();

        public async Task SaveAsync() => await _context.SaveChangesAsync();

    }
}
