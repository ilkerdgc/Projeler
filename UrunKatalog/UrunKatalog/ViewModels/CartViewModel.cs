using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UrunKatalog.Models;

namespace UrunKatalog.ViewModels
{
    public static class CartViewModel
    {
        private static DataContext _DB = new DataContext();
        public static DataContext DB { get { return _DB; } }

        public static IEnumerable<Cart> CartList(string username)
        {
            var list = _DB.Cart.Where(x => x.UserName == username).ToList();
            return list;
        }

        public static void AddProduct(Products products, int quantity, string username)
        {
            var line = _DB.Cart.Where(x => x.ProductId == products.ProductId).FirstOrDefault();

            if (line == null)
            {
                Cart cart = new Cart()
                {
                    ProductId = products.ProductId,
                    Products = products,
                    Quantity = quantity,
                    UserName = username,
                };
                _DB.Cart.Add(cart);
            }
            else
            {
                line.Quantity += quantity;
            }
            _DB.SaveChanges();
        }

        public static void DeleteProduct(int id, string username)
        {
            var line = _DB.Cart.Where(x => x.ProductId == id && x.UserName == username).FirstOrDefault();

            if (line != null)
            {
                _DB.Cart.Remove(line);
                _DB.SaveChanges();
            }
        }

        public static void ClearCart(string username)
        {
            var cart = from product in _DB.Cart
                       where product.UserName == username
                       select product;

            _DB.Cart.RemoveRange(cart);
            _DB.SaveChanges();
        }

        public static double TotalProduct()
        {
            if (_DB.Cart.Count() == 0)
            {
                return 0;
            }
            else
            {
                return _DB.Cart.Sum(x => x.Products.Price * x.Quantity);
            }

        }
    }
}