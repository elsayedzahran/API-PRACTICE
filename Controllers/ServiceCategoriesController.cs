

using API_Practice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCategoriesController : ControllerBase
    {
        private readonly ICategoriesService categoryService;
        public ServiceCategoriesController(ICategoriesService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(CategoryDto dto)
        {

            var category = new Category { Name = dto.name };
            await categoryService.AddAsync(category);

            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(byte id, [FromBody] CategoryDto dto)
        {
            var category = await categoryService.GetByIdAsync(id);

            if (category == null)  
                return NotFound($"No category was found with ID: {id}");
            category.Name = dto.name;

            categoryService.Update(category);
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var category = await categoryService.GetByIdAsync(id);

            if (category == null)
                return NotFound($"No category was found with ID: {id}");

            categoryService.Delete(category);
            return Ok(category);
        }
    }
}
