using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceDemo.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        [Required]
        [Display(Name = "Internal Code")]
        public string InternalCode { get; set; }
        [Required]
        [Display(Name = "Bar Code")]
        public string BarCode { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        [Required]
        [Display (Name = "Product Name")]
        public string ProductName { get; set; }
        [Required]
        [Display(Name = "Product Name (Arabic)")]
        public string ProductNameAr { get; set; }
        [Required]
        public double Price { get; set; }
        public bool Discounted { get; set; }
        public List<InvoiceDetails> InvoiceDetails { get; set; }

    }
}
