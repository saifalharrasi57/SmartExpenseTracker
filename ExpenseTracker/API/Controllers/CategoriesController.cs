using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.DTO;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
//ControllerBase: Provides core API methods like Ok(), NotFound(), and CreatedAtAction().
public class CategoriesController : ControllerBase
{

    private readonly ApplicationDbContext Context;

    public CategoriesController(ApplicationDbContext _context)
    {
        Context = _context;
    }

    // 'async': Tells C# this method contains background work and returns a Task.
// 'await': Pauses execution of this method until the database task completes,
//          freeing up the web server thread to handle other incoming HTTP requests.

// note that whenever using Async methods it's associated with await 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        var Categories = await Context.Categories.ToListAsync();
        return Ok(Categories);
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<Category>>> PostCategory(CategoryDTO Dto)
    {
        var C = new Category();
        C.Name = Dto.Name;
        // Marks this as a user-created category so the system knows it can be edited or deleted later
        // (unlike system default categories which are protected)
        C.IsSystemDefault = false;
        Context.Categories.Add(C);
        await Context.SaveChangesAsync();
        return Ok("new Catgory added");
    }


    [HttpGet("get Category by id")]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategoryById(int Id)
    {
        Category b = await Context.Categories.FirstOrDefaultAsync(c => c.Id == Id);
        if (b == null)
        {
            return NotFound();
        }

        return Ok(b);

    }
    
    
    
}
