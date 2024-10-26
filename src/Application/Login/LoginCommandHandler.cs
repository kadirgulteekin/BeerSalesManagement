using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Login;

internal sealed class LoginCommandHandler(
    IApplicationDbContext context,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider) : ICommandHandler<LoginCommand, string>
{
    public async Task<Result<string>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        User user = await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email == command.Email, cancellationToken);
        if(user is null)
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmail);
        }
        bool verified = passwordHasher.Verify(command.Password, user.PasswordHash);
        if (!verified)
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmail);
        }
        string token = tokenProvider.Create(user);
        return token;
    }
}
