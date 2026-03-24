namespace ArticlesApp.API.Models;

/// <summary>
/// Represents an article with its details such as title, content, creation date, publication status, and tag.
/// </summary>
public class Article
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public bool IsPublished { get; set; }
    public string? Tag { get; set; }
}
