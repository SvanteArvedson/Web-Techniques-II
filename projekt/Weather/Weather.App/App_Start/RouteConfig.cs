using System.Web.Mvc;
using System.Web.Routing;

namespace Weather.App
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Home", 
                "", 
                new { controller = "Forecast", action = "Index" }, 
                new { httpMethod = new HttpMethodConstraint("GET") }
            );

            routes.MapRoute("Login",
                "Konto/Inloggning",
                new { controller = "Account", action = "Login" }
            );

            routes.MapRoute("CreateAccount",
                "Konto/SkapaKonto",
                new { controller = "Account", action = "Create" },
                new { httpMethod = new HttpMethodConstraint("POST") }
            );

            routes.MapRoute("Error", 
                "Fel/{code}", 
                new { controller = "Error", action = "Error" },
                new { httpMethod = new HttpMethodConstraint("GET"), code = "^400$|^404$|^500$" }
            );

            routes.MapRoute("SearchUglyUrl", 
                "Sök", 
                new { controller = "Forecast", action = "Search" }, 
                new { httpMethod = new HttpMethodConstraint("GET") }
            );

            routes.MapRoute("SearchPrettyUrl", 
                "Sök/{search}", 
                new { controller = "Forecast", action = "Search" }, 
                new { httpMethod = new HttpMethodConstraint("GET") }
            );

            routes.MapRoute("Weather", 
                "Väderlek/Sverige/{region}/{*name}", 
                new { controller = "Forecast", action = "Weather" }, 
                new { httpMethod = new HttpMethodConstraint("GET") }
            );
        }
    }
}