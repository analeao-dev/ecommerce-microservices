namespace ProductsMicroService.Communication.Responses;

public record ProductResponse(Guid ProductId, string ProductName, decimal UnitPrice, int QuantityInStock, string CategoryName);