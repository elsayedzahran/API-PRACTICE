using API_Practice.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceMedicinesController : ControllerBase
    {
        private readonly IMapper mapper; 
        private readonly IMedicinesService medicinesService;
        private readonly ICategoriesService categoryService;
        public ServiceMedicinesController(IMedicinesService medicinesService, ICategoriesService categoryService, IMapper mapper)
        {
            this.medicinesService = medicinesService;
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        new List<string> _allowedExtenstions = new List<string> { ".jpg" , ".png" };
        long _maxAllowedPosterSize = 1048576;


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var medicines = await medicinesService.GetAll();
            var data = mapper.Map<IEnumerable<MedicinesDetailsDto>>(medicines) ;
            
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var medicine = await medicinesService.GetMedicineById(id);

            if (medicine == null) 
                return NotFound($"there is no medicine with ID: {id}");

            var data = mapper.Map<MedicinesDetailsDto>(medicine) ;

            return Ok(data);
        }

        [HttpGet("GetByCategoryId")]
        public async Task<IActionResult> GetByCategoryIdAsync(byte CategoryId)
        {
            var medicine = await medicinesService.GetAll(CategoryId);

            if (medicine == null) 
                return NotFound($"there is no Category with ID: {CategoryId}");

            var data = mapper.Map<MedicinesDetailsDto>(medicine);

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromForm]MedicineDto dto)
        {
            if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("only .png .jpg are allowed");

            if (_maxAllowedPosterSize < dto.Poster.Length)
                return BadRequest("max allowed size for poster is 1MB");

            var isValidCategory = await categoryService.isValidCategory(dto.CategotyId);

            if (!isValidCategory) 
                return BadRequest("category id is invalid");

            using var datastream = new MemoryStream();
            await dto.Poster.CopyToAsync(datastream);

            var medicine = mapper.Map<Medicine>(dto);
            medicine.Poster = datastream.ToArray();

            medicinesService.Add(medicine);

            return Ok(medicine);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] MedicineDto dto)
        {

            var medicine = await medicinesService.GetMedicineById(id);

            if (medicine == null)  
                return NotFound("id not found");

            var isValidCategory = await categoryService.isValidCategory(dto.CategotyId);

            if (!isValidCategory) 
                return BadRequest("category id is invalid");

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

            medicinesService.Update(medicine);

            return Ok(medicine);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            var medicine = await medicinesService.GetMedicineById(id);

            if (medicine == null)
                return NotFound("id not found");
            
            medicinesService.Delete(medicine);

            return Ok(medicine);
        }
    }
}
