using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceDemo.Models
{
    public class ValidationError
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public string Message { get; set; }
    }
}
