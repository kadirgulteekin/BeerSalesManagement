using Domain.Beers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Sales;

internal sealed class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.HasKey(s => s.SaleId);

        builder.Property(s => s.SaleId)
               .IsRequired();

        builder.Property(s => s.Date)
               .HasConversion(d => DateTime.SpecifyKind(d, DateTimeKind.Utc), v => v)
               .IsRequired();

        builder.Property(s => s.Quantity)
               .IsRequired();

      
    }
}