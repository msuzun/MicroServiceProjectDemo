using MicroServiceProject.CustomerService.Models;

namespace MicroServiceProject.CustomerService.Repositories.Abstract
{
    public interface IUnitOfWork
    {
        IGenericRepository<Customer> Customers { get; }
        Task<int> SaveAsync();
    }
}
