

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public CategoriesController(ApplicationDbContext _context) => context = _context; 

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await context.Categories.ToListAsync();
            return Ok(categories);
        }
        [HttpPost]
        public async Task<IActionResult> AddAsync(CategoryDto dto)
        {
            var category = new Category { Name = dto.name };
            await context.Categories.AddAsync(category);
            context.SaveChanges();
            return Ok(category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] CategoryDto dto)
        {
            var category = await context.Categories.SingleOrDefaultAsync( c => c.Id == id);
            if (category == null)  
                return NotFound($"No category was found with ID: {id}");
            category.Name = dto.name;
            context.SaveChanges();
            return Ok(category);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var category = await context.Categories.SingleOrDefaultAsync(c => c.Id == id);
            if (category == null)
                return NotFound($"No category was found with ID: {id}");
            context.Categories.Remove(category);
            context.SaveChanges();
            return Ok(category);
        }
    }
}
