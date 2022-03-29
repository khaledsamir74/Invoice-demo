using InvoiceDemo.HelpingModels.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceDemo.HelpingModels.ViewModels.Invoice
{
    public class SubmitViewModel
    {
        public string CustomerId { get; set; }
        public string InvoiceDate { get; set; }
        public List<ProductSubmitViewModel> Products { get; set; }
    }
}
