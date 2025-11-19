using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace InManSys.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required, StringLength(150)]
        public string ProductName { get; set; } = string.Empty;

        [Required, Precision(18, 2)]
        public decimal UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int ReorderLevel { get; set; }

        // 🔗 Foreign Key
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }   // Navigation property

        // 🔗 Foreign Key
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }

        public Supplier? Supplier { get; set; }   // Navigation property

        // 🖼️ Image Fields
        [Display(Name = "Product Image")]
        public string? ProductImage { get; set; }   // Path saved to DB like "/uploads/img123.jpg"

        [NotMapped]
        public IFormFile? ImageFile { get; set; }   // Used only for upload form
    }
}
