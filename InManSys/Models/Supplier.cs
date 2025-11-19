using System.ComponentModel.DataAnnotations;

namespace InManSys.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Required]
        [StringLength(100)]
        public string? SupplierName { get; set; }

        [StringLength(15)]
        public string? ContactNo { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }
    }
}
