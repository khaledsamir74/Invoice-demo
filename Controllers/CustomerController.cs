using InvoiceDemo.Models;
using InvoiceDemo.Models.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDemo.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext context;

        public CustomerController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET: CustomerController
        public ActionResult Index()
        {
            return View(context.Customers.ToList());
        }
        // GET: CustomerController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(context.Customers.Find(customer.Id) is null)
                    {
                        var newCustomer = new Customer
                        {
                            Name = customer.Name,
                            NameAr = customer.NameAr,
                            BuildingNo = customer.BuildingNo,
                            Street = customer.Street,
                            StreetAr = customer.StreetAr,
                            City = customer.City,
                            CityAr = customer.CityAr,
                            Country = customer.Country,
                            CountryAr = customer.CountryAr,
                            Number = customer.Number,
                            RegistrationNumber = customer.RegistrationNumber,
                            Type = customer.Type,
                            CompanyId = 1,
                        };
                        context.Customers.Add(newCustomer);
                        context.SaveChanges();
                        return RedirectToAction(nameof(Index));
                    }

                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var customer = context.Customers.Find(id);
            if (customer is not null)
                return View(customer);
            else return View();
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var updateCustomer = context.Customers.Find(id);
                    if (updateCustomer is not null)
                    {

                        updateCustomer.BuildingNo = customer.BuildingNo;
                        updateCustomer.Street = customer.Street;
                        updateCustomer.StreetAr = customer.StreetAr;
                        updateCustomer.City = customer.City;
                        updateCustomer.CityAr = customer.CityAr;
                        updateCustomer.Country = customer.Country;
                        updateCustomer.CountryAr = customer.CountryAr;
                        updateCustomer.Name = customer.Name;
                        updateCustomer.NameAr = customer.NameAr;
                        updateCustomer.Number = customer.Number;
                        updateCustomer.RegistrationNumber = customer.RegistrationNumber;
                        updateCustomer.Type = customer.Type;
                        context.Customers.Update(updateCustomer);
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

        // GET: CustomerController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(context.Customers.Find(id));
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Product product)
        {
            try
            {
                var customer = context.Customers.Find(id);
                if(customer is not null)
                {
                    context.Customers.Remove(customer);
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
            var customers = context.Customers.ToList();
            var builder = new StringBuilder();
            builder.AppendLine("ID,Name,Type,Building number,Street, City, Country,Registraion Number");
            foreach (var customer in customers)
            {
                builder.AppendLine($"{customer.Id} , {customer.Name}, {customer.BuildingNo}, {customer.Street}, {customer.City}, {customer.Country}, {customer.RegistrationNumber}");
            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "Customers.csv");
        }
    }
}
