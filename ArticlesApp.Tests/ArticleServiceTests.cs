using Microsoft.EntityFrameworkCore;
using ArticlesApp.API.Data;
using ArticlesApp.API.Models;
using ArticlesApp.API.Services;

namespace ArticlesApp.Tests;

/// <summary>
/// Unit tests for the <see cref="ArticleService"/> class, covering various scenarios for retrieving and searching articles based on tags, titles, and pagination. 
/// These tests ensure that the service methods return expected results under different conditions, including edge cases such as empty results and page numbers that exceed available data. 
/// The tests utilize an in-memory database to isolate test cases and provide a consistent testing environment.
/// </summary>
public class ArticleServiceTests
{
    [Fact]
    public async Task GetByTagAsync_ReturnsExpectedResult()
    {
        var context = GetDbContext();

        context.Articles.AddRange(
            new Article 
            { 
                Title = "A1", 
                Tag = "Tech",
                IsPublished = true
            },
            new Article 
            { 
                Title = "A2", 
                Tag = "News", 
                IsPublished = true
            }
        );

        await context.SaveChangesAsync();

        var service = new ArticleService(context);

        var result = await service.GetByTagAsync("Tech");

        Assert.Single(result);
    }

    [Fact]
    public async Task SearchAsync_ReturnsExpectedResult()
    {
        var context = GetDbContext();

        context.Articles.AddRange(
            new Article { Title = "Dependency Injection in C#", IsPublished = true },
            new Article { Title = "Clean Code", IsPublished = true }
        );

        await context.SaveChangesAsync();

        var service = new ArticleService(context);

        var result = await service.SearchAsync("Dependency");

        Assert.Single(result);
    }

    [Fact]
    public async Task GetPagedAsync_ReturnsCorrectPage()
    {
        var context = GetDbContext();

        for (int i = 1; i <= 20; i++)
        {
            context.Articles.Add(new Article { Title = $"Article {i}", IsPublished = true });
        }

        await context.SaveChangesAsync();

        var service = new ArticleService(context);

        var result = await service.GetPagedAsync(2, 5);

        Assert.Equal(5, result.Items.Count);
        Assert.Equal("Article 6", result.Items.First().Title);
    }

    [Fact]
    public async Task GetPagedAsync_PageTooBig_ReturnsLastPage()
    {
        var context = GetDbContext();

        for (int i = 1; i <= 3; i++)
        {
            context.Articles.Add(new Article { Title = $"Article {i}", IsPublished = true });
        }

        await context.SaveChangesAsync();

        var service = new ArticleService(context);

        var result = await service.GetPagedAsync(10, 2);

        Assert.NotEmpty(result.Items);
    }

    [Fact]
    public async Task SearchAsync_NoMatches_ReturnsEmpty()
    {
        var context = GetDbContext();

        context.Articles.Add(new Article { Title = "Test", IsPublished = true });
        await context.SaveChangesAsync();

        var service = new ArticleService(context);

        var result = await service.SearchAsync("XYZ");

        Assert.Empty(result);
    }

    [Fact]
    public async Task SearchAsync_NotPublished_ReturnsEmpty()
    {
        var context = GetDbContext();

        context.Articles.Add(new Article { Title = "Test", IsPublished = false});
        await context.SaveChangesAsync();

        var service = new ArticleService(context);

        var result = await service.SearchAsync("Test");

        Assert.Empty(result);
    }

    private static AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

}
