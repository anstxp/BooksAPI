using BooksAPI.Clients;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class GoogleBooksController : Controller
{
    private readonly Client _client;

    public GoogleBooksController(Client client)
    {
        _client = client;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks(string name)
    {
        var books = await _client.GetBooksAsync(name);
        if (books.Any())
            return Ok(books);
        return NotFound();
    }
}