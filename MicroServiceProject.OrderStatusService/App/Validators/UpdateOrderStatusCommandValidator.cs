using FluentValidation;
using MicroServiceProject.OrderStatusService.App.Features.Commands.OrderStatusService;

namespace MicroServiceProject.OrderStatusService.App.Validators
{
    public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
    {
        public UpdateOrderStatusCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0).WithMessage("OrderId must be greater than 0.");

            RuleFor(x => x.OrderStatus)
                .NotEmpty().WithMessage("OrderStatus is required.")
                .Must(status => new[] { "Pending", "Shipped", "Delivered" }.Contains(status))
                .WithMessage("OrderStatus must be one of the following: Pending, Shipped, Delivered.");
        }
    }
}
