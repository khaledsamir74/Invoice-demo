using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvoiceDemo.Models
{
    public class Customer
    {
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Customer Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Customer Name (Arabic)")]
        public string NameAr { get; set; }
        [Required, Display(Name = "Building Number")]
        public int BuildingNo { get; set; }
        [Required, Display(Name = "Street Name")]
        public string Street { get; set; }
        [Required, Display(Name = "Street Name (arabic)")]
        public string StreetAr { get; set; }
        [Required, Display(Name = "City")]
        public string City { get; set; }
        [Required, Display(Name = "City (arabic)")]
        public string CityAr { get; set; }
        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Required]
        [Display(Name = "Country (Arabic)")]
        public string CountryAr { get; set; }
        [Required]
        public string Number { get; set; }
        [Required, Display(Name ="Registration Number"), MaxLength(9), MinLength(9)]
        public string RegistrationNumber { get; set; }
        [Required, Display(Name = "Type")]
        public string Type { get; set; }


        public List<Invoice> Invoices { get; set; }
    }
}
