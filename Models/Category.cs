

using System.ComponentModel.DataAnnotations.Schema;

namespace API_Practice.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
