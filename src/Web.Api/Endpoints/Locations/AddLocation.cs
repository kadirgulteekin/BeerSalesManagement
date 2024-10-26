
using Application.Location.AddLocation;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Locations;

internal sealed class AddLocation : IEndpoint
{
    public sealed class Request
    {
        public string LocationName { get; set; }
    }
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("locations", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new AddLocationCommand
            {
                LocationName = request.LocationName
            };

            Result<Guid> result = await sender.Send(command, cancellationToken);
            return result.Match(Results.Ok,CustomResults.Problem);
        })
        .RequireAuthorization()
        .WithTags(Tags.User);
    }
}
