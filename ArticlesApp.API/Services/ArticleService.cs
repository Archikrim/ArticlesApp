using ArticlesApp.API.Data;
using ArticlesApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ArticlesApp.API.Services;

/// <summary>
/// Provides operations for creating, retrieving, updating, and deleting articles in the application data store.
/// </summary>
/// <remarks>This service encapsulates data access logic for articles, enabling asynchronous CRUD operations and
/// search capabilities. It is intended to be used as a dependency in application layers that require article management
/// functionality.</remarks>
/// <param name="context">The database context used to access and manage article entities.</param>
public class ArticleService(AppDbContext context) : IArticleService
{
    private readonly AppDbContext _context = context;

    /// <inheritdoc/>
    public async Task<List<Article>> GetAllAsync()
        => await _context.Articles.ToListAsync();

    /// <inheritdoc/>
    public async Task<Article?> GetByIdAsync(int id)
        => await _context.Articles.FindAsync(id);

    /// <inheritdoc/>
    public async Task<List<Article>> GetByTagAsync(string tag)
        => await _context.Articles
            .Where(a => a.Tag == tag)
            .ToListAsync();

    /// <inheritdoc/>
    public async Task<List<Article>> SearchAsync(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return await _context.Articles.ToListAsync();

        return await _context.Articles
            .Where(a => a.Title.Contains(title))
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<Article> CreateAsync(Article article)
    {
        article.CreatedOn = DateTime.UtcNow;
        _context.Articles.Add(article);
        await _context.SaveChangesAsync();
        return article;
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Article article)
    {
        _context.Articles.Update(article);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Articles.FindAsync(id);
        if (entity != null)
        {
            _context.Articles.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    /// <inheritdoc/>
    public async Task<PagedResult<Article>> GetPagedAsync(int page, int pageSize)
    {
        var query = _context.Articles.AsQueryable();

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Article>
        {
            Items = items,
            TotalCount = totalCount
        };
    }
}
