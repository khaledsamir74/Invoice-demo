using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceDemo.Models
{
    public class InvoiceDetails
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public float Tax { get; set; }
        public float Discount { get; set; }
        public Product Product { get; set; }
        public Invoice Invoice { get; set; }

    }
}
