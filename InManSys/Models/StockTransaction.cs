using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InManSys.Models
{
    public class StockTransaction
    {
        [Key]
        public int TransactionId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [Required]
        [StringLength(10)]
        public string? Type { get; set; } // IN or OUT

        [Required]
        public int Quantity { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.Now;
    }
}
