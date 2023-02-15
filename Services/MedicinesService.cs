using API_Practice.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Practice.Services
{
    public class MedicinesService : IMedicinesService
    {
        private readonly ApplicationDbContext context;
        public MedicinesService(ApplicationDbContext _context) => context = _context;

        public async Task<IEnumerable<Medicine>> GetAll(byte CategoryId = 0)
        {
            var medicines = await context.Medicines
                .Where( m => m.CategotyId == CategoryId || CategoryId == 0 )
                .Include(m => m.category).ToListAsync();
            return medicines;
        }

        public async Task<Medicine> GetMedicineById(int id)
        {
            var medicine = await context.Medicines.Include( m => m.category ).SingleOrDefaultAsync( m => m.Id == id);
            return medicine;
        }
        public async Task<Medicine> Add(Medicine medicine)
        {
            await context.Medicines.AddAsync(medicine);
            return medicine;
        }

        public Medicine Update(Medicine medicine)
        {
            context.Update(medicine);
            context.SaveChanges();
            return medicine;

        }

        public Medicine Delete(Medicine medicine)
        {
            context.Remove(medicine);
            context.SaveChanges();
            return medicine;
        }

    }
}
