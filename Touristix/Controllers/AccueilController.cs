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
        //
        // GET: /Accueil/

        public ActionResult Index(string url)
        {
            if (url == null)
            {
                url = Server.MapPath("~/Images/ImagesAccueil/");
            }
            
            var modelImagesAccueil = new ImagesAccueilModel(url);

            return View("Index", modelImagesAccueil);
        }
    }
}
