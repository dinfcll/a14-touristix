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
                    ViewData["Verif"] = "";
                    return View(temperature);
                }

            }
                
            ViewData["Verif"] = "Erreur";
            return View("MeteoRecherche");             
        }

        public void FormatterDonnees(Temperature temperature)
        {
            string icone = temperature.weather[0].icon;
            temperature.main.temp = (int)(temperature.main.temp - 273.2);
            temperature.main.temp_min = (int)(temperature.main.temp_min - 273.2);
            temperature.main.temp_max = (int)(temperature.main.temp_max - 273.2);
            temperature.weather[0].icon = "http://openweathermap.org/img/w/" + icone + ".png";
            temperature.main.pressure /= 10;
            temperature.wind.speed = Math.Round((3.6 * temperature.wind.speed), 0);
            int indDegree = (int)(Math.Round((temperature.wind.deg / 45), 0));
            if (indDegree == 8)
            {
                indDegree = 0;
            }
            FormatterDirectionVent(indDegree, temperature.wind);

        }

        public void FormatterDirectionVent(int ind, Wind vent)
        {
            string[] tVent = { "Nord-Est", "Est", "Sud-Est", "Sud", "Sud-Ouest", "Ouest", "Nord-Ouest", "Nord" }; 
            vent.DirectionVent = tVent[ind];
        }
    }
}

