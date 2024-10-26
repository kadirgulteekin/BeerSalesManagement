using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Location.GetLocation;
using Domain.Beers;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Sales.GenerateSales;

internal sealed class GenerateRandomSalesCommandHandler(IApplicationDbContext context, INoSqlDbContext noSqlDbContext)
    : ICommandHandler<GenerateRandomSalesCommand, Result>
{
    public async Task<Result<Result>> Handle(GenerateRandomSalesCommand command, CancellationToken cancellationToken)
    {
        var location = await context.Locations
             .AsNoTracking()
             .Select(p => new LocationResponse
             {
                 LocationId = command.LocationId
             })
             .FirstOrDefaultAsync(cancellationToken);
        if (location is null)
        {
            return Result.Failure<string>(UserErrors.NotFoundAnyAvaliableLocations);
        }

        var beerBrand = await context.BeerBrands
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        var random = new Random();
        var salesRecords = new List<SalesRecord>();
        for (int i = 0; i < command.Count; i++) 
        { 
            var randomBeerBrand = beerBrand[random.Next(beerBrand.Count)];
            var randomDate =SharedKernel.GetDate.GetRandomDate(command.StartDate,command.EndDate,random);
            var salesRecord = new SalesRecord
            {
                LocationId = command.LocationId,
                BeerBrandId = randomBeerBrand.BeerBrandId,
                SalesDate = randomDate,
                Quantity = random.Next(1, 10)
            };
            salesRecords.Add(salesRecord);
        }
        await noSqlDbContext.SalesRecords.InsertManyAsync(salesRecords);
        return Result.Success();
    }
}
