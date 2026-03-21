using ArticlesApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ArticlesApp.API.Data;

/// <summary>
/// Represents the application's database context, providing access to the Articles and Products tables.
/// </summary>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<Product> Products => Set<Product>();
}
