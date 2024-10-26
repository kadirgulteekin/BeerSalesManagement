using Domain.Beers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BeerBrands;

internal sealed class BeerBrandConfiguration : IEntityTypeConfiguration<BeerBrand>
{
    public void Configure(EntityTypeBuilder<BeerBrand> builder)
    {
        builder.HasKey(b => b.BeerBrandId);

        builder.Property(b => b.Name)
               .IsRequired()
               .HasMaxLength(100);
    }
}