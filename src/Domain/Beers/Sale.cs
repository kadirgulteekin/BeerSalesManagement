namespace Domain.Beers;

public sealed class Sale
{
    public Guid SaleId { get; set; }      
    public Guid LocationId { get; set; }    
    public Guid BeerBrandId { get; set; }
    public DateTime Date { get; set; }     
    public int Quantity { get; set; }      
    public string Location { get; set; }
    public string BeerBrand { get; set; }
    public DateTime? SalesDate{ get; set; }
}
