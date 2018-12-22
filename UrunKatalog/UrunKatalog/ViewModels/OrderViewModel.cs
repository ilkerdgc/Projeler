using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UrunKatalog.Models;

namespace UrunKatalog.ViewModels
{
    public class OrderForm
    {
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen adres tanımını giriniz!")]
        public string AdresBasligi { get; set; }

        [Required(ErrorMessage = "Lütfen bir adres giriniz!")]
        public string Adres { get; set; }

        [Required(ErrorMessage = "Lütfen bir şehir giriniz!")]
        public string Sehir { get; set; }

        [Required(ErrorMessage = "Lütfen bir semt giriniz!")]
        public string Semt { get; set; }

        [Required(ErrorMessage = "Lütfen bir mahalle giriniz!")]
        public string Mahalle { get; set; }

        [Required(ErrorMessage = "Lütfen bir posta kodu giriniz!")]
        public string PostaKodu { get; set; }
    }

    public class OrderDetailsModel
    {
        public int OrderId { get; set; }   
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public EnumOrderState OrderState { get; set; }
        public int TotalProduct { get; set; }
        public double TotalPrice { get; set; }
    }

    public class OrderDetailsModel2
    {
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public string OrderNumber { get; set; }
        public double TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public EnumOrderState OrderState { get; set; }

        public string AdresBasligi { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }
        public string Semt { get; set; }
        public string Mahalle { get; set; }
        public string PostaKodu { get; set; }

        public List<OrderProductDetailsModel> OrderProductDetails { get; set; }
    }

    public class OrderProductDetailsModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }

    public enum EnumOrderState
    {
        [Display(Name = "Onay Bekleniyor")]
        Waiting,
        [Display(Name = "Tamamlandı")]
        Completed
    }

    public static class OrderViewModel
    {
        private static DataContext db = CartViewModel.DB;

        public static void SaveOrder(List<Cart> cart, OrderForm orderForm)
        {
            var orderdetails = new OrderDetails()
            {
                UserName = orderForm.UserName,
                OrderNumber = "A" + (new Random().Next(111111,999999).ToString()),
                OrderDate = DateTime.Now,
                OrderState = EnumOrderState.Waiting,
                AdresBasligi = orderForm.AdresBasligi,
                Adres = orderForm.Adres,
                Sehir = orderForm.Sehir,
                Semt = orderForm.Semt,
                Mahalle = orderForm.Mahalle,
                PostaKodu = orderForm.PostaKodu,
                Total = CartViewModel.TotalProduct(),
                
                Order = new List<Orders>()
            };

            foreach (var item in cart)
            {
                var order = new Orders()
                {
                    Quantity = item.Quantity,
                    Price = item.Quantity * item.Products.Price,
                    ProductId = item.ProductId,
                };

                orderdetails.Order.Add(order);
            }

            db.OrderDetails.Add(orderdetails);
            db.SaveChanges();
        }
    }
}