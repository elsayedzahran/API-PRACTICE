namespace API_Practice.Services
{
    public interface ICategoriesService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(byte id);
        Task<Category> AddAsync(Category category);
        Category Update(Category category);
        Category Delete(Category category); 
        Task<bool> isValidCategory(byte id);
    }
}
