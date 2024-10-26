using Application.Abstractions.Messaging;

namespace Application.Register;

public sealed record RegisterUserCommand(string Email,string FirstName, string LastName,string Gender,string Password) 
    : ICommand<Guid>;
