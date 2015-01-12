using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Weather.App.ViewModels;
using Weather.Domain.Entities;
using Weather.Domain.PersistentStorage;

namespace Weather.App.Controllers
{
    [OutputCache(Duration = 0)]
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
                    TempData["Success"] = String.Format("Kontot {0} har skapats", model.Name);

                    ClaimsIdentity identity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);

                    return RedirectToAction("Index", "Forecast");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(model);
        }

        //
        // POST: /Konto/InloggningGoogle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GoogleLogin(string returnUrl = "~/")
        {
            if (User.Identity.IsAuthenticated)
            {
                TempData["Error"] = "Du måste vara utloggad för att logga in";
                return RedirectToAction("Index", "Forecast");
            }

            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleLoginCallback", new { returnUrl = returnUrl })
            };
            HttpContext.GetOwinContext().Authentication.Challenge(properties, "Google");
            return new HttpUnauthorizedResult();
        }

        // Callback method for google login
        public ActionResult GoogleLoginCallback(string returnUrl = "~/")
        {
            ExternalLoginInfo loginInfo = AuthenticationManager.GetExternalLoginInfo();
            User user = UserManager.Find(loginInfo.Login);
            if (user == null)
            {
                user = new User
                {
                    Email = loginInfo.Email,
                    UserName = loginInfo.DefaultUserName,
                };
                IdentityResult result = UserManager.Create(user);
                if (!result.Succeeded)
                {
                    TempData["Error"] = "Ett fel inträffade när ditt konto skulle skapas";
                    return RedirectToAction("Index", "Forecast");
                }
                else
                {
                    result = UserManager.AddLogin(user.Id, loginInfo.Login);
                    if (!result.Succeeded)
                    {
                        TempData["Error"] = "Ditt konto skapades men ett fel inträffade när kontot försökte logga in";
                        return RedirectToAction("Index", "Forecast");
                    }
                }
            }

            ClaimsIdentity identity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddClaims(loginInfo.ExternalIdentity.Claims);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);

            TempData["Success"] = String.Format("Användaren {0} är inloggad", user.UserName);
            return Redirect(returnUrl);
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