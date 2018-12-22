using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrunKatalog.Models;
using UrunKatalog.ViewModels;

namespace UrunKatalog.Controllers
{
    public class WishlistController : Controller
    {
        DataContext db = WishlistViewModel.DB;

        // GET: Wishlist
        public ActionResult Index()
        {
            ViewBag.TotalProduct = WishlistViewModel.TotalProduct();
            return View(WishlistViewModel.WishlistList()); 
        }

        public ActionResult AddToWishlist(int id)
        {
            var product = db.Products.Where(x => x.ProductId == id).FirstOrDefault();

            WishlistViewModel.AddProduct(product, 1);

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromWishlist(int id)
        {
            WishlistViewModel.DeleteProduct(id);

            return RedirectToAction("Index");
        }

        public ActionResult Clear()
        {
            WishlistViewModel.ClearWishlist();

            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult AddToCart(int id)
        {
            WishlistViewModel.AddToCartProduct(id, User.Identity.Name);

            return RedirectToAction("Index");
        }
    }
}