using MicroServiceProject.ProductService.Models;
using System.Linq.Expressions;

namespace MicroServiceProject.ProductService.Repositories.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<Product> FindAsync(Expression<Func<Product, bool>> predicate);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
