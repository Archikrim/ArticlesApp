using Microsoft.EntityFrameworkCore;
using ArticlesApp.API.Data;
using ArticlesApp.API.Models;
using ArticlesApp.API.Services;

namespace ArticlesApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>
                     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IArticleService, ArticleService>();
            builder.Services.AddHttpClient<IProductService, ProductService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            ApplyMigrations(app);

            app.Run();
        }

        private static void ApplyMigrations(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();

            if (!db.Articles.Any())
            {
                db.Articles.AddRange(new Article
                {
                    Title = "First Article",
                    Content = "Welcome to First Article",
                    IsPublished = true,
                    Tag = "General",
                },
                new Article
                {
                    Title = "Second Article",
                    Content = "Welcome to Second Article",
                    IsPublished = true,
                    Tag = "General"
                },
                new Article
                {
                    Title = "Third Article",
                    Content = "Welcome to Article With Another Tag",
                    IsPublished = true,
                    Tag = "Another"
                },
                new Article
                {
                    Title = "No Tag Article",
                    Content = "Welcome to No Tag Article",
                    IsPublished = true
                },
                new Article 
                {
                    Title = "Unpublished",
                    Content = "Blah-Blah-Blah",
                    IsPublished = false,
                    Tag = "Draft"
                });
                db.SaveChanges();
            }
        }
    }
}
