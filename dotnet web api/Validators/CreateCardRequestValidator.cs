using dotnet_web_api.Dtos;
using FluentValidation;

namespace dotnet_web_api.Validators;

public class CreateCardRequestValidator : AbstractValidator<CreateCardRequest>
{
    public CreateCardRequestValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required.") // NotEmpty to avoid 'null' inputs which Length doesn't cover
            .Length(3, 50).WithMessage("Name must be between 3 and 50 characters.");
        RuleFor(c => c.Description).MaximumLength(200).WithMessage("Description must be at most 200 characters.");
        RuleFor(c => c.ColumnId).GreaterThan(0).WithMessage("ColumnId must be a positive integer");
        
        
    }
}