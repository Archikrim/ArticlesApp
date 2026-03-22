namespace ArticlesApp.API.DTOs;

/// <summary>
/// Represents a product in the Fake Store API data transfer object format.
/// </summary>
public class FakeStoreProductDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Image { get; set; } = null!;
}
