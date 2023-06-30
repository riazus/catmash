using CatMashApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CatMashApi.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options)
        : base(options)
    {

    }

	public DbSet<Cat>  Cats { get; set; }

}