using ArticlesApp.API.Models;

namespace ArticlesApp.API.Services;

/// <summary>
/// Defines a contract for managing and retrieving articles, including operations to create, update, delete, and query
/// articles asynchronously.
/// </summary>
/// <remarks>Implementations of this interface provide asynchronous methods for accessing and manipulating article
/// data. All methods return tasks to support non-blocking operations, making the interface suitable for use in scalable
/// and responsive applications. Callers should ensure that input parameters meet the documented requirements to avoid
/// exceptions or failed operations.</remarks>
public interface IArticleService
{
    /// <summary>
    /// Asynchronously retrieves all articles from the data source.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of all articles. The list
    /// will be empty if no articles are found.</returns>
    Task<List<Article>> GetAllAsync();

    /// <summary>
    /// Asynchronously retrieves the article with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the article to retrieve.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Article"/> if found;
    /// otherwise, <see langword="null"/>.</returns>
    Task<Article?> GetByIdAsync(int id);

    /// <summary>
    /// Asynchronously retrieves a list of articles that are associated with the specified tag.
    /// </summary>
    /// <param name="tag">The tag used to filter articles. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of articles that have the
    /// specified tag. The list is empty if no articles are found.</returns>
    Task<List<Article>> GetByTagAsync(string tag);

    /// <summary>
    /// Asynchronously searches for articles with titles that match the specified search term.
    /// </summary>
    /// <param name="title">The search term to use for matching article titles. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of articles whose titles
    /// match the search term. The list is empty if no matching articles are found.</returns>
    Task<List<Article>> SearchAsync(string title);

    /// <summary>
    /// Asynchronously creates a new article and returns the created instance.
    /// </summary>
    /// <param name="article">The article to create. Must not be null. The article's properties should be set to valid values before calling
    /// this method.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created article, including any
    /// server-assigned values such as identifiers.</returns>
    Task<Article> CreateAsync(Article article);

    /// <summary>
    /// Asynchronously updates the specified article in the data store.
    /// </summary>
    /// <param name="article">The article to update. Cannot be null. The article must have a valid identifier corresponding to an existing
    /// record.</param>
    /// <returns>A task that represents the asynchronous update operation.</returns>
    Task UpdateAsync(Article article);

    /// <summary>
    /// Asynchronously deletes the entity with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    Task DeleteAsync(int id);
}
