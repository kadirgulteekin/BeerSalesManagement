using Application.Abstractions.Messaging;

namespace Application.Location.GetLocation;

public sealed record GetLocationsQuery(int PageNumber,int PageSize) : IQuery<List<LocationResponse>>;
