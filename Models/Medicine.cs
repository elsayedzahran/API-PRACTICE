using System.ComponentModel.DataAnnotations.Schema;

namespace API_Practice.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        public double Price { get; set; }
        public int Year { get; set; }
        [MaxLength(2500)]
        public string Description { get; set; }
        public byte[] Poster { get; set; }
        public byte CategotyId { get; set; }
        [ForeignKey(nameof(CategotyId))]
        public Category category { get; set; }
    }
}
