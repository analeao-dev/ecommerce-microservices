namespace ProductsMicroService.Core.Dto;

public class ProductDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int QuantityInStock { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}