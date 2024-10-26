
using Application.Brands;
using MediatR;
using Polly;
using Polly.Registry;
using SharedKernel;
using Web.Api.Endpoints.Users;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Brands;

public class GetBrandsByLocationId : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("locations/{locationId:guid}/top-selling-beer-brands", async (Guid locationId, DateTime startDate, DateTime endDate, ISender sender, CancellationToken cancellationToken, ResiliencePipelineProvider<string> pipelineProvider) =>
        {
            ResiliencePipeline<Result<List<TopSellingBeerBrandResponse>>> pipeline =
                pipelineProvider.GetPipeline<Result<List<TopSellingBeerBrandResponse>>>("gh-fallback");

            var query = new GetTopSellingBeerBrandsQuery(locationId, startDate, endDate);

            Result<List<TopSellingBeerBrandResponse>> result = await pipeline.ExecuteAsync(async (context) =>
            {
                return await sender.Send(query, cancellationToken);
            }, cancellationToken);

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
