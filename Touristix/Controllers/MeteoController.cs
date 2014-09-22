using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Touristix.Models;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace Touristix.Controllers
{
    public class MeteoController : Controller
    {
        //
        // GET: /Meteo/

        [HttpGet]
        public ActionResult MeteoForm()
        {
            return View("MeteoRecherche");
        }

        [HttpPost]
        public ActionResult MeteoForm(Ville ville)
        {
            var client = new HttpClient();
            var temperature = new Temperature();
            client.BaseAddress = new Uri("http://api.openweathermap.org");
            var response = client.GetAsync("/data/2.5/weather?q=" + ville.VilleCible).Result;

            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                temperature = new JavaScriptSerializer().Deserialize<Temperature>(responseBody);

                if (temperature.cod == 200)
                {
                    FormatterDonnees(temperature);
                    ViewData["Verif"] = null;
                    return View(temperature);
                }

            }
                
            ViewData["Verif"] = "Erreur";
            return View("MeteoRecherche");             
        }

        public void FormatterDonnees(Temperature temperature)
        {
            string icone = temperature.weather[0].icon;
            temperature.main.temp = (int)(temperature.main.temp - 273);
            temperature.main.temp_min = (int)(temperature.main.temp_min - 273);
            temperature.main.temp_max = (int)(temperature.main.temp_max - 273);
            temperature.weather[0].icon = "http://openweathermap.org/img/w/" + icone + ".png";
            temperature.main.pressure /= 10;
            temperature.wind.speed = Math.Round((3.6 * temperature.wind.speed), 0);
            temperature.wind.deg = (Math.Round((temperature.wind.deg / 45), 0));
            FormatterDirectionVent(temperature.wind);
            
        }
        public void FormatterDirectionVent(Wind vent)
        {
            switch ((int)vent.deg)
            {
                case 1: vent.DirectionVent = "Nord-Est";
                    break;
                case 2: vent.DirectionVent = "Est";
                    break;
                case 3: vent.DirectionVent = "Sud-Est";
                    break;
                case 4: vent.DirectionVent = "Sud";
                    break;
                case 5: vent.DirectionVent = "Sud-Ouest";
                    break;
                case 6: vent.DirectionVent = "Ouest";
                    break;
                case 7: vent.DirectionVent = "Nord-Ouest";
                    break;
                default: vent.DirectionVent = "Nord";
                    break;
            }
        }
    }
}
