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

        var existingIds = await _context.Products
            .Select(x => x.ExternalId)
            .ToListAsync();

        var newProducts = products
            .Where(p => !existingIds.Contains(p.Id))
            .Select(p => new Product
            {
                ExternalId = p.Id,
                Title = p.Title ?? "No title",
                Price = p.Price,
                Description = p.Description,
                Category = p.Category,
                Image = p.Image
            })
            .ToList();

        if (newProducts.Count != 0)
        {
            _context.Products.AddRange(newProducts);
            await _context.SaveChangesAsync();
        }
    }

    /// <inheritdoc/>
    public async Task<List<Product>> GetAllAsync()
        => await _context.Products.ToListAsync();

    /// <inheritdoc/>
    public async Task<Product?> GetByIdAsync(int id)
        => await _context.Products.FindAsync(id);

    /// <inheritdoc/>
    public async Task<PagedResult<Product>> GetPagedAsync(int page, int pageSize)
    {
        if (page < 1) page = 1;

        var query = _context.Products.AsQueryable();
        var totalCount = await query.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        if (page > totalPages)
            page = totalPages == 0 ? 1 : totalPages;

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Product>
        {
            Items = items,
            TotalCount = totalCount
        };
    }
}
