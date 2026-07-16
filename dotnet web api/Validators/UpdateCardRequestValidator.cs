using dotnet_web_api.Dtos;
using FluentValidation;

namespace dotnet_web_api.Validators;

public class UpdateCardRequestValidator : AbstractValidator<UpdateCardRequest>
{
    public UpdateCardRequestValidator()
    {
        RuleFor(c => c.Id).GreaterThan(0).WithMessage("Id must be a positive integer");
        RuleFor(c => c.Name).Length(3, 50).WithMessage("Name must be between 3 and 50 characters.");
        RuleFor(c => c.Description).MaximumLength(200).WithMessage("Description must be at most 200 characters.");
        RuleFor(c => c.ColumnId).GreaterThan(0).WithMessage("ColumnId must be a positive integer");
        
    }
}