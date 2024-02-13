namespace Greggs.Products.Core.Models; 

public class Product : ModelBase
{
    public long ProductId { get; set; } 
    public string Name { get; set; }
    public decimal PriceInPounds { get; set; }
}