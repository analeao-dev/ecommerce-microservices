using FluentValidation;
using ProductsMicroService.Communication.Requests;

namespace ProductsMicroService.Core.UseCases.Category.Register;

public class RegisterCategoryValidator : AbstractValidator<RegisterCategoryRequest>
{
    public RegisterCategoryValidator()
    {
        RuleFor(registerCategoryRequest => registerCategoryRequest.CategoryName)
            .NotEmpty().WithMessage("Category name is required.");    
    }
}