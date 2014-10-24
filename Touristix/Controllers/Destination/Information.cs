using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Touristix.Models;
using System.Web.Script.Serialization;
using System.Net.Http;

namespace Touristix.Controllers
{
    public partial class DestinationController : Controller
    {
        public ActionResult InformationDestination(int id = 0)
        {
            DestinationModel DestinationModelActif = db.Destinations.Find(id);
            var temperature = new Temperature();
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://api.openweathermap.org");
            var response = client.GetAsync("/data/2.5/weather?q=" + DestinationModelActif.Ville).Result;

            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                temperature = new JavaScriptSerializer().Deserialize<Temperature>(responseBody);

                if (temperature.cod == 200)
                {
                    FormatterDonnees(temperature);
                    ViewData["Verif"] = "";                    
                    return View(new Tuple<DestinationModel, Temperature >(DestinationModelActif, temperature));                    
                }
            }

            ViewData["Verif"] = "Erreur";           
            return View(DestinationModelActif);
        }

        public ActionResult InformationBatiment(int id = 0)
        {
            BatimentModel BatimentModelActif = db.Batiments.Find(id);
            if (BatimentModelActif == null)
            {
                return HttpNotFound();
            }
            return View(BatimentModelActif);
        }

        public ActionResult InformationActivite(int id = 0)
        {
            ActiviteModel ActiviteModelActif = db.Activites.Find(id);
            if (ActiviteModelActif == null)
            {
                return HttpNotFound();
            }
            return View(ActiviteModelActif);
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
