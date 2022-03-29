using InvoiceDemo.Models;
using InvoiceDemo.Models.Context;
using InvoiceDemo.Models.DTO.Product;
using InvoiceDemo.Models.ViewModels.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InvoiceDemo.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext context;

        public ProductController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET: ProductController
        [HttpGet]
        public ActionResult Index()
        {
            var productList = context.Products.ToList();
                return View(productList.Select(x => new Product { 
                    Price = x.Price, Id = x.Id , IsActive = x.IsActive ,
                    ProductName = x.ProductName, ProductNameAr = x.ProductNameAr
                }));
        }

        // GET: ProductController/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            var record = new Product
            {
                ProductName = product.ProductName,
                ProductNameAr = product.ProductNameAr,
                Price = product.Price,
                InternalCode = product.InternalCode,
                BarCode = product.BarCode,
                Description = product.Description,
                CompanyId = 1,
                CreatedDate = dateTime,
                IsActive = product.IsActive,
                Unit = product.Unit,
                Discounted = false
            };
            if (ModelState.IsValid)
            {
                context.Products.Add(product);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            
            return View();
        }

        // GET: ProductController/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = context.Products.Find(id);
            if (obj == null)
            {
                return NotFound();

            }
            return View(obj);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                DateTime dateTime = DateTime.UtcNow.Date;
                var record = context.Products.Find(id);
                if (record != null)
                {
                    record.ProductName = product.ProductName;
                    record.ProductNameAr = product.ProductNameAr;
                    record.Description = product.Description;
                    record.Unit = product.Unit;
                    record.Price = product.Price;
                    record.InternalCode = product.InternalCode;
                    record.BarCode = product.BarCode;
                    record.CompanyId = 1;
                    record.ModifiedDate = dateTime;
                    record.IsActive = product.IsActive;

                    context.Products.Update(record);
                    context.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        //get delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var obj = context.Products.Find(id);
            return View(obj);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id,Product product)
        {
            var obj = context.Products.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            context.Products.Remove(obj);
            context.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult GetProductsNames()
        {
            var productList = context.Products.ToList().Select(x => new SelectListItem { 
                Value = x.ProductName,
                Text = x.ProductName
            }).ToList();
            return Json(productList);
        }

        [HttpGet]
        public ActionResult GetProductByName(string Name)
        {
            var product = context.Products.Single(x => x.ProductName == Name);
            return Json(product);
        }
        public IActionResult ExportToCSV()
        {
            var products = context.Products.ToList();
            var builder = new StringBuilder();
            builder.AppendLine("BarCode,Product Name,Price, InternalCode, Created Date");
            foreach (var product in products)
            {
                builder.AppendLine($"{product.BarCode} , {product.ProductName}, {product.Price}, {product.InternalCode}, {product.CreatedDate}");
            }
           
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "Products.csv");
        }
    }
}
