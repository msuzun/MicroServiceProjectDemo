using MicroServiceProject.ProductService.App.DTOs;

namespace MicroServiceProject.ProductService.App.Validators;

public class CreateProductValidator : ProductDtoValidatorBase<CreateProductDto>
{
    public CreateProductValidator():base()
    {
    }
}

