using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UrunKatalog.Identity;

namespace UrunKatalog.ViewModels
{
    public class UserDetails
    {
        //private IdentityDataContext _Db = new IdentityDataContext();
        //public IdentityDataContext DB { get { return _Db; } }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

    }
}