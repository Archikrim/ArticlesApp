using ArticlesApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArticlesApp.Web.Controllers;

/// <summary>
/// Controller for displaying and searching articles using an external API.
/// </summary>
/// <param name="factory">The factory used to create HTTP clients for API communication.</param>
public class ArticlesController(IHttpClientFactory factory) : Controller
{
    private readonly HttpClient _client = factory.CreateClient("api");

    /// <summary>
    /// Displays a paginated list of articles retrieved from the API. The page parameter determines which set of articles to display,
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    public async Task<IActionResult> Index(int page = 1)
    {
        int pageSize = 5;

        var result = await _client.GetFromJsonAsync<PagedResult<Article>>(
            $"api/articles/paged?page={page}&pageSize={pageSize}");

        ViewBag.Page = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalCount = result?.TotalCount ?? 0;

        return View(result?.Items ?? []);
    }

    /// <summary>
    /// Displays the details of the specified article.
    /// </summary>
    /// <param name="id">The identifier of the article to display.</param>
    /// <returns>An asynchronous operation that returns the details view for the article.</returns>
    public async Task<IActionResult> Details(int id)
    {
        var article = await _client.GetFromJsonAsync<Article>($"api/articles/{id}");
        return View(article);
    }

    /// <summary>
    /// Retrieves articles associated with the specified tag and returns them in the Index view.
    /// </summary>
    /// <param name="tag">The tag used to filter articles.</param>
    /// <returns>An asynchronous operation that returns an IActionResult containing the filtered articles.</returns>
    public async Task<IActionResult> ByTag(string tag)
    {
        var articles = await _client.GetFromJsonAsync<List<Article>>($"api/articles/tag/{tag}");
        return View("Index", articles);
    }

    /// <summary>
    /// Searches for articles matching the specified query and returns the results in the Index view.
    /// </summary>
    /// <param name="query">The search term used to filter articles by title.</param>
    /// <returns>An asynchronous operation that returns an IActionResult containing the search results.</returns>
    public async Task<IActionResult> Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return RedirectToAction("Index");
        }

        var response = await _client.GetAsync($"api/articles/search?title={query}");

        if (!response.IsSuccessStatusCode)
        {
            return View("Index", new List<Article>());
        }

        var articles = await response.Content.ReadFromJsonAsync<List<Article>>();

        return View("Index", articles);
    }
}
