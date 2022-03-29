using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceDemo.HelpingModels.ViewModels.Product
{
    public class ProductSubmitViewModel
    {
        public string ProductName { get; set; }
        public string ProductNameAr { get; set; }
        public float Price { get; set; }
        public int Qty { get; set; }
        public string BarCode { get; set; }
        public float SalesTotal { get; set; }
        public float Tax { get; set; }
        public float TaxAmmount { get; set; }
        public float Total { get; set; }
        public float Discount { get; set; }
        public float DiscountAmount { get; set; }
    }
}
