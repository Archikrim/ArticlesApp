using ArticlesApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArticlesApp.Web.Controllers;

/// <summary>
/// Controller for displaying and searching products using an external API.
/// </summary>
/// <param name="factory"></param>
public class ProductsController(IHttpClientFactory factory) : Controller
{
    private readonly HttpClient _client = factory.CreateClient("api");

    /// <summary>
    /// Retrieves a paginated list of products from the external API and displays them in the view.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<IActionResult> Index(int page = 1, int pageSize = 4)
    {
        var result = await _client.GetFromJsonAsync<PagedResult<Product>>(
            $"api/products/paged?page={page}&pageSize={pageSize}");

        ViewBag.Page = page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)(result?.TotalCount ?? 0) / pageSize);

        return View(result?.Items ?? []);
    }

    /// <summary>
    /// Retrieves details of a specific product by its ID and displays them in the view.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IActionResult> Details(int id)
    {
        var product = await _client.GetFromJsonAsync<Product>($"api/products/{id}");
        return View(product);
    }

    /// <summary>
    /// Triggers a synchronization of products with the external API and redirects back to the index view.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Sync()
    {
        await _client.PostAsync("api/products/sync", null);

        return RedirectToAction("Index");
    }
}
