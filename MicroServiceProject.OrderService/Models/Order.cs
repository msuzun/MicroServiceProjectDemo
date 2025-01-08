﻿namespace MicroServiceProject.OrderService.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
