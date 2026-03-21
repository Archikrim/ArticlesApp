using ArticlesApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ArticlesApp.API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Article> Articles => Set<Article>();
        public DbSet<Product> Products => Set<Product>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {}
    }
}
