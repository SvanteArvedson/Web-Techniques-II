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
    /// <summary>
    /// Controller methods for handling user accounts
    /// </summary>
    [OutputCache(Duration = 0)]
    public class AccountController : Controller
    {
        /// <summary>
        /// Repository for user tables
        /// </summary>
        private UserManager<User> _userManager;

        /// <summary>
        /// Property returning _userManager
        /// </summary>
        private UserManager<User> UserManager
        {
            get { return _userManager ?? (_userManager = UnitOfWork.getInstance().UserManager); }
        }

        /// <summary>
        /// Handles authentication cookies
        /// </summary>
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

                    // Right message
                    TempData["Success"] = String.Format("Användaren {0} är inloggad", user.UserName);

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
                    ClaimsIdentity identity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);

                    // Right message
                    TempData["Success"] = String.Format("Kontot {0} har skapats", model.Name);

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

            // Sets callback url for Google login service
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleLoginCallback", new { returnUrl = returnUrl })
            };
            HttpContext.GetOwinContext().Authentication.Challenge(properties, "Google");
            return new HttpUnauthorizedResult();
        }

        /// <summary>
        /// Callback method for Google login service
        /// </summary>
        /// <param name="returnUrl">Url to return to after login</param>
        /// <returns>RedirectToAction result</returns>
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

            // Right message
            TempData["Success"] = String.Format("Användaren {0} är inloggad", user.UserName);
            return Redirect(returnUrl);
        }

        /// <summary>
        /// Private helper method to display error messages
        /// </summary>
        /// <param name="result"></param>
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError(String.Empty, error);
            }
        }
    }
}