using AutoMapper;
using MicroServiceProject.OrderService.App.DTOs;
using MicroServiceProject.OrderService.Models;

namespace MicroServiceProject.OrderService.App.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();
        }
    }
}
