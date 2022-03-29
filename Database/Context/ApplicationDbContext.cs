using Microsoft.EntityFrameworkCore;
using InvoiceDemo.Models.ViewModels.Customer;
using InvoiceDemo.HelpingModels.ViewModels.Error;

namespace InvoiceDemo.Models.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<InvoiceDetails> InvoiceDetails { get; set; }
        public DbSet<InvoiceDemo.Models.ViewModels.Customer.CreateViewModel> CreateViewModel { get; set; }
        public DbSet<InvoiceDemo.HelpingModels.ViewModels.Error.ErrorVM> ErrorViewModel { get; set; }

    }
}
