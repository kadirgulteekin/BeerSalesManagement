using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Brands;

public class GetTopSellingBeerBrandsQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetTopSellingBeerBrandsQuery,List<TopSellingBeerBrandResponse>>
{
    public async Task<Result<List<TopSellingBeerBrandResponse>>> Handle(GetTopSellingBeerBrandsQuery command, CancellationToken cancellationToken)
    {
        var sales = await context.Sales
            .Where(s => s.LocationId == command.LocationId &&
                      s.SalesDate >= command.StartDate &&
                      s.SalesDate <= command.EndDate)
            .GroupBy(s => s.BeerBrandId)
            .Select(p => new
            {
                BeerBrandId = p.Key,
                TotalSales = p.Sum(s => s.Quantity),
                LocationName = p.FirstOrDefault().Location
            })
            .OrderByDescending(g => g.TotalSales)
            .ToListAsync(cancellationToken);

        var topSellingBrands = sales.Select(s => new TopSellingBeerBrandResponse
        {
            BeerBrandName = context.BeerBrands.Find(s.BeerBrandId).Name,
            LocationName = s.LocationName,
            TotalSales = s.TotalSales
        }).ToList();
        if(topSellingBrands is null)
        {
            return Result.Failure<List<TopSellingBeerBrandResponse>>(UserErrors.NoSalesData);
        }
        return topSellingBrands;
    }
}
