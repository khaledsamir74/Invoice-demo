using System;
using System.Collections.Generic;

namespace InvoiceDemo.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public float Discount { get; set; }
        public string Status { get; set; }
        public string InternalId { get; set; }
        public DateTime Date { get; set; }
        public List<InvoiceDetails> InvoiceDetails { get; set; }
    }
}
