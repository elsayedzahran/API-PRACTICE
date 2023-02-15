using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public MedicinesController(ApplicationDbContext _context) => context = _context;

        new List<string> _allowedExtenstions = new List<string> { ".jpg" , ".png" };
        long _maxAllowedPosterSize = 1048576;


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var medicines = await context.Medicines
                .Include(m => m.category)
                .Select(m => new MedicinesDetailsDto
                {
                    Id = m.Id,
                    CategotyId = m.CategotyId,
                    CategoryName = m.category.Name,
                    Poster = m.Poster,
                    Price = m.Price,
                    Year = m.Year,
                    Description = m.Description,
                    Title = m.Title
                }).ToListAsync();

            return Ok(medicines);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var medicine = await context.Medicines
                .Include(m => m.category)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (medicine == null) 
                return NotFound($"there is no medicine with ID: {id}");

            var dto = new MedicinesDetailsDto
            {
                Id = medicine.Id,
                CategotyId = medicine.CategotyId,
                CategoryName = medicine.category.Name,
                Poster = medicine.Poster,
                Price = medicine.Price,
                Year = medicine.Year,
                Description = medicine.Description,
                Title = medicine.Title
            };
            return Ok(medicine);
        }

        [HttpGet("GetByCategoryId")]
        public async Task<IActionResult> GetByCategoryIdAsync(byte id)
        {
            // return values using category Id
            var medicine = await context.Medicines
                .Where(m => m.CategotyId == id)
                .Include(m => m.category)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (medicine == null) 
                return NotFound($"there is no Category with ID: {id}");

            var dto = new MedicinesDetailsDto
            {
                Id = medicine.Id,
                CategotyId = medicine.CategotyId,
                CategoryName = medicine.category.Name,
                Poster = medicine.Poster,
                Price = medicine.Price,
                Year = medicine.Year,
                Description = medicine.Description,
                Title = medicine.Title
            };

            return Ok(medicine);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromForm]MedicineDto dto)
        {
            if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("only .png .jpg are allowed");

            if (_maxAllowedPosterSize < dto.Poster.Length)
                return BadRequest("max allowed size for poster is 1MB");

            var isValidCategory = await context.Categories.AnyAsync(c => c.Id == dto.CategotyId);

            if (!isValidCategory) 
                return BadRequest("invalid category  id");

            using var datastream = new MemoryStream();
            await dto.Poster.CopyToAsync(datastream);

            var medicine = new Medicine
            {
                Title= dto.Title,
                Price= dto.Price,
                Description= dto.Description,
                Poster= datastream.ToArray(),
                Year= dto.Year,
                CategotyId= dto.CategotyId
            };

            await context.AddAsync(medicine);
            context.SaveChanges();
            return Ok(medicine);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] MedicineDto dto)
        {

            var medicine = await context.Medicines.FindAsync(id);
            if (medicine == null)  
                return NotFound("id not found");

            var isValidCategory = await context.Categories
                .AnyAsync(c => c.Id == dto.CategotyId);

            if (!isValidCategory)
                return BadRequest("invalid category  id");

            if (dto.Poster != null)
            {
                if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("only .png .jpg are allowed");
                if (_maxAllowedPosterSize < dto.Poster.Length)
                    return BadRequest("max allowed size for poster is 1MB");

                using var datastream = new MemoryStream();
                await dto.Poster.CopyToAsync(datastream);

                medicine.Poster = datastream.ToArray();
            }

            medicine.Title = dto.Title;
            medicine.Price = dto.Price;
            medicine.Description = dto.Description;
            medicine.Year = dto.Year;

            context.SaveChanges();

            return Ok(medicine);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            var medicine = await context.Medicines.FindAsync(id);
            if (medicine == null) return NotFound("ID Not found");
            context.Medicines.Remove(medicine);
            context.SaveChanges();
            return Ok(medicine);
        }
    }
}
