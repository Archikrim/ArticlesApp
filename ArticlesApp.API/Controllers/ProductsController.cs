using ArticlesApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArticlesApp.API.Controllers;

/// <summary>
/// API controller for managing product operations.
/// </summary>
/// <param name="service">Service for handling product-related business logic.</param>
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService service) : ControllerBase
{
    private readonly IProductService _service = service;

    /// <summary>
    /// Synchronizes product data asynchronously.
    /// </summary>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
    [HttpPost("sync")]
    public async Task<IActionResult> Sync()
    {
        await _service.SyncProductsAsync();
        return Ok();
    }

    /// <summary>
    /// Retrieves all entities.
    /// </summary>
    /// <returns>An IActionResult containing the collection of entities.</returns>
    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _service.GetAllAsync());

    /// <summary>
    /// Retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product.</param>
    /// <returns>An IActionResult containing the product if found; otherwise, a NotFound result.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await _service.GetByIdAsync(id);
        return product == null ? NotFound() : Ok(product);
    }

    /// <summary>
    /// Retrieves a paginated list of products based on the specified page number and page size. The default page number is 1 and the default page size is 5.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet("paged")]
    public async Task<IActionResult> GetPaged(int page = 1, int pageSize = 5)
    {
        return Ok(await _service.GetPagedAsync(page, pageSize));
    }
}
