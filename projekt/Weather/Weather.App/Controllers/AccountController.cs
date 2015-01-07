using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weather.App.ViewModels;

namespace Weather.App.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Konto/Inloggning
        public ActionResult Login()
        {
            var model = new AccountLoginViewModel();
            return View(model);
        }

        public ActionResult Create(AccountLoginViewModel model)
        {
            return null;
        }
	}
}