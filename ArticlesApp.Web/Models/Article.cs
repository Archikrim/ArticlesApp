namespace ArticlesApp.Web.Models;

/// <summary>
/// Represents an article with content, metadata, and publication status.
/// </summary>
public class Article
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public bool IsPublished { get; set; }
    public string Tag { get; set; } = null!;
}
