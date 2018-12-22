using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UrunKatalog.Models
{
    public class Reviews
    {
        [Key]
        public int ReviewId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Review { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsDeleted { get; set; }
        public string UserName { get; set; }

        public int ProductId { get; set; }
        public virtual Products Product { get; set; }
    }
}