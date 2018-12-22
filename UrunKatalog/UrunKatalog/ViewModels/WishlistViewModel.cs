using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UrunKatalog.Models;

namespace UrunKatalog.ViewModels
{
    public static class WishlistViewModel
    {

        private static DataContext _DB = new DataContext();
        public static DataContext DB { get { return _DB; } }

        public static IEnumerable<Wishlists> WishlistList()
        {
            var list = _DB.Wishlists.ToList();
            return list;
        }

        public static void AddProduct(Products products, int quantity)
        {
            var line = _DB.Wishlists.Where(x => x.ProductId == products.ProductId).FirstOrDefault();

            if (line == null)
            {
                Wishlists wishlists = new Wishlists()
                {
                    IsActive = true,
                    ProductId = products.ProductId,
                    Products = products,
                    Quantity = quantity
                };

                _DB.Wishlists.Add(wishlists);
            }
            else
            {
                line.Quantity += quantity;
            }
            _DB.SaveChanges();
        }

        public static void DeleteProduct(int id)
        {
            var line = _DB.Wishlists.Where(x => x.ProductId == id).FirstOrDefault();

            if (line != null)
            {
                _DB.Wishlists.Remove(line);
                _DB.SaveChanges();
            }
        }

        public static void ClearWishlist()
        {
            var wishlist = from product in _DB.Wishlists
                           select product;

            _DB.Wishlists.RemoveRange(wishlist);
            _DB.SaveChanges();
        }

        public static double TotalProduct()
        {
            if (_DB.Wishlists.Count() == 0)
            {
                return 0;
            }
            else
            {
                return _DB.Wishlists.Sum(x => x.Products.Price * x.Quantity);
            }
            
        } 

        public static void AddToCartProduct(int id, string username)
        {
            var wishlistline = _DB.Wishlists.Where(x => x.ProductId == id).SingleOrDefault();
            var cartline = _DB.Cart.Where(x => x.ProductId == wishlistline.ProductId).SingleOrDefault();

            if (cartline == null)
            {
                Cart cart = new Cart()
                {
                    ProductId = wishlistline.ProductId,
                    Products = wishlistline.Products,
                    Quantity = wishlistline.Quantity,
                    UserName = username
                };
   
                _DB.Cart.Add(cart);
            }
            else
            {
                cartline.Quantity += wishlistline.Quantity;
            }

            _DB.Wishlists.Remove(wishlistline);
            _DB.SaveChanges();
        }
    }
}