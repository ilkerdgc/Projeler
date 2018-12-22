using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrunKatalog.Identity;
using UrunKatalog.Models;
using UrunKatalog.ViewModels;

namespace UrunKatalog.Controllers
{
    public class AccountController : Controller
    {
        private DataContext db = new DataContext();
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        public AccountController()
        {
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            userManager = new UserManager<ApplicationUser>(userStore);
            RoleStore<ApplicationRole> roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            roleManager = new RoleManager<ApplicationRole>(roleStore);
        }

        //// GET: Account
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Error", new string[] { "Erişim hakkınız yok" });
            }

            ViewBag.returnUrl = returnUrl;

            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.Find(loginModel.UserName, loginModel.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Yanlış kullanıcı adı veya parola");
                }
                else
                {
                    
                    var autManager = HttpContext.GetOwinContext().Authentication;

                    var identity = userManager.CreateIdentity(user, "ApplicationCookie");
                    var autProperties = new AuthenticationProperties()
                    {
                        IsPersistent = true
                    };

                    autManager.SignOut();
                    autManager.SignIn(autProperties, identity);
                    
                    return Redirect(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);
                }
            }

            ViewBag.returnUrl = returnUrl;

            return View();
        }

        
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register registerModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Name = registerModel.Name,
                    Surname = registerModel.Surname,
                    UserName = registerModel.Username,
                    Email = registerModel.Email,           
                };

                IdentityResult ıdentityResult = userManager.Create(user, registerModel.Password);

                if (ıdentityResult.Succeeded)
                {
                    if (roleManager.RoleExists("user"))
                    {
                        userManager.AddToRole(user.Id, "user");
                    }
                    return RedirectToAction("Login","Account");
                }
                else
                {
                    ModelState.AddModelError("RegisterUserError", "Kullanıcı oluşturma hatası");
                }
            }

            return View(registerModel);
        }

        public ActionResult Logout()
        {
            var autmanager = HttpContext.GetOwinContext().Authentication;
            autmanager.SignOut();

            return RedirectToAction("Index","Home");
        }
    }
}