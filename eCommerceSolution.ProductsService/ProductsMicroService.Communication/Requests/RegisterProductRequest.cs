namespace ProductsMicroService.Communication.Requests;

public record RegisterProductRequest(string ProductName, decimal? UnitPrice, int? QuantityInStock, Guid CategoryId);