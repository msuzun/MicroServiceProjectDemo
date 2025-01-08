using AutoMapper;
using MicroServiceProject.OrderStatusService.App.DTOs;
using MicroServiceProject.OrderStatusService.App.Features.Commands.OrderStatusService;


namespace MicroServiceProject.OrderStatusService.App.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderStatusDto, UpdateOrderStatusCommand>();
        }
    }
}
