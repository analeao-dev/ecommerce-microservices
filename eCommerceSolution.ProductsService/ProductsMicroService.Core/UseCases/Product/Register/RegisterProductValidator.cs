using FluentValidation;
using ProductsMicroService.Communication.Requests;

namespace ProductsMicroService.Core.UseCases.Product.Register;

public class RegisterProductValidator : AbstractValidator<RegisterProductRequest>
{
    public RegisterProductValidator()
    {
        RuleFor(p => p.ProductName)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(255).WithMessage("Product name must not exceed 255 characters.");

        RuleFor(p => p.UnitPrice)
            .NotNull().WithMessage("Unit price is required.")
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(p => p.QuantityInStock)
            .NotNull().WithMessage("Quantity in stock is required.")
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

        RuleFor(p => p.CategoryId)
            .NotEmpty().WithMessage("Category is required.");
    }
}