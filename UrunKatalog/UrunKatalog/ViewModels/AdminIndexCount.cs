using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrunKatalog.ViewModels
{
    public class AdminIndexCount
    {
        public int ReviewCount { get; set; }
        public int ProductCount { get; set; }
        public int OrderCount { get; set; }
        public int UserCount { get; set; }
    }
}