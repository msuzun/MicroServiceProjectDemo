using FluentValidation;
using MicroServiceProject.ProductService.App.DTOs;

namespace MicroServiceProject.ProductService.App.Validators;

public class UpdateProductDtoValidator : ProductDtoValidatorBase<UpdateProductDto>
{
    public UpdateProductDtoValidator():base()
    {
    }
}
