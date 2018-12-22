using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UrunKatalog.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public int OrderDetailsId { get; set; }
        public virtual OrderDetails OrderDetails { get; set; }

        public int ProductId { get; set; }
        public virtual Products Product { get; set; }
    }
}