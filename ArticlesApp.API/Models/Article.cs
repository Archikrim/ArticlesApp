namespace ArticlesApp.API.Models;

/// <summary>
/// Represents an article with its details such as title, content, creation date, publication status, and tag.
/// </summary>
public class Article
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
    public bool IsPublished { get; set; }
    public string Tag { get; set; } = null!;
}
