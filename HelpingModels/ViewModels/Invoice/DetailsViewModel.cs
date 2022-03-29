using System;
using System.Collections.Generic;
using InvoiceDemo.HelpingModels.ViewModels.Product;
using InvoiceDemo.Models;

namespace InvoiceDemo.Models.ViewModels.Invoice
{
    public class DetailsViewModel
    {
        public int InvoiceId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNameAr { get; set; }
        public string CompanyRegistrationNumber { get; set; }
        public string CompanyType { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyAddressAr { get; set; }
        public string CompanyTaxActivityCode { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNameAr { get; set; }
        public string CustomerRegistrationNumber { get; set; }
        public string CustomerType { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerAddressAr { get; set; }
        public float Discount { get; set; }
        public List<ProductSubmitViewModel> ProductList { get; set; }
    }
}
