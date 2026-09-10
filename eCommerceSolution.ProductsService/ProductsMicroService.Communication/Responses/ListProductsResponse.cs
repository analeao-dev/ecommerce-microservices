namespace ProductsMicroService.Communication.Responses;

public record ListProductsResponse(Guid ProductId, string ProductName, decimal UnitPrice, int QuantityInStock, string CategoryName);