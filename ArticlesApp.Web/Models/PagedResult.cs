namespace ArticlesApp.Web.Models;

/// <summary>
/// Represents a paginated result set, containing a list of items and the total count of items available for pagination purposes.
/// </summary>
/// <typeparam name="T"></typeparam>
public class PagedResult<T>
{
    public List<T> Items { get; set; } = [];
    public int TotalCount { get; set; }
}
