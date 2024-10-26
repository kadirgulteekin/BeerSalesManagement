using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Sales.GetSalesById;

internal class GetSalesByLocationIdQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetSalesByLocationIdQuery, List<SalesResponse>>
{
    public async Task<Result<List<SalesResponse>>> Handle(GetSalesByLocationIdQuery command, CancellationToken cancellationToken)
    {
        List<SalesResponse> sales = await context.Sales
             .Where(a => a.LocationId == command.LocationId &&
                       a.SalesDate >= command.StartDate &&
                       a.SalesDate <= command.EndDate)
             .AsNoTracking()
             .Select(p => new SalesResponse
             {
                 BeerBrandName = p.BeerBrand,
                 LocationName = p.Location
             })
             .ToListAsync(cancellationToken);
        if (sales is null)
        {
            return Result.Failure<List<SalesResponse>>(UserErrors.NotFoundAnyAvaliableLocations);
        }
        return sales;
    }
}
