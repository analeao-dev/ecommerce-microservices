namespace ProductsMicroService.Core.Entities;

public class Product
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int QuantityInStock { get; set; }
    public Guid CategoryId { get; set; }
}