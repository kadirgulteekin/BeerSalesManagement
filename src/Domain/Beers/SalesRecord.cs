namespace Domain.Beers;

public sealed class SalesRecord
{
    public Guid LocationId { get; set; }
    public Guid BeerBrandId { get; set; }
    public DateTime SalesDate { get; set; }
    public int Quantity { get; set; }
}
