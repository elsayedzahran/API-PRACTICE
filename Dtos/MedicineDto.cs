namespace API_Practice.Dtos
{
    public class MedicineDto
    {
        [MaxLength(50)]
        public string Title { get; set; }
        public double Price { get; set; }
        public int Year { get; set; }
        [MaxLength(2500)]
        public string Description { get; set; }
        public IFormFile Poster { get; set; }
        public byte CategotyId { get; set; }
    }
}
 