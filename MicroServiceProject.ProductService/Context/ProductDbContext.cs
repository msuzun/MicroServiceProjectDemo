using MicroServiceProject.ProductService.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroServiceProject.ProductService.Context;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}
