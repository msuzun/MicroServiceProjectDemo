using MicroServiceProject.OrderStatusService.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace MicroServiceProject.OrderStatusService.Repositories.Concreate
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();


        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);



        public void Update(T entity) => _dbSet.Update(entity);

    }
}
