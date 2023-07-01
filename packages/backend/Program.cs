using CatMashApi.Models;
using CatMashApi.Data;
using CatMashApi.Services.CatService;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CatMashApi;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("_myAllowSpecificOrigins",
                builder =>
                {
                    builder.WithOrigins("http://localhost:4000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyOrigin();
                });
        });

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApiDbContext>(options =>
            options.UseNpgsql(connectionString));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<ICatService, CatService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApiDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (await context.Cats.CountAsync() == 0)
            {
                using (var client = new HttpClient())
                {
                    string url = "https://latelier.co/data/cats.json";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        CatData catData = JsonConvert.DeserializeObject<CatData>(json)!;

                        List<ImageData> images = catData.Images;
                        List<Cat> cats = new List<Cat>();
                        for (int i = 0; i < images.Count; i++)
                        {
                            Cat newCat = new Cat()
                            {
                                Id = Guid.NewGuid(),
                                Sequence = i,
                                LikeCount = 0,
                                ExternalId = images[i].id,
                                ImageUrl = images[i].url
                            };
                            cats.Add(newCat);
                        }
                        await context.Cats.AddRangeAsync(cats);
                        await context.SaveChangesAsync();

                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                    }
                }
            
            }
        }

        app.UseCors("_myAllowSpecificOrigins");
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
