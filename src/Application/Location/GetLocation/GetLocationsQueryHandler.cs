using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System.Linq;

namespace Application.Location.GetLocation;

internal sealed class GetLocationsQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetLocationsQuery, List<LocationResponse>>
{
    public async Task<Result<List<LocationResponse>>> Handle(GetLocationsQuery command, CancellationToken cancellationToken)
    {

        var locations = await context.Locations
            .Skip((command.PageNumber - 1) * command.PageSize)
            .Take(command.PageSize)
            .ToListAsync(cancellationToken);

        var response = locations.Select(p => new LocationResponse
        {
            LocationId = p.LocationId,
            LocationName = p.Name
        })
             .ToList();
        if (response is null)
        {
            return Result.Failure<List<LocationResponse>>(UserErrors.NotFoundAnyAvaliableLocations);
        }
        return response;
    }
}
