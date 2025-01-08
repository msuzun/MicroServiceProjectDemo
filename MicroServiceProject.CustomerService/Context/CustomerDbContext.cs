using MicroServiceProject.CustomerService.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroServiceProject.CustomerService.Context
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
    }
    
}
