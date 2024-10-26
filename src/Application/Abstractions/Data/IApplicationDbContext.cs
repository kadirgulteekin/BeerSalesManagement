using Domain.Beers;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<BeerBrand> BeerBrands { get; }
    DbSet<Domain.Beers.Location> Locations{ get; }
    DbSet<Sale> Sales { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

