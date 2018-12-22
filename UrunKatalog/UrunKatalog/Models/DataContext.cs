using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UrunKatalog.Models
{
    public class DataContext:DbContext
    {
        public DataContext():base("dataConnection")
        {

        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<ShippingDetails> ShippingDetails { get; set; }
        public DbSet<Wishlists> Wishlists { get; set; }
        public DbSet<Cart> Cart { get; set; }
    }
}