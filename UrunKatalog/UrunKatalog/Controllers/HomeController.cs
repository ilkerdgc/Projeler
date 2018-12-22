using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrunKatalog.Identity;
using UrunKatalog.Models;
using UrunKatalog.ViewModels;

namespace UrunKatalog.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        IdentityDataContext Db = new IdentityDataContext();

        // GET: Home
        public ActionResult Index()
        {
            var products = db.Products.Where(x => x.IsHome == true && x.IsApproved == true).ToList();

            return View(products);
        }

        public ActionResult Details(int id)
        {
            var productDetails = db.Products.Where(x => x.ProductId == id).FirstOrDefault();

            return View(productDetails);
        }

        public ActionResult ProductList(int? id)
        {
            var productList = db.Products.AsQueryable();

            if (id != null)
            {
                productList = productList.Where(x => x.CategoryId == id);
            }

            return View(productList);
        }

        public PartialViewResult _CategoryList()
        {
            var categoryList = db.Categories.ToList();

            return PartialView(categoryList);
        }

        [Authorize]
        public ActionResult Profile(string username)
        {
            var userdetails = new UserDetails();

            foreach (var item in Db.Users)
            {
                if (item.UserName == username)
                {
                    userdetails.Name = item.Name;
                    userdetails.Surname = item.Surname;
                    userdetails.Email = item.Email;
                    userdetails.UserName = item.UserName;
                }   
            }

            return View(userdetails);
        }

        [Authorize]
        public ActionResult OrderDetails(string username)
        {
            var orders = db.OrderDetails.Where(i => i.UserName == username).Select(x => new OrderDetailsModel()
            {
                OrderDate = x.OrderDate,
                OrderNumber = x.OrderNumber,
                OrderState = x.OrderState,
                TotalPrice = x.Order.Sum(z => z.Price),
                TotalProduct = x.Order.Count,
                OrderId = x.OrderDetailsId
            }).OrderByDescending(x => x.OrderDate).ToList();


            return View(orders); 
        }

        [Authorize]
        public ActionResult OrderProductDetails(int id)
        {
            var orders = db.OrderDetails.Where(x => x.OrderDetailsId == id).Select(i => new OrderDetailsModel2()
            {
                OrderId = i.OrderDetailsId, 
                UserName = i.UserName,
                OrderDate = i.OrderDate,
                OrderNumber = i.OrderNumber,
                OrderState = i.OrderState,
                TotalPrice = i.Order.Sum(x => x.Price),
                AdresBasligi = i.AdresBasligi,
                Adres = i.Adres,
                Sehir = i.Sehir,
                Semt = i.Semt,
                Mahalle = i.Mahalle,
                PostaKodu = i.PostaKodu,
                OrderProductDetails = db.Orders.Select(y => new OrderProductDetailsModel()
                {
                    ProductId = y.ProductId,
                    ProductName = y.Product.ProductNames,
                    Image = y.Product.Image,
                    Price = y.Price,
                    Quantity = y.Quantity
                }).ToList()
            }).FirstOrDefault();

            return View(orders);
        }

        public ActionResult CreateReview(string reviews, int productId, string username)
        {

            var user = Db.Users.Where(x => x.UserName == username).FirstOrDefault();

            var review = new Reviews()
            {
                ProductId = productId,
                UserName = username,
                Review = reviews,
                DateTime = DateTime.Now,
                Email = user.Email,
                Name = user.Name,
                IsDeleted = false
            };
            db.Reviews.Add(review);
            db.SaveChanges();

            return RedirectToAction("Details");
        }
    }
}