using System;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Touristix.Models;

namespace Touristix.Controllers
{
    public partial class DestinationController : Controller
    {
        public ActionResult InformationDestination(int id = 0)
        {
            DestinationModel DestinationModelActif = db.Destinations.Find(id);
            var client = new HttpClient {BaseAddress = new Uri("http://api.openweathermap.org")};
            var response = client.GetAsync("/data/2.5/weather?q=" + DestinationModelActif.Ville).Result;

            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                var temperature = new JavaScriptSerializer().Deserialize<Temperature>(responseBody);

                if (temperature.cod == 200)
                {
                    FormatterDonnees(temperature);
                    ViewData["Verif"] = "";                    
                    return View(new Tuple<DestinationModel, Temperature >(DestinationModelActif, temperature));                    
                }
            }
            ViewData["Verif"] = "Erreur";
            return View(new Tuple<DestinationModel, Temperature>(DestinationModelActif, null));
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
            DescAnglaisaFrancais(temperature.weather[0]);

        }

        public void FormatterDirectionVent(int ind, Wind vent)
        {
            string[] tVent = { "Nord-Est", "Est", "Sud-Est", "Sud", "Sud-Ouest", "Ouest", "Nord-Ouest", "Nord" };
            vent.DirectionVent = tVent[ind];
        }

        public void DescAnglaisaFrancais(Weather temp)
        {
            bool Trouve = false;
            int i = 0;
            string[,] tDescription =    {{"thunderstorm with light rain","Orages avec faible pluie"},
                                         {"thunderstorm with rain","Orages avec pluie"},
                                         {"thunderstorm with heavy rain","Orages avec forte pluie"},
                                         {"light thunderstorm ","Faible orages"},
                                         {"thunderstorm","Orages"},
                                         {"heavy thunderstorm","Fortes orages"},
                                         {"ragged thunderstorm","Orages violentes"},
                                         {"thunderstorm with light drizzle","Orages avec faible bruine"},
                                         {"thunderstorm with drizzle","Orages avec bruine"},
                                         {"thunderstorm with heavy drizzle","Orages avec forte bruine"},
                                         {"light intensity drizzle","Faible bruine"},
                                         {"drizzle","Bruine"},
                                         {"heavy intensity drizzle","Bruine de forte intensitée"},
                                         {"light intensity drizzle rain","Bruine de faible intensitée mêlée de pluie"},
                                         {"drizzle rain","Bruine mêlée de pluie"},
                                         {"heavy intensity drizzle rain","Bruine de forte intensitée mêlée de pluie"},
                                         {"shower rain and drizzle","Averses de pluie mêlée de bruine"},
                                         {"heavy shower rain and drizzle","Fortes averses de pluie mêlée de bruine"},
                                         {"shower drizzle","Averses de bruines"},
                                         {"light rain","Faible pluie"},
                                         {"moderate rain","Pluie modérée"},
                                         {"heavy intensity rain","Pluie de forte intensitée"},
                                         {"very heavy rain","Très forte pluie"},
                                         {"extreme rain","Pluie extrème"},
                                         {"freezing rain","Pluie verglaçante"},
                                         {"light intensity shower rain","Averses de pluie de faible intensité"},
                                         {"shower rain","Averses de pluie"},
                                         {"heavy intensity shower rain","Averses de pluie de forte intensité"},
                                         {"ragged shower rain","Averses de pluie violente"},
                                         {"light snow","Faible neige"},
                                         {"snow","Neige"},
                                         {"heavy snow","Forte neige"},
                                         {"sleet","Neige fondante"},
                                         {"shower sleet","Averse de neige fondante"},
                                         {"light rain and snow","Légère pluie mêlée de neige"},
                                         {"rain and snow","Pluie mêlée de neige"},
                                         {"light shower snow","Faibles averses de neige"},
                                         {"shower snow","Averses de neige"},
                                         {"heavy shower snow","Fortes averses de neige"},
                                         {"mist","Brume"},
                                         {"smoke","Fumée"},
                                         {"haze","Brume"},
                                         {"sand, dust whirls","Tempête de sable"},
                                         {"fog","Brouillard"},
                                         {"sand","Sable"},
                                         {"dust","Poussière"},
                                         {"volcanic ash","Éruption volcanique"},
                                         {"squalls","Rafales"},
                                         {"clear sky","Ciel dégagé"},
                                         {"few clouds","Quelques nuages"},
                                         {"scattered Nuages dispersés",""},
                                         {"broken clouds","Partiellement nuageux"},
                                         {"overcast clouds","Ciel couvert"},
                                         {"tornado","Tornade"},
                                         {"tropical storm","Tempête Tropical"},
                                         {"hurricane","Ouragan"},
                                         {"cold","Froid"},
                                         {"hot","Chaud"},
                                         {"windy","Venteux"},
                                         {"hail","Grêle"},
                                         {"calm ","Calme"},
                                         {"light breeze","Légère Brise"},
                                         {"gentle breeze","Brise"},
                                         {"moderate breeze","Brise modérée"},
                                         {"fresh breeze","Brise fraîche"},
                                         {"strong breeze","Forte Brise"},
                                         {"high wind, near gale","Très forts vents"},
                                         {"gale","Tempête"},
                                         {"severe gale","Tempête violente"},
                                         {"storm","Tenpête"},
                                         {"violent storm","Tempête violente"},
                                         {"hurricane","Ouragan"}};
           
            while (!Trouve && i < 72)
            {
                if (tDescription[i, 0] == temp.description)
                {
                    Trouve = true;
                }
                else
                {
                    i++;
                }
            }

            temp.description = Trouve ? tDescription[i, 1] : "Description non Disponible";
        }
    }
}
