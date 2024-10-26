using Application.Abstractions.Messaging;

namespace Application.Brands;

public sealed record GetTopSellingBeerBrandsQuery(Guid LocationId, DateTime StartDate, DateTime EndDate) : IQuery<List<TopSellingBeerBrandResponse>>;