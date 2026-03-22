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
    /// Retrieves a list of products and displays them in the view.
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Index()
    {
        var products = await _client.GetFromJsonAsync<List<Product>>("api/products");
        return View(products);
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
}
