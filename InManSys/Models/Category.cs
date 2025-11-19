using System.ComponentModel.DataAnnotations;

namespace InManSys.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string? CategoryName { get; set; }
    }
}
