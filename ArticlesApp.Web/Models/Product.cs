namespace ArticlesApp.Web.Models;

/// <summary>
/// Represents a product with its details such as title, price, description, category, and image URL.
/// </summary>
public class Product
{
    public int Id { get; set; }
    public int ExternalId { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public string? Image { get; set; }
}
