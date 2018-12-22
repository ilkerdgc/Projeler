using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrunKatalog.Models;
using UrunKatalog.ViewModels;

namespace UrunKatalog.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        DataContext db = new DataContext();

        // GET: Admin
        public ActionResult Index()
        {
            var result = new AdminIndexCount()
            {
                OrderCount = db.Orders.Count(),
                ProductCount = db.Products.Count(),
                ReviewCount = db.Reviews.Count(),
            };

            return View(result);
        }

        
    }
}