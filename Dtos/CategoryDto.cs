namespace API_Practice.Dtos
{
    public class CategoryDto
    {
        [MaxLength(50)]
        public string name { get; set; }
    }
}
