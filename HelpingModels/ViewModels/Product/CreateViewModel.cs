namespace InvoiceDemo.Models.ViewModels.Product
{
    public class CreateViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductNameAr { get; set; }
        public double Price { get; set; }
        public bool Discounted { get; set; }
    }
}
