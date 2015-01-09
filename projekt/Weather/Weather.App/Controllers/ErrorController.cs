using System.Web.Mvc;

namespace Weather.App.Controllers
{
    [OutputCache(Duration = 0)]
    public class ErrorController : Controller
    {
        // GET: /Error/{code}
        public ActionResult Error(int code)
        {
            if (code == 400)
            {
                return View("Error400");
            }
            else if (code == 404)
            {
                return View("Error404");
            }
            else
            {
                return View("Error500");
            }
        }
	}
}