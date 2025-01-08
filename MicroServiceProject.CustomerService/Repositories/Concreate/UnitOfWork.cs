using MicroServiceProject.CustomerService.Context;
using MicroServiceProject.CustomerService.Models;
using MicroServiceProject.CustomerService.Repositories.Abstract;

namespace MicroServiceProject.CustomerService.Repositories.Concreate
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CustomerDbContext _context;
        private IGenericRepository<Customer> _customers;

        public UnitOfWork(CustomerDbContext context)
        {
            _context = context;
        }
        public IGenericRepository<Customer> Customers =>
          _customers ??= new GenericRepository<Customer>(_context,_context.Customers);

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
