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
    [Authorize]
    public class CartController : Controller
    {
        DataContext db = CartViewModel.DB;

        // GET: Cart
        public ActionResult Index()
        {
            ViewBag.TotalProduct = CartViewModel.TotalProduct();
            return View(CartViewModel.CartList(User.Identity.Name));
           
        }

        public ActionResult AddToCart(int id)
        {
            var product = db.Products.Where(x => x.ProductId == id).FirstOrDefault();

            CartViewModel.AddProduct(product, 1, User.Identity.Name);

            return RedirectToAction("Index");

        }

        public ActionResult RemoveFromCart(int id)
        {
            CartViewModel.DeleteProduct(id, User.Identity.Name);

            return RedirectToAction("Index");
        }

        public ActionResult Clear()
        {
            CartViewModel.ClearCart(User.Identity.Name);

            return RedirectToAction("Index");
        }

        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(OrderForm orderForm)
        {
            var cart = db.Cart.Where(x => x.UserName == orderForm.UserName).ToList();

            if (cart == null)
            {
                ModelState.AddModelError("", "Sepetinizde ürün bulunmamaktadır.");
            }

            if (ModelState.IsValid)
            {
                OrderViewModel.SaveOrder(cart, orderForm);
                CartViewModel.ClearCart(orderForm.UserName);

                return View("Completed");
            }
            else
            {
                return View(orderForm);
            }
        }
    }
}