using CatMashApi.Models;
using CatMashApi.Services.CatService;
using Microsoft.AspNetCore.Mvc;

namespace CatMashApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CatsController : ControllerBase
{
    private readonly ILogger<CatsController> _logger;
    private readonly ICatService _catService;

    public CatsController(
        ILogger<CatsController> logger,
        ICatService catService)
    {
        _logger = logger;
        _catService = catService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Cat>>> GetAll()
    {
        return await _catService.GetAll();
    }

    [HttpGet("pair")]
    public async Task<ActionResult<List<Cat>>> GetPair()
    {
        var result = await _catService.GetPair();
        if (result is null)
        {
            return NotFound("Sorry, but cats count not enough.");
        }

        return result;
    }

    [HttpPost]
    public async Task<ActionResult<Cat>> AddCat(Cat newCat)
    {
        return await _catService.AddCat(newCat);
    }

    [HttpPut]
    public async Task<ActionResult<Cat>> UpdateCat(Cat newCat)
    {
        var result = await _catService.UpdateCat(newCat);

        if (result is null)
        {
            return NotFound("Sorry, but cat doesn't exist.");
        }

        return Ok(result);
    }
}
