using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;


[ApiController]
[Route("api/[controller]")]
public class RateBookController : ControllerBase
{

    private readonly BooksService _booksService;

    public RateBookController(BooksService booksService) =>
        _booksService = booksService;

    [HttpGet]
    public async Task<List<RateBook>> Get() =>
    await _booksService.GetRatingAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<RateBook>> Get(string id)
    {
        var bookRating = await _booksService.GetRatingAsync(id);

        if (bookRating is null)
        {
            return NotFound();
        }

        return bookRating;
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, RateBook updatedRating)
    {
        var bookRating = await _booksService.GetRatingAsync(id);

        if (bookRating is null)
        {
            return NotFound();
        }

        updatedRating.Id = bookRating.Id;

        await _booksService.UpdateRatingAsync(id, updatedRating);

        return NoContent();
    }
}
