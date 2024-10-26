
using Application.Location.GetLocation;
using Application.Sales.GetSalesById;
using MediatR;
using Polly;
using Polly.Registry;
using SharedKernel;
using System.Security.Claims;
using Web.Api.Endpoints.Users;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Sales;

public class GetSalesByLocationId : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("locations{locationId:guid}", async (Guid locationId,DateTime startDate,DateTime endDate, ISender sender,
            CancellationToken cancellationToken, ResiliencePipelineProvider<string> pipelineProvider) =>
        {
            ResiliencePipeline<Result<List<SalesResponse>>> pipeline =
           pipelineProvider.GetPipeline<Result<List<SalesResponse>>>("gh-fallback");
            var query = new GetSalesByLocationIdQuery(locationId,startDate,endDate);
            Result<List<SalesResponse>> result = await pipeline.ExecuteAsync(async (context) =>
            {
                return await sender.Send(query, cancellationToken);
            });
            return result.Match(Results.Ok, CustomResults.Problem);
        })
            .HasPermission(Permissions.UsersAccess)
            .RequireAuthorization()
            .WithTags(Tags.User)
            .CacheOutput(builder => builder
                .Expire(TimeSpan.FromMinutes(10))
                .VaryByValue((httpcontext, _) =>
                {
                    return ValueTask.FromResult(new KeyValuePair<string, string>(
                        nameof(ClaimsPrincipalExtensions.GetUserId),
                        httpcontext.User.GetUserId().ToString()));
                }),
                true);
    }
}
