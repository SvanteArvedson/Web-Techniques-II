using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weather.App.ViewModels;
using Weather.Domain.Entities;
using Weather.Domain.PersistentStorage;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.Owin.Security;

namespace Weather.App.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private UserManager<User> UserManager
        {
            get { return _userManager ?? (_userManager = UnitOfWork.getInstance().UserManager); }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        //
        // GET: /Konto/Inloggning
        public ActionResult Login(string returnUrl = "~/")
        {
            if (User.Identity.IsAuthenticated)
            {
                TempData["Error"] = "Du måste vara utloggad för att logga in";
                return RedirectToAction("Index", "Forecast");
            }

            ViewBag.returnUrl = returnUrl;
            var model = new AccountLoginViewModel();
            return View(model);
        }

        //
        // POST: /Konto/Inloggning
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Name, Password")]AccountLoginViewModel model, string returnUrl = "~/")
        {
            if (User.Identity.IsAuthenticated)
            {
                TempData["Error"] = "Du måste vara utloggad för att logga in";
                return RedirectToAction("Index", "Forecast");
            }

            if (ModelState.IsValid)
            {
                User user = UserManager.Find(model.Name, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError(String.Empty, "Felaktigt användarnamn eller lösenord");
                }
                else
                {
                    ClaimsIdentity identity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);

                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Konto/Utloggning
        [Authorize]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Forecast");
        }

        //
        // GET: /Konto/Nytt
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                TempData["Error"] = "Du måste vara utloggad för att skapa ett nytt konto";
                return RedirectToAction("Index", "Forecast");
            }

            AccountCreateViewModel model = new AccountCreateViewModel();
            return View(model);
        }

        //
        // POST: /Konto/Nytt
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Password, Email")]AccountCreateViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                TempData["Error"] = "Du måste vara utloggad för att skapa ett nytt konto";
                return RedirectToAction("Index", "Forecast");
            }

            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    UserName = model.Name,
                    Email = model.Email
                };
                IdentityResult result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    TempData["Success"] = String.Format("Kontot för {0} har skapats", model.Name);
                    return RedirectToAction("Index", "Forecast");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(model);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError(String.Empty, error);
            }
        }
	}
}