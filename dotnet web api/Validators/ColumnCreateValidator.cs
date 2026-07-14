using dotnet_web_api.Dtos;
using FluentValidation;

namespace dotnet_web_api.Validators;

public class ColumnCreateValidator : AbstractValidator<CreateColumnRequest>
{
    public ColumnCreateValidator()
    {
        RuleFor(b => b.Name).Length(3, 50).WithMessage("Name must be between 3 and 50 characters.");
        RuleFor(b => b.Description).MaximumLength(200).WithMessage("Description must be at most 200 characters.");
    }
}