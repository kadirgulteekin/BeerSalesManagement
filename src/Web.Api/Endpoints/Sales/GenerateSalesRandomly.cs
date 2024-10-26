using Application.Sales.GenerateSales;
using MediatR;
using Web.Api.Endpoints.Users;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Sales;

internal sealed class GenerateSalesRandomly : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("locations/{locationId:guid}/sales/generate", async (Guid locationId, DateTime startDate, DateTime endDate, int count, ISender sender,
        CancellationToken cancellationToken) =>
        {
            var command = new GenerateRandomSalesCommand(locationId, startDate, endDate, count);
            var result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
    .HasPermission(Permissions.UsersAccess)
    .RequireAuthorization()
    .WithTags(Tags.User);
    }
}
