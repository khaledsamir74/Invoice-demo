using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceDemo.Models.ViewModels.Customer
{
    public class CreateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string CountryAr { get; set; }
        public int Number { get; set; }
    }
}
