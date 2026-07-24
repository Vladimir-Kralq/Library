using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public BooksController(ApplicationDbContext db) => _db = db;

    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _db.Books.ToListAsync());

    [HttpGet("{id}")] public async Task<IActionResult> GetById(int id) => Ok(await _db.Books.FindAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create(Book b)
    {
        _db.Books.Add(b);
        await _db.SaveChangesAsync();
        return Ok(b);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Book b)
    {
        _db.Entry(b).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var b = await _db.Books.FindAsync(id);
        _db.Books.Remove(b!);
        await _db.SaveChangesAsync();
        return Ok();
    }
}
