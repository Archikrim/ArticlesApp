using Microsoft.EntityFrameworkCore;
using ArticlesApp.API.Data;
using ArticlesApp.API.DTOs;
using ArticlesApp.API.Models;

namespace ArticlesApp.API.Services;

/// <summary>
/// Provides operations for synchronizing and retrieving product data from an external API and the application database.
/// </summary>
/// <param name="httpClient">The HTTP client used to fetch product data from the external API.</param>
/// <param name="context">The database context for accessing and managing product entities.</param>
public class ProductService(HttpClient httpClient, AppDbContext context) : IProductService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly AppDbContext _context = context;

    /// <inheritdoc/>
    public async Task SyncProductsAsync()
    {
        var products = await _httpClient
            .GetFromJsonAsync<List<FakeStoreProductDto>>("https://fakestoreapi.com/products");

        if (products == null) return;

        foreach (var p in products)
        {
            if (!_context.Products.Any(x => x.ExternalId == p.Id))
            {
                _context.Products.Add(new Product
                {
                    ExternalId = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Description = p.Description,
                    Category = p.Category,
                    Image = p.Image
                });
            }
        }

        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<List<Product>> GetAllAsync()
        => await _context.Products.ToListAsync();

    /// <inheritdoc/>
    public async Task<Product?> GetByIdAsync(int id)
        => await _context.Products.FindAsync(id);
}
