using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrunKatalog.Models;

namespace UrunKatalog.Controllers
{
    public class AdminProductController : Controller
    {
        DataContext db = new DataContext();

        // GET: AdminProduct
        public ActionResult Index()
        {
            var model = db.Products.ToList();

            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Products products, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file.FileName != null && file.ContentLength > 0)
                {
                    var extension = Path.GetExtension(file.FileName);

                    if (extension == ".jpg" || extension == ".png" || extension == ".JPEG" || extension == ".jpeg")
                    {
                        var folder = Server.MapPath("~/images"); //dosyanın tam yolunu alıyoruz.
                        var randomfilename = Path.GetRandomFileName(); // rastgele dosya ismi oluşturuyoruz 
                        var filename = Path.ChangeExtension(randomfilename, ".jpg"); //rastgele üretilen dosya ismi ile uzantısını birleştiriyouz
                        var path = Path.Combine(folder, filename); //birleştirilen dosya adını dosya tam yolu ile birleştiriyoruz      

                        file.SaveAs(path);
                        products.Image = filename;

                        db.Products.Add(products);
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View(products);
        }

        public ActionResult Edit(int id)
        {
            var product = db.Products.Find(id);

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Products products, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {              
                if (file.FileName != null && file.ContentLength > 0)
                {               
                    var extension = Path.GetExtension(file.FileName);

                    if (extension == ".jpg" || extension == ".png" || extension == ".JPEG" || extension == ".jpeg")
                    {
                        var product = db.Products.Where(x => x.ProductId == products.ProductId).FirstOrDefault();

                        var folder = Server.MapPath("~/images"); //dosyanın tam yolunu alıyoruz.
                        var path = Path.Combine(folder, product.Image);

                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }

                        var randomfilename = Path.GetRandomFileName(); // rastgele dosya ismi oluşturuyoruz 
                        var filename = Path.ChangeExtension(randomfilename, ".jpg"); //rastgele üretilen dosya ismi ile uzantısını birleştiriyouz
                        var path2 = Path.Combine(folder, filename); //birleştirilen dosya adını dosya tam yolu ile birleştiriyoruz      

                        file.SaveAs(path2);   

                        product.Image = filename;
                        product.ProductNames = products.ProductNames;
                        product.Description = products.Description;
                        product.Price = products.Price;
                        product.Stock = products.Stock;
                        product.IsHome = products.IsHome;
                        product.IsApproved = products.IsApproved;

                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View(products);
        }

        public ActionResult Delete(int id)
        {
            if (id != null)
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}