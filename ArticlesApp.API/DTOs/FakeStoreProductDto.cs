namespace ArticlesApp.API.DTOs;

/// <summary>
/// Represents a data transfer object for products retrieved from the Fake Store API. This DTO is used to map the product data
/// </summary>
/// <param name="Id"></param>
/// <param name="Title"></param>
/// <param name="Price"></param>
/// <param name="Description"></param>
/// <param name="Category"></param>
/// <param name="Image"></param>
public record FakeStoreProductDto(int Id, string Title, decimal Price, string? Description, string? Category, string? Image);
