using FluentValidation;
using MicroServiceProject.OrderService.App.Features.Commands.Orders;

namespace MicroServiceProject.OrderService.App.Validators
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithMessage("CustomerId must be greater than 0.");

            RuleFor(x => x.OrderItems)
                .NotEmpty()
                .WithMessage("OrderItems cannot be empty.");

            RuleForEach(x => x.OrderItems).ChildRules(orderItem =>
            {
                orderItem.RuleFor(o => o.ProductId)
                    .GreaterThan(0)
                    .WithMessage("ProductId must be greater than 0.");

                orderItem.RuleFor(o => o.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Quantity must be greater than 0.");

                orderItem.RuleFor(o => o.TotalPrice)
                    .GreaterThan(0)
                    .WithMessage("TotalPrice must be greater than 0.");
            });
        }
    }
}
