using InvoiceDemo.HelpingModels.ViewModels.Error;
using InvoiceDemo.HelpingModels.ViewModels.Invoice;
using InvoiceDemo.HelpingModels.ViewModels.Product;
using InvoiceDemo.HelperFunctions;
using InvoiceDemo.Models;
using InvoiceDemo.Models.Context;
using InvoiceDemo.Models.ViewModels.Invoice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.OData.Edm;
using NPOI.SS.Util;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDemo.Controllers
{
    public class InvoiceController : Controller
    {
        int internalId = 267;
        private readonly ApplicationDbContext context;

        public InvoiceController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET: InvoiceController
        public ActionResult Index()
        {
            return View(context.Invoices.ToList());
        }

        // GET: InvoiceController/Details/5
        public ActionResult Details(int id)
        {
            float TotalNet = 0;
            double TotalTaxAmount = 0;
            float FinalTotal = 0;


            DetailsViewModel detailsViewModel = new DetailsViewModel();

            var Invoice = context.Invoices.Find(id);
            var Company = context.Companies.Find(1);
            var Customer = context.Customers.Find(Invoice.CustomerId);
            var InvoiceDetailsList = context.InvoiceDetails
                .Where(x => x.InvoiceId == Invoice.Id);
            detailsViewModel.InvoiceId = int.Parse(Invoice.InternalId);
            detailsViewModel.SubmissionDate = Invoice.Date;
            detailsViewModel.CustomerId = Customer.Id;
            detailsViewModel.CustomerName = Customer.Name;
            detailsViewModel.CustomerNameAr = Customer.NameAr;
            detailsViewModel.CustomerRegistrationNumber = Customer.RegistrationNumber;
            detailsViewModel.CustomerType = Customer.Type;
            detailsViewModel.CustomerAddress = Customer.BuildingNo + ", " + Customer.Street + ", " + Customer.City + ", " + Customer.Country;
            detailsViewModel.CustomerAddressAr = Customer.BuildingNo + ", " + Customer.StreetAr + " st., " + Customer.CityAr + ", " + Customer.CountryAr;
            detailsViewModel.CompanyId = Company.Id;
            detailsViewModel.CompanyName = Company.Name;
            detailsViewModel.CompanyNameAr = Company.NameAr;
            detailsViewModel.CompanyRegistrationNumber = Company.RegistrationNumber;
            detailsViewModel.CompanyType = Company.Type;
            detailsViewModel.CompanyTaxActivityCode = Company.TaxActivityCode;
            detailsViewModel.CompanyAddress = Company.BuildingNo + ", " + Company.Street + ", " + Company.City + ", " + Company.Country;
            detailsViewModel.CompanyAddressAr = Company.BuildingNo + ", " + Company.StreetAr + " st., " + Company.CityAr + ", " + Company.CountryAr;
            detailsViewModel.Discount = Invoice.Discount;
            detailsViewModel.ProductList = InvoiceDetailsList.ToList().Select(x => new ProductSubmitViewModel
            {
                ProductName = context.Products.Single(p => p.Id == x.ProductId).ProductName,
                ProductNameAr = context.Products.Single(p => p.Id == x.ProductId).ProductNameAr,
                Tax = x.Tax,
                Price = x.Price,
                Qty = x.Quantity,
                TaxAmmount = (x.Tax / 100) * (x.Price * x.Quantity),
                SalesTotal = x.Price * x.Quantity,
                Total = (x.Price * x.Quantity) + ((x.Tax / 100) * (x.Price * x.Quantity)), //(x.Price + ((x.Tax / 100) * x.Price)) * x.Quantity,
                BarCode = context.Products.Single(p => p.Id == x.ProductId).BarCode

            }).ToList();
            foreach (var pro in detailsViewModel.ProductList)
            {
                TotalNet += pro.SalesTotal;
                TotalTaxAmount += pro.TaxAmmount;
                FinalTotal += pro.Total;
            }
            TotalTaxAmount = Math.Round(TotalTaxAmount, 2);
            ViewBag.TotalNet = TotalNet;
            ViewBag.TotalTaxAmount = TotalTaxAmount;
            FinalTotal -= Invoice.Discount;
            ViewBag.FinalTotal = TotalNet + TotalTaxAmount;
            ViewBag.GrandTotal = FinalTotal;
            return View(detailsViewModel);
        }

        // GET: InvoiceController/Create
        public ActionResult Create()
        {
            ViewBag.customers = context.Customers.ToList().Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = x.Name
            }).ToList();

            if (context.Invoices.ToList().Any())
            {
                var lastInvoice = context.Invoices.OrderBy(x => x.Id).Last();
                if (lastInvoice is not null) ViewBag.lastInv = lastInvoice.Id + 1;
            }
            else
            {
                ViewBag.lastInv = 1;
            }
            return View();
        }

        // POST: InvoiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InvoiceController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var invoice = context.Invoices.Find(id);
            var products = context.Products.ToList();
            ViewBag.products = products;
            if (invoice is not null)
            {
                var customer = context.Customers.Find(invoice.CustomerId);
                if (customer is not null) ViewBag.CustomerName = customer.Name;
                else ViewBag.CustomerName = "Not Selected Customer";
                ViewBag.InvoiceId = invoice.Id;
                ViewBag.InvoiceDate = invoice.Date;
                ViewBag.Discount = invoice.Discount;

                var InvoiceDetails = context.InvoiceDetails.Where(x => x.InvoiceId == invoice.Id).ToList();
                var InvoiceProducts = new List<ProductSubmitViewModel>();
                float TotalTaxAmmount = 0;
                float TotalSales = 0;
                float TotalGrand = 0;

                foreach (var item in InvoiceDetails)
                {
                    var product = context.Products.Find(item.ProductId);
                    float SalesTotal = item.Quantity * (float)product.Price;
                    float TaxAmmount = item.Tax / 100 * SalesTotal;
                    float Total = SalesTotal + TaxAmmount;
                    TotalSales += SalesTotal;
                    TotalTaxAmmount += TaxAmmount;
                    TotalGrand += Total;
                    InvoiceProducts.Add(new ProductSubmitViewModel
                    {
                        ProductName = product.ProductName,
                        ProductNameAr = product.ProductNameAr,
                        Price = (float)product.Price,
                        Qty = item.Quantity,
                        SalesTotal = SalesTotal,
                        Tax = item.Tax,
                        TaxAmmount = TaxAmmount,
                        Total = Total
                    });
                };
                ViewBag.InvoiceProducts = InvoiceProducts;
                ViewBag.TotalSales = TotalSales;
                ViewBag.TotalTaxAmmount = TotalTaxAmmount;
                ViewBag.TotalGrand = TotalGrand - invoice.Discount;
            }
            return View();
        }

        // POST: InvoiceController/Edit/5
        [HttpPost]
        public ActionResult Update(int invID,
            float Discount, List<ProductSubmitViewModel> Products)
        {
            var invoice = context.Invoices.Find(invID);
            invoice.Discount = Discount;

            context.Invoices.Update(invoice);
            context.SaveChanges();

            var InvoiceDetail = context.InvoiceDetails.Where(x => x.InvoiceId == invoice.Id).ToList();
            context.InvoiceDetails.RemoveRange(InvoiceDetail);
            context.SaveChanges();

            var InvoiceDetailTemp = new List<InvoiceDetails>();
            foreach (var item in Products)
            {
                InvoiceDetailTemp.Add(new InvoiceDetails
                {
                    InvoiceId = invoice.Id,
                    ProductId = context.Products.Single(x => x.ProductName == item.ProductName).Id,
                    Quantity = item.Qty,
                    Price = item.Price,
                    Tax = item.Tax,
                });

            }
            context.InvoiceDetails.AddRange(InvoiceDetailTemp);
            context.SaveChanges();

            foreach (var item in InvoiceDetailTemp)
            {
                var product = context.Products.Find(item.ProductId);
                product.Price = item.Price;
                context.Products.Update(product);
                context.SaveChanges();
            }
            return Json("dd");
        }

        // GET: InvoiceController/Delete/5
        public ActionResult Delete(int id)
        {
            float TotalNet = 0;
            double TotalTaxAmount = 0;
            float FinalTotal = 0;

            DetailsViewModel detailsViewModel = new();
            var Invoice = context.Invoices.Find(id);
            var Company = context.Companies.Find(1);
            var Customer = context.Customers.Find(Invoice.CustomerId);
            var InvoiceDetailsList = context.InvoiceDetails.Where(x => x.InvoiceId == id);
            detailsViewModel.InvoiceId = id;
            detailsViewModel.CustomerId = Customer.Id;
            detailsViewModel.CustomerName = Customer.Name;
            detailsViewModel.CustomerNameAr = Customer.NameAr;
            detailsViewModel.CustomerRegistrationNumber = Customer.RegistrationNumber;
            detailsViewModel.CustomerType = Customer.Type;
            detailsViewModel.CustomerAddress = Customer.BuildingNo + ", " + Customer.Street + ", " + Customer.City + ", " + Customer.Country;
            detailsViewModel.CustomerAddressAr = Customer.BuildingNo + ", " + Customer.StreetAr + " st., " + Customer.CityAr + ", " + Customer.CountryAr;
            detailsViewModel.CompanyId = Company.Id;
            detailsViewModel.CompanyName = Company.Name;
            detailsViewModel.CompanyNameAr = Company.NameAr;
            detailsViewModel.CompanyRegistrationNumber = Company.RegistrationNumber;
            detailsViewModel.CompanyType = Company.Type;
            detailsViewModel.CompanyTaxActivityCode = Company.TaxActivityCode;
            detailsViewModel.CompanyAddress = Company.BuildingNo + ", " + Company.Street + ", " + Company.City + ", " + Company.Country;
            detailsViewModel.CompanyAddressAr = Company.BuildingNo + ", " + Company.StreetAr + " st., " + Company.CityAr + ", " + Company.CountryAr;
            detailsViewModel.Discount = Invoice.Discount;
            detailsViewModel.ProductList = InvoiceDetailsList.ToList().Select(x => new ProductSubmitViewModel
            {
                ProductName = context.Products.Single(p => p.Id == x.ProductId).ProductName,
                ProductNameAr = context.Products.Single(p => p.Id == x.ProductId).ProductNameAr,
                Tax = x.Tax,
                Price = x.Price,
                Qty = x.Quantity,
                TaxAmmount = (x.Tax / 100) * (x.Price * x.Quantity),
                SalesTotal = x.Price * x.Quantity,
                Total = (x.Price * x.Quantity) + ((x.Tax / 100) * (x.Price * x.Quantity)) //(x.Price + ((x.Tax / 100) * x.Price)) * x.Quantity,

            }).ToList();
            foreach (var pro in detailsViewModel.ProductList)
            {
                TotalNet += pro.SalesTotal;
                TotalTaxAmount += pro.TaxAmmount;
                FinalTotal += pro.Total;
            }
            TotalTaxAmount = Math.Round(TotalTaxAmount, 2);
            ViewBag.TotalNet = TotalNet;
            ViewBag.TotalTaxAmount = TotalTaxAmount;
            FinalTotal -= Invoice.Discount;
            ViewBag.FinalTotal = TotalNet + TotalTaxAmount;
            ViewBag.GrandTotal = FinalTotal;
            return View(detailsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var invoice = context.Invoices.Find(id);
                if (invoice is not null)
                {
                    context.Invoices.Remove(invoice);
                    context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // submit new Invoice
        [HttpPost]
        public ActionResult Submit(string CustomerId, string Date,
            float Discount, List<ProductSubmitViewModel> Products)
        {
            internalId++;
            var date = DateTime.Now;
            if (Date is not null || Date != "")
                date = DateTime.ParseExact(Date, "M/d/yyyy", CultureInfo.InvariantCulture);
            var CustID = context.Customers.Single(x => x.Name == CustomerId);
            var invoice = new Invoice
            {
                CustomerId = CustID.Id,
                Date = date,
                Discount = Discount,
                Status = "InValid",
                InternalId = internalId.ToString()
            };
            context.Invoices.Add(invoice);
            context.SaveChanges();

            var LastInvoice = context.Invoices.OrderBy(x => x.Id).Last();
            var InvoiceDetail = new List<InvoiceDetails>();

            foreach (var item in Products)
            {
                InvoiceDetail.Add(new InvoiceDetails
                {
                    InvoiceId = LastInvoice.Id,
                    ProductId = context.Products.Single(x => x.ProductName == item.ProductName).Id,
                    Quantity = item.Qty,
                    Price = item.Price,
                    Tax = (item.Tax),
                });

            }
            context.InvoiceDetails.AddRange(InvoiceDetail);
            context.SaveChanges();

            foreach (var item in InvoiceDetail)
            {
                var product = context.Products.Find(item.ProductId);
                product.Price = item.Price;
                context.Products.Update(product);
                context.SaveChanges();
            }
            return Json(new { CustomerId, Date, Products });
        }
        [HttpGet]
        public IActionResult Error()
        {
            var model = new ErrorVM();
            model.validationErrors = new List<ValidationError>();
            return View(model);
        }
        // Import invoice from excel file
        [HttpPost]
        public ActionResult Import(IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file?.Length > 0)
                {
                    // convert to stream
                    var stream = file.OpenReadStream();
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                        var worksheet = package.Workbook.Worksheets.First();
                        var RowCount = worksheet.Dimension.Rows;
                        var ColCount = worksheet.Dimension.Columns;
                        var Errors = new List<ValidationError>();

                        // types validation
                        Errors = TypesValidations(worksheet, RowCount, Errors);
                        Errors.AddRange(TotalPerRowValidations(worksheet, RowCount, Errors));
                        Errors.AddRange(InvoiceTotalValidations(worksheet, RowCount, Errors));
                        if (Errors.Any())
                        {
                            return View("Error", new ErrorVM
                            {
                                validationErrors = Errors
                            });
                        }

                            // loop throw each row in file
                            var InvoiceDetailList = new List<InvoiceDetails>();
                            var InvoiceIDict = new Dictionary<string, int>();
                            ProcessExcelSheetRows(worksheet, RowCount, InvoiceDetailList, InvoiceIDict);
                            // seting invoice details
                            SettingInvoiceDetails(InvoiceDetailList, InvoiceIDict);
                    }
                }

            }
            return RedirectToAction("Index");
        }

        public Product ProcessInvoiceItem(ExcelWorksheet worksheet, int row)
        {
            var product = context.Products
                                .SingleOrDefault(x => x.BarCode ==
                                    worksheet.Cells[row, 21].Value.ToString());
            if (product is not null)
            {
                product.BarCode = worksheet.Cells[row, 21].Value?.ToString();
                product.Description = worksheet.Cells[row, 19].Value?.ToString();
                product.ModifiedDate = DateTime.Now;
                product.InternalCode = worksheet.Cells[row, 24].Value?.ToString();
                product.ProductName = worksheet.Cells[row, 20].Value?.ToString();
                product.ProductNameAr = worksheet.Cells[row, 20].Value?.ToString();
                product.Price = double.Parse(worksheet.Cells[row, 29].Value?.ToString());
                product.Unit = worksheet.Cells[row, 22].Value?.ToString();
                context.Products.Update(product);
                context.SaveChanges();
            }
            else if (product is null)
            {
                var newProdcut = new Product
                {
                    BarCode = worksheet.Cells[row, 21].Value?.ToString(),
                    Description = worksheet.Cells[row, 19].Value?.ToString(),
                    CompanyId = 1,
                    CreatedDate = DateTime.Now,
                    InternalCode = worksheet.Cells[row, 24].Value?.ToString(),
                    IsActive = true,
                    ProductName = worksheet.Cells[row, 20].Value?.ToString(),
                    ProductNameAr = worksheet.Cells[row, 20].Value?.ToString(),
                    Price = double.Parse(worksheet.Cells[row, 29].Value?.ToString()),
                    Discounted = false,
                    Unit = worksheet.Cells[row, 22].Value?.ToString()
                };
                context.Products.Add(newProdcut);
                context.SaveChanges();

                product = context.Products
                .SingleOrDefault(x => x.BarCode ==
                    worksheet.Cells[row, 21].Value.ToString());
            }
            return product;
        }
        public Invoice ProcessInvoice(ExcelWorksheet worksheet, int row, Customer customer,
            Dictionary<string, int> InvoiceIDict)
        {
            var invoice = context.Invoices
                .SingleOrDefault(x => x.InternalId ==
                    worksheet.Cells[row, 18].Value.ToString());
            if (invoice is not null)
            {
                invoice.Date = DateTime.Parse(worksheet.Cells[row, 16].Value?.ToString());
                invoice.Discount = float.Parse(worksheet.Cells[row, 41].Value?.ToString());
                context.Invoices.Update(invoice);
                context.SaveChanges();
                if (!InvoiceIDict.ContainsKey(invoice.Id.ToString()))
                    InvoiceIDict.Add(invoice.Id.ToString(), 1);
            }
            else if (invoice is null)
            {
                var newInvoice = new Invoice
                {
                    CustomerId = customer.Id,
                    InternalId = worksheet.Cells[row, 18].Value?.ToString(),
                    Status = "valid",
                    Date = DateTime.Parse(worksheet.Cells[row, 16].Value?.ToString()),
                    Discount = float.Parse(worksheet.Cells[row, 41].Value?.ToString()),
                };
                context.Invoices.Add(newInvoice);
                context.SaveChanges();

                invoice = context.Invoices
                .SingleOrDefault(x => x.InternalId ==
                    worksheet.Cells[row, 18].Value.ToString());

                if (!InvoiceIDict.ContainsKey(invoice.Id.ToString()))
                    InvoiceIDict.Add(invoice.Id.ToString(), 1);
            }
            return invoice;
        }
        public List<ValidationError> InvoiceTotalValidations(ExcelWorksheet worksheet, int RowCount, List<ValidationError> Errors)
        {
            decimal Sales = 0; decimal Net = 0; decimal Grand = 0;
            var FirstInternalId = worksheet.Cells[6, 18].Value?.ToString();
            for (int row = 6; row <= RowCount + 1; row++)
            {
                if(row == RowCount + 1)
                {
                    if (FirstInternalId == worksheet.Cells[row, 18].Value?.ToString())
                    {
                        var TotalsList = HelperFunctions.HelperFunctions.GetTotalPerRow(
                        worksheet, row,
                        Sales, Net, Grand);
                        Sales = TotalsList[0];
                        Net = TotalsList[1];
                        Grand = TotalsList[2];
                        var InvoiceSales = decimal.Parse(worksheet.Cells[row, 38].Value?.ToString());
                        var InvoiceNet = decimal.Parse(worksheet.Cells[row, 39].Value?.ToString());
                        var InvoiceGrand = decimal.Parse(worksheet.Cells[row, 40].Value?.ToString());
                        var InvoiceDiscount = decimal.Parse(worksheet.Cells[row, 41].Value?.ToString());
                        if (Sales != InvoiceSales)
                        {
                            Errors.Add(new ValidationError
                            {
                                Row = row,
                                Col = 38,
                                Message = "Please re-calculate Invoice Sales Total"
                            });
                        }
                        if (Net != InvoiceNet)
                        {
                            Errors.Add(new ValidationError
                            {
                                Row = row,
                                Col = 39,
                                Message = "Please re-calculate Invoice Net Total"
                            });
                        }
                        Grand -= InvoiceDiscount;
                        if (Grand != InvoiceGrand)
                        {
                            Errors.Add(new ValidationError
                            {
                                Row = row,
                                Col = 40,
                                Message = "Please re-calculate Invoice Grand Total"
                            });
                        }
                        // reset Totals for next invoice
                        Sales = 0; Net = 0; Grand = 0;
                    }
                    if (FirstInternalId != worksheet.Cells[row, 18].Value?.ToString())
                    {
                        FirstInternalId = worksheet.Cells[row, 18].Value?.ToString();
                        // check for total invoices
                        var InvoiceSales = decimal.Parse(worksheet.Cells[row, 38].Value?.ToString());
                        var InvoiceNet = decimal.Parse(worksheet.Cells[row, 39].Value?.ToString());
                        var InvoiceGrand = decimal.Parse(worksheet.Cells[row, 40].Value?.ToString());
                        var InvoiceDiscount = decimal.Parse(worksheet.Cells[row, 41].Value?.ToString());
                        if (Sales != InvoiceSales)
                        {
                            Errors.Add(new ValidationError
                            {
                                Row = row,
                                Col = 38,
                                Message = "Please re-calculate Invoice Sales Total"
                            });
                            Errors = Errors.Distinct().ToList();

                        }
                        if (Net != InvoiceNet)
                        {
                            Errors.Add(new ValidationError
                            {
                                Row = row,
                                Col = 39,
                                Message = "Please re-calculate Invoice Net Total"
                            });
                            Errors = Errors.Distinct().ToList();

                        }
                        Grand -= InvoiceDiscount;
                        if (Grand != InvoiceGrand)
                        {
                            Errors.Add(new ValidationError
                            {
                                Row = row,
                                Col = 40,
                                Message = "Please re-calculate Invoice Grand Total"
                            });
                            Errors = Errors.Distinct().ToList();

                        }
                        // reset Totals for next invoice
                        Sales = 0; Net = 0; Grand = 0;
                    }
                }
                else
                {
                    if (FirstInternalId == worksheet.Cells[row, 18].Value?.ToString())
                    {
                        var TotalsList = HelperFunctions.HelperFunctions.GetTotalPerRow(
                        worksheet, row,
                        Sales, Net, Grand);
                        Sales = TotalsList[0];
                        Net = TotalsList[1];
                        Grand = TotalsList[2];

                    }
                    if (FirstInternalId != worksheet.Cells[row+1, 18].Value?.ToString())
                    {
                        FirstInternalId = worksheet.Cells[row+1, 18].Value?.ToString();
                        // check for total invoices
                        var InvoiceSales = decimal.Parse(worksheet.Cells[row, 38].Value?.ToString());
                        var InvoiceNet = decimal.Parse(worksheet.Cells[row, 39].Value?.ToString());
                        var InvoiceGrand = decimal.Parse(worksheet.Cells[row, 40].Value?.ToString());
                        var InvoiceDiscount = decimal.Parse(worksheet.Cells[row, 41].Value?.ToString());
                        if (Sales != InvoiceSales)
                        {
                            Errors.Add(new ValidationError
                            {
                                Row = row - 1,
                                Col = 38,
                                Message = "Please re-calculate Invoice Sales Total"
                            });
                            Errors = Errors.Distinct().ToList();

                        }
                        if (Net != InvoiceNet)
                        {
                            Errors.Add(new ValidationError
                            {
                                Row = row - 1,
                                Col = 39,
                                Message = "Please re-calculate Invoice Net Total"
                            });
                            Errors = Errors.Distinct().ToList();

                        }
                        Grand -= InvoiceDiscount;
                        if (Grand != InvoiceGrand)
                        {
                            Errors.Add(new ValidationError
                            {
                                Row = row - 1,
                                Col = 40,
                                Message = "Please re-calculate Invoice Grand Total"
                            });
                            Errors = Errors.Distinct().ToList();

                        }
                        // reset Totals for next invoice
                        Sales = 0; Net = 0; Grand = 0;
                    }
                    
                }
                
            }
            return Errors;
        }
        public List<ValidationError> TotalPerRowValidations(ExcelWorksheet worksheet, int RowCount, List<ValidationError> Errors)
        {
            for (var row = 6; row <= RowCount + 1; row++)
            {
                var rowErrors = HelperFunctions.HelperFunctions.CheckItemTotals(Errors, worksheet, row);
                Errors.AddRange(rowErrors);
                Errors = Errors.Distinct().ToList();
            }

            return Errors;
        }
        public List<ValidationError> TypesValidations(ExcelWorksheet worksheet, int RowCount, List<ValidationError> Errors)
        {
            for (var row = 6; row <= RowCount + 1; row++)
            {
                var rowErrors = HelperFunctions.HelperFunctions.ValidateRow(worksheet, row);
                Errors.AddRange(rowErrors);
                Errors = Errors.Distinct().ToList();
            }
            return Errors;
        }
        public Customer ProcessInvoiceCustomer(ExcelWorksheet worksheet, int row)
        {
            var customer = context.Customers
                                .FirstOrDefault(x => x.RegistrationNumber ==
                                    worksheet.Cells[row, 13].Value.ToString());
            if (customer is not null)
            {
                customer.RegistrationNumber = worksheet.Cells[row, 13].Value?.ToString();
                customer.BuildingNo = int.Parse(worksheet.Cells[row, 6].Value?.ToString());
                customer.City = worksheet.Cells[row, 4].Value?.ToString();
                customer.CityAr = worksheet.Cells[row, 4].Value?.ToString();
                customer.Country = worksheet.Cells[row, 2].Value?.ToString();
                customer.CountryAr = worksheet.Cells[row, 2].Value?.ToString();
                customer.Street = worksheet.Cells[row, 5].Value?.ToString();
                customer.StreetAr = worksheet.Cells[row, 5].Value?.ToString();
                customer.Type = worksheet.Cells[row, 12].Value?.ToString();
                customer.Name = worksheet.Cells[row, 14].Value?.ToString();
                customer.NameAr = worksheet.Cells[row, 14].Value?.ToString();
                context.Customers.Update(customer);
                context.SaveChanges();
            }
            else if (customer is null)
            {
                var newCustomer = new Customer
                {
                    RegistrationNumber = worksheet.Cells[row, 13].Value?.ToString(),
                    BuildingNo = int.Parse(worksheet.Cells[row, 6].Value?.ToString()),
                    City = worksheet.Cells[row, 4].Value?.ToString(),
                    CityAr = worksheet.Cells[row, 4].Value?.ToString(),
                    Country = worksheet.Cells[row, 2].Value?.ToString(),
                    CountryAr = worksheet.Cells[row, 2].Value?.ToString(),
                    CompanyId = 1,
                    Street = worksheet.Cells[row, 5].Value?.ToString(),
                    StreetAr = worksheet.Cells[row, 5].Value?.ToString(),
                    Type = worksheet.Cells[row, 12].Value?.ToString(),
                    Name = worksheet.Cells[row, 14].Value?.ToString(),
                    NameAr = worksheet.Cells[row, 14].Value?.ToString(),
                    Number = "01122"
                };
                context.Customers.Add(newCustomer);
                context.SaveChanges();
                customer = context.Customers
                .FirstOrDefault(x => x.RegistrationNumber ==
                    worksheet.Cells[row, 13].Value.ToString());
            }
            return customer;
        }
        private void SettingInvoiceDetails(List<InvoiceDetails> InvoiceDetailList, Dictionary<string, int> InvoiceIDict)
        {
            foreach (var key in InvoiceIDict.Keys)
            {
                var invoice = context.Invoices
                    .Find(int.Parse(key));
                var invDetails = context.InvoiceDetails
                    .Where(x => x.InvoiceId == invoice.Id);
                context.InvoiceDetails.RemoveRange(invDetails);
                context.SaveChanges();

                var newInvDetails = InvoiceDetailList
                    .Where(x => x.InvoiceId == invoice.Id);
                context.InvoiceDetails.AddRange(newInvDetails);
                context.SaveChanges();
            }
        }
        private void ProcessExcelSheetRows(ExcelWorksheet worksheet, int RowCount, 
            List<InvoiceDetails> InvoiceDetailList, Dictionary<string, int> InvoiceIDict)
        {
            for (var row = 6; row <= RowCount + 1; row++)
            {
                // process customer
                var Customer = ProcessInvoiceCustomer(worksheet, row);

                // process invoice
                var Invoice = ProcessInvoice(worksheet, row, Customer, InvoiceIDict);

                // process product
                var Product = ProcessInvoiceItem(worksheet, row);

                // add to invoice details list
                InvoiceDetailList.Add(new InvoiceDetails
                {
                    InvoiceId = Invoice.Id,
                    ProductId = Product.Id,
                    Price = float.Parse(worksheet.Cells[row, 29].Value?.ToString()),
                    Quantity = int.Parse(worksheet.Cells[row, 23].Value?.ToString()),
                    Tax = float.Parse(worksheet.Cells[row, 37].Value?.ToString()),
                });

            }
        }
    }
}