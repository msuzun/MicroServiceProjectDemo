using FluentValidation;
using MicroServiceProject.ProductService.App.DTOs;

namespace MicroServiceProject.ProductService.App.Validators;

public abstract class ProductDtoValidatorBase<T> : AbstractValidator<T> where T : class
{
    public ProductDtoValidatorBase()
    {
        RuleFor(x => x.GetType().GetProperty("Name").GetValue(x, null) as string)
        .NotEmpty().WithMessage("Ürün adı boş olamaz.")
        .MaximumLength(100).WithMessage("Ürün adı 100 karakterden uzun olamaz.");

        RuleFor(x => Convert.ToDecimal(x.GetType().GetProperty("Price").GetValue(x, null)))
            .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır.");

        RuleFor(x => Convert.ToInt32(x.GetType().GetProperty("Stock").GetValue(x, null)))
            .GreaterThanOrEqualTo(0).WithMessage("Stok miktarı negatif olamaz.");
    }
}
