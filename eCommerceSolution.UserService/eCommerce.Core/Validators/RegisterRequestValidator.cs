using eCommerce.Core.DTO;
using FluentValidation;

namespace eCommerce.Core.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(1, 50).WithMessage("Name should be 1 to 50 characters long");

        RuleFor(x => x.Gender)
            .Must(x => Enum.TryParse<GenderOptions>(x, ignoreCase: true, out _)).WithMessage("invalid gender option. Allowed values: Male, Female, Others");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email address format");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}