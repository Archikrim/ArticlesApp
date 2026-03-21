using ArticlesApp.API.Models;
using ArticlesApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArticlesApp.API.Controllers;

/// <summary>
/// Controller for managing articles in the application, providing endpoints for CRUD operations and article retrieval based on various criteria.
/// </summary>
/// <param name="service"></param>
[ApiController]
[Route("api/[controller]")]
public class ArticlesController(IArticleService service) : ControllerBase
{
    private readonly IArticleService _service = service;

    /// <summary>
    /// Retrieves all items from the service.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IActionResult"/> that,
    /// when successful, includes a collection of all items returned by the service.</returns>
    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _service.GetAllAsync());

    /// <summary>
    /// Retrieves the article with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the article to retrieve.</param>
    /// <returns>An <see cref="IActionResult"/> containing the article if found; otherwise, a NotFound result.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var article = await _service.GetByIdAsync(id);
        return article == null ? NotFound() : Ok(article);
    }

    /// <summary>
    /// Retrieves a collection of items that are associated with the specified tag.
    /// </summary>
    /// <param name="tag">The tag used to filter items. Cannot be null or empty.</param>
    /// <returns>An <see cref="IActionResult"/> containing the items that match the specified tag. Returns an empty collection if
    /// no items are found.</returns>
    [HttpGet("tag/{tag}")]
    public async Task<IActionResult> ByTag(string tag)
        => Ok(await _service.GetByTagAsync(tag));

    /// <summary>
    /// Searches for items that match the specified title.
    /// </summary>
    /// <param name="title">The title or partial title to search for. The search is case-insensitive and may return multiple matching items.
    /// Cannot be null.</param>
    /// <returns>An <see cref="IActionResult"/> containing the search results. Returns an empty collection if no items match the
    /// specified title.</returns>
    [HttpGet("search")]
    public async Task<IActionResult> Search(string title)
        => Ok(await _service.SearchAsync(title));

    /// <summary>
    /// Creates a new article and returns the result of the creation operation.
    /// </summary>
    /// <param name="article">The article to create. Must not be null.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the create operation. Returns a 200 OK response with
    /// the created article if successful.</returns>
    [HttpPost]
    public async Task<IActionResult> Create(Article article)
        => Ok(await _service.CreateAsync(article));

    /// <summary>
    /// Updates the specified article with new values.
    /// </summary>
    /// <param name="article">The article entity containing the updated values. Must not be null.</param>
    /// <returns>A result indicating the outcome of the update operation. Returns a 204 No Content response if the update is
    /// successful.</returns>
    [HttpPut]
    public async Task<IActionResult> Update(Article article)
    {
        await _service.UpdateAsync(article);
        return NoContent();
    }

    /// <summary>
    /// Deletes the resource with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the resource to delete.</param>
    /// <returns>A <see cref="NoContentResult"/> if the resource was successfully deleted.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
