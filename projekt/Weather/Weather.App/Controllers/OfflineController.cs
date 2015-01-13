using System.Text;
using System.Web.Mvc;

namespace Weather.App.Controllers
{
    /// <summary>
    /// Controller for offline files
    /// </summary>
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