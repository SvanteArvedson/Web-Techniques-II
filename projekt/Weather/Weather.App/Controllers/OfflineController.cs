using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace Weather.App.Controllers
{
    [OutputCache(Duration = 0)]
    public class OfflineController : Controller
    {
        //
        // GET: /Manifest
        public ActionResult Manifest()
        {
            Response.ContentType = "text/cache-manifest";
            Response.ContentEncoding = Encoding.UTF8;

            return View();
        }
    }
}