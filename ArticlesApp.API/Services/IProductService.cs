using ArticlesApp.API.Models;

namespace ArticlesApp.API.Services;

/// <summary>
/// Defines operations for synchronizing and retrieving product data.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Synchronizes product data with the external data source asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SyncProductsAsync();

    /// <summary>
    /// Asynchronously retrieves all products.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of products.</returns>
    Task<List<Product>> GetAllAsync();

    /// <summary>
    /// Asynchronously retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the product if found; otherwise,
    /// null.</returns>
    Task<Product?> GetByIdAsync(int id);
}
