using MicroServiceProject.ProductService.Context;
using MicroServiceProject.ProductService.Models;
using MicroServiceProject.ProductService.Repositories.Abstract;

namespace MicroServiceProject.ProductService.Repositories.Concreate;

public class UnitOfWork : IUnitOfWork
{
    private readonly ProductDbContext _context;
    public IGenericRepository<Product> _products;

    public UnitOfWork(ProductDbContext context)
    {
        _context = context;
    }
    public IGenericRepository<Product> Products => _products ??= new GenericRepository<Product>(_context, _context.Products);
    public void Dispose() => _context.Dispose();


    public Task<int> SaveAsync() => _context.SaveChangesAsync();

}
