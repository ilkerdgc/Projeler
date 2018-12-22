using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UrunKatalog.Models
{
    public class ShippingDetails
    {
        [Key]
        public int ShippingId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int MobileNumber { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public int PostCode { get; set; }
    }
}