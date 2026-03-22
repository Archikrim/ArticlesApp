namespace ArticlesApp.Web.Models;

/// <summary>
/// Represents a product with its details such as title, price, description, category, and image URL.
/// </summary>
public class Product
{
    public int Id { get; set; }
    public int ExternalId { get; set; }
    public string Title { get; set; } = null!;
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Image { get; set; } = null!;
}
