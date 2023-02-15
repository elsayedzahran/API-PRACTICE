using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace API_Practice.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
    }
}
