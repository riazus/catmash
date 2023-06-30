using CatMashApi.Models;

namespace CatMashApi.Services.CatService;

public interface ICatService
{
    Task<List<Cat>> GetAll();
    Task<List<Cat>?> GetPair();
    Task<Cat> AddCat(Cat newCat);
    Task<Cat?> UpdateCat(Cat newCat);
}