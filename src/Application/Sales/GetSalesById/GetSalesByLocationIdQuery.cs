using Application.Abstractions.Messaging;
using Application.Location.GetLocation;

namespace Application.Sales.GetSalesById;

public sealed record GetSalesByLocationIdQuery(Guid LocationId, DateTime StartDate, DateTime EndDate) : IQuery<List<SalesResponse>>;
