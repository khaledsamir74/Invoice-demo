using InvoiceDemo.Models;
using System.Collections.Generic;

namespace InvoiceDemo.HelpingModels.ViewModels.Error
{
    public class ErrorVM
    {
        public int Id { get; set; }
        public List<ValidationError> validationErrors { get; set; }
    }
}
