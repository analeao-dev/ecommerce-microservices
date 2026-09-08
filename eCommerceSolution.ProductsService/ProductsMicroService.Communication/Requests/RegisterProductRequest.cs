namespace ProductsMicroService.Communication.Requests;

public record RegisterProductRequest(string ProductName, decimal? Price, int? Quantity, Guid CategoryId);