using AutoMapper;
using MicroServiceProject.ProductService.App.DTOs;
using MicroServiceProject.ProductService.App.Features.Commands.Products;
using MicroServiceProject.ProductService.Models;

namespace MicroServiceProject.ProductService.App.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateProductDto, CreateProductCommand>();
            CreateMap<UpdateProductDto, UpdateProductCommand>();
            CreateMap<Product, GetProductsDto>();
            CreateMap<Product, GetProductByIdDto>();
        }
    }
}
