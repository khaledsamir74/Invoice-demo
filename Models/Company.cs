using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceDemo.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Company Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Company Name (Arabic)")]
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
        [Required, Display(Name = "Registration Number"), MaxLength(9), MinLength(9)]
        public string RegistrationNumber { get; set; }
        [Required, Display(Name = "Tax Activity Code"), MaxLength(4), MinLength(4)]
        public string TaxActivityCode { get; set; }
        [Required, Display(Name = "Type")]
        public string Type { get; set; }
        [NotMapped]
        public List<Customer> Customers { get; set; }
    }
}
