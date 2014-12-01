using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Touristix.Models;

namespace Touristix.Controllers
{
    public class AccueilController : Controller
    {
        private DestinationDBContext db = new DestinationDBContext();

        public ActionResult Index(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                url = Server.MapPath("~/Images/Destinations/");
            }
            
            var modelImagesAccueil = new ImagesAccueilModel(url);

            modelImagesAccueil.ListALaUne = db.ALaUne.ToList();

            return View("Index", modelImagesAccueil);
        }
    }
}
