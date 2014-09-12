using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Touristix.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            HttpClient client = new HttpClient();
            string monstring;
            client.BaseAddress = new Uri("http://api.worldweatheronline.com/free/v1/weather.ashx");

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));


            HttpResponseMessage response = client.GetAsync("api.worldweatheronline.com/free/v1/weather.ashx?q=London&format=json&key=%20c661a3b9dfba00ee29ad03214e1e4ef88e9ed7ca").Result;
            if (response.IsSuccessStatusCode)
            {
                ObjetRecu objrecu = response.Content.ReadAsAsync<IEnumerable<ObjetRecu>>().Result;
                foreach (var x in yourcustomobjects)
                {
                    //Call your store method and pass in your own object
                    monstring += x;
                }
            }

            return View();
        }

    }
}
