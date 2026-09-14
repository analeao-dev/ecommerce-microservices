using FluentValidation;
using ProductsMicroService.Communication.Requests;

public class DeleteCategoryValidator : AbstractValidator<DeleteCategoryRequest>
{
    public DeleteCategoryValidator()
    {
        RuleFor(x => x.IsActive)
            .NotEmpty().WithMessage("IsActive is required.")
            .Must(value => value == true || value == false).WithMessage("IsActive must be a boolean value.");
    }
}