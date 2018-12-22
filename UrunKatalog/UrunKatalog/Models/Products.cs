using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UrunKatalog.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        [MaxLength(50)]
        public string ProductNames { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string Image { get; set; }
        public bool IsHome { get; set; }
        public bool IsApproved { get; set; }

        public int CategoryId { get; set; }
        public virtual Categories Category { get; set; }

        public virtual List<Reviews> Reviews { get; set; }
    }
}