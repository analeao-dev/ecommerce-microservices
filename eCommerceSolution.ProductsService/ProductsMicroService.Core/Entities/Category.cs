namespace ProductsMicroService.Core.Entities;

public class Category
{
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }  = string.Empty;
    public bool IsActive { get; set; }
}