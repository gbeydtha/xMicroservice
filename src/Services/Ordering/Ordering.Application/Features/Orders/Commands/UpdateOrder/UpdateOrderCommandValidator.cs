using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{UserName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} must not exceed 50 chars.");

            RuleFor(p => p.EmailAddress)
                .NotEmpty().WithMessage("{EmailAddress}  is required");

            RuleFor(p => p.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice} is requried")
                .GreaterThan(0).WithMessage("{TotalPrice} should be greater than zero");
        }
    }
}
