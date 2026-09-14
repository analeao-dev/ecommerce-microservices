namespace ProductsMicroService.Communication.Requests;
public record UpdateProductRequest(string ProductName, decimal? UnitPrice, int? QuantityInStock, Guid CategoryId);