using Application.Location.GetLocation;
using MediatR;
using Polly;
using Polly.Registry;
using SharedKernel;
using Web.Api.Endpoints.Users;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Locations;

internal sealed class GetLocation : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("locations/location", async (ISender sender,int pageNumber,int pageSize,CancellationToken cancellationToken,
             ResiliencePipelineProvider<string> pipelineProvider) =>
        {
            ResiliencePipeline<Result<List<LocationResponse>>> pipeline =
               pipelineProvider.GetPipeline<Result<List<LocationResponse>>>("gh-fallback");
            var query = new GetLocationsQuery(pageNumber,pageSize);
            Result<List<LocationResponse>> result = await pipeline.ExecuteAsync(async (context) =>
            {
                return await sender.Send(query, cancellationToken);
            });
            return result.Match(Results.Ok, CustomResults.Problem);
        })
            .HasPermission(Permissions.UsersAccess)
            .RequireAuthorization(policy=>policy.RequireRole("User"))
            .CacheOutput()
            .WithTags(Tags.Location);
    }
}
