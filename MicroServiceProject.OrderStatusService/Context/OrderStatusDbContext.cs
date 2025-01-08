using MicroServiceProject.OrderStatusService.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroServiceProject.OrderStatusService.Context
{
    public class OrderStatusDbContext : DbContext
    {
        public OrderStatusDbContext(DbContextOptions<OrderStatusDbContext> options) : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
    }
    
}
