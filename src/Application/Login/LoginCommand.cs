using Application.Abstractions.Messaging;

namespace Application.Login;

public sealed class LoginCommand : ICommand<string>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

