using FluentValidation;

namespace Application.Location.AddLocation;

internal sealed class AddLocationRequestValidator : AbstractValidator<AddLocationCommand>
{
    public AddLocationRequestValidator()
    {
       RuleFor(c=>c.LocationName).NotEmpty();
    }
}
