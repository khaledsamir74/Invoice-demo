using ClosedXML.Excel;
using InvoiceDemo.Models;
using InvoiceDemo.Models.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Text;

namespace InvoiceDemo.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext context;

        public CompanyController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET: CompanyControlle1
        public ActionResult Index()
        {
            return View(context.Companies.ToList());
        }

        // GET: CompanyControlle1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompanyControlle1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyControlle1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Company company)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    
                        var newCompany = new Company
                        {
                            Name = company.Name,
                            NameAr = company.NameAr,
                            BuildingNo = company.BuildingNo,
                            Street = company.Street,
                            StreetAr = company.StreetAr,
                            City = company.City,
                            CityAr = company.CityAr,
                            Country = company.Country,
                            CountryAr = company.CountryAr,
                            Number = company.Number,
                            RegistrationNumber = company.RegistrationNumber,
                            Type = company.Type
                        };
                        context.Companies.Add(newCompany);
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

        // GET: CompanyControlle1/Edit/5
        public ActionResult Edit(int id)
        {
            var company = context.Companies.Find(id);
            if (company is not null)
                return View(company);
            else return View();
        }

        // POST: CompanyControlle1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Company company)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var updateCompany = context.Companies.Find(id);
                    if (updateCompany is not null)
                    {

                        updateCompany.BuildingNo = company.BuildingNo;
                        updateCompany.Street = company.Street;
                        updateCompany.StreetAr = company.StreetAr;
                        updateCompany.City = company.City;
                        updateCompany.CityAr = company.CityAr;
                        updateCompany.Country = company.Country;
                        updateCompany.CountryAr = company.CountryAr;
                        updateCompany.Name = company.Name;
                        updateCompany.NameAr = company.NameAr;
                        updateCompany.Number = company.Number;
                        updateCompany.RegistrationNumber = company.RegistrationNumber;
                        updateCompany.Type = company.Type;
                        context.Companies.Update(updateCompany);
                        context.SaveChanges();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View();
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: CompanyControlle1/Delete/5
        public ActionResult Delete(int id)
        {
            return View(context.Companies.Find(id));
        }

        // POST: CompanyControlle1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Product product)
        {
            try
            {
                var company = context.Companies.Find(id);
                if (company is not null)
                {
                    context.Companies.Remove(company);
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
        public IActionResult ExportToCSV()
        {
            var companies = context.Companies.ToList();
            var builder = new StringBuilder();
            builder.AppendLine("ID,Name,Type,Building number,Street, City, Country,Registraion Number ,Tax Activity Code");
            foreach (var company in companies)
            {
                builder.AppendLine($"{company.Id} , {company.Name}, {company.BuildingNo}, {company.Street}, {company.City}, {company.Country}, {company.RegistrationNumber}, {company.TaxActivityCode}");
            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "Companies.csv");
        }
        public IActionResult ExportToExcel()
        {
            var companies = context.Companies.ToList();
            using (var workbook=new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Products");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ID";
                worksheet.Cell(currentRow, 1).Style.Font.FontName = "Calibri";
                worksheet.Cell(currentRow, 1).Style.Font.FontSize =11;
                worksheet.Cell(currentRow, 1).Style.Fill.SetBackgroundColor(XLColor.Gray);
                worksheet.Cell(currentRow, 2).Value = "Name";
                worksheet.Cell(currentRow, 3).Value = "Building number";
                worksheet.Cell(currentRow, 4).Value = "street";
                worksheet.Cell(currentRow, 5).Value = "City";
                foreach (var company in companies)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = company.Id;
                    worksheet.Cell(currentRow, 2).Value = company.Name;
                    worksheet.Cell(currentRow, 3).Value = company.BuildingNo;
                    worksheet.Cell(currentRow, 4).Value = company.Street;
                    worksheet.Cell(currentRow, 5).Value = company.City;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Company.xlsx");
                }
            }
        }
    }
}
