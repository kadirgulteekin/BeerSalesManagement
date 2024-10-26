
using Application.Register;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;

internal sealed class Register : IEndpoint
{
    public sealed record Request(string Email,string FristName, string LastName, string Gender,string Passwowrd);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("user/register", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new RegisterUserCommand(
                request.LastName,
                request.FristName,
                request.LastName,
                request.Gender,
                request.Passwowrd);

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
            .WithTags(Tags.User);
    }
}
