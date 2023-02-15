using Microsoft.EntityFrameworkCore;

namespace API_Practice.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext context;
        public CategoriesService(ApplicationDbContext _context) => context = _context;
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var categories = await context.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetByIdAsync(byte id)
        {
            var category = await context.Categories.SingleOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<Category> AddAsync(Category category)
        {
            await context.Categories.AddAsync(category);
            context.SaveChanges();

            return category;
        }

        public Category Update(Category category)
        {
            context.Update(category);
            context.SaveChanges();
            return category;
        }

        public Category Delete(Category category)
        {
            context.Remove(category);
            context.SaveChanges();
            return category;
        }

        public Task<bool> isValidCategory(byte id)
        {
            return context.Categories.AnyAsync(c => c.Id == id);
        }
    }
}
