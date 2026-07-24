using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Models;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public AuthorsController(ApplicationDbContext db) => _db = db;

    [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _db.Authors.ToListAsync());

    [HttpGet("{id}")] public async Task<IActionResult> GetById(int id) => Ok(await _db.Authors.FindAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create(Author a)
    {
        _db.Authors.Add(a);
        await _db.SaveChangesAsync();
        return Ok(a);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Author a)
    {
        _db.Entry(a).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var a = await _db.Authors.FindAsync(id);
        _db.Authors.Remove(a!);
        await _db.SaveChangesAsync();
        return Ok();
    }

    // ВРЪЗКАТА (Many-to-Many)
    [HttpPost("{aId}/link/{bId}")]
    public async Task<IActionResult> Link(int aId, int bId)
    {
        var a = await _db.Authors.Include(x => x.Books).FirstAsync(x => x.Id == aId);
        var b = await _db.Books.FindAsync(bId);
        a.Books.Add(b!);
        await _db.SaveChangesAsync();
        return Ok();
    }
}
