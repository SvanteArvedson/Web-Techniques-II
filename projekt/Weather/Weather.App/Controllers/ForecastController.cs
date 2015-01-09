using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weather.App.ViewModels;
using Weather.Domain;
using Weather.Domain.Entities;

namespace Weather.App.Controllers
{
    [OutputCache(Duration = 0)]
    public class ForecastController : Controller
    {
        private IWeatherService _weatherFacade;

        public ForecastController(IWeatherService weatherFacade)
        {
            _weatherFacade = weatherFacade;
        }

        // GET: /Startsida
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Sök/{search} OR /Sök?search=string
        public ActionResult Search(string search)
        {
            if (!String.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
                List<Place> places = _weatherFacade.SearchPlace(search).ToList();
                ForecastSearchViewModel model = null;
                switch (places.Count)
                {
                    case 0:
                        model = new ForecastSearchViewModel() { Places = null };
                        return View(model);
                    case 1:
                        return RedirectToAction("Weather", new { region = places[0].Region, name = places[0].Name });
                    default:
                        model = new ForecastSearchViewModel() { Places = places };
                        return View(model);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: /Väderlek/Sverige/{region}/{*name}
        public ActionResult Weather(string region, string name)
        {
            if(String.IsNullOrEmpty(name))
            {
                throw new HttpException(400, "Bad Request");
            }

            List<Forecast> forecasts = _weatherFacade.GetWeatherForecast(region, name).ToList();

            if (forecasts.Count == 0)
            {
                return new HttpNotFoundResult();
            }
            else
            {
                ForecastWeatherViewModel model = new ForecastWeatherViewModel(
                    forecasts, region, name
                );
                return View(model);
            }
        }
    }
}