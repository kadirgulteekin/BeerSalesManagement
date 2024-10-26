using FluentValidation;

namespace Application.Login;

internal sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(c => c.Email).EmailAddress();
        RuleFor(c => c.Password).NotEmpty();
    }
}
