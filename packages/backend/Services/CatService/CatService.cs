using CatMashApi.Data;
using CatMashApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CatMashApi.Services.CatService;

public class CatService : ICatService
{
    private readonly ApiDbContext _context;
    
    public CatService(ApiDbContext context)
    {
        _context = context;
    }

    public async Task<Cat> AddCat(Cat newCat)
    {
        var cat = await _context.Cats.FindAsync(newCat.Id);
        if (cat is null)
        {
            await _context.Cats.AddAsync(newCat);
            await _context.SaveChangesAsync();
        }

        return newCat;
    }

    public async Task<List<Cat>> GetAll()
    {
        return await _context.Cats.ToListAsync();
    }

    public async Task<List<Cat>?> GetPair()
    {
        int catsCount = await _context.Cats.CountAsync();
        if (catsCount < 2)
        {
            return null;
        }

        Random rnd = new Random();
        int random1 = rnd.Next(catsCount-1);
        int random2 = rnd.Next(catsCount-1);
        while (random1 == random2)
            random2 = rnd.Next(catsCount-1);

        var pair = await _context.Cats
            .Where(cat => cat.Sequence == random1 || cat.Sequence == random2)
            .ToListAsync();

        return pair;
    }

    public async Task<Cat?> UpdateCat(Cat newCat)
    {
        var cat = await _context.Cats.FindAsync(newCat.Id);
        if (cat is null)
        {
            return null;
        }

        cat.ExternalId = newCat.ExternalId;
        cat.ImageUrl = newCat.ImageUrl;
        cat.LikeCount = newCat.LikeCount;

        await _context.SaveChangesAsync();

        return cat;
    }
}