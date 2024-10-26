
using Application.Login;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Users;
internal sealed class Login : IEndpoint
{
    public sealed class Request
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("user/login", async (Request request,ISender sender, CancellationToken cancellationToken ) =>
        {
            var command = new LoginCommand
            {
                Email = request.Email,
                Password = request.Password
            };
            Result<string> result = await sender.Send(command, cancellationToken );
            return result.Match(Results.Ok, CustomResults.Problem);
        })
            .HasPermission(Permissions.UsersAccess)
            .WithTags(Tags.User);
    }
}
