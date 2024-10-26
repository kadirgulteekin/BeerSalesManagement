using FluentValidation;

namespace Application.Register;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
        RuleFor(c => c.Email).EmailAddress();
        RuleFor(c => c.Password).NotEmpty().MinimumLength(8);
        RuleFor(c => c.Gender).NotEmpty();
    }
}
