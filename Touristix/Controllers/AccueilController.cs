using System.Web.Mvc;
using Touristix.Models;

namespace Touristix.Controllers
{
    public class AccueilController : Controller
    {
        //
        // GET: /Accueil/

        public ActionResult Index(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                url = Server.MapPath("~/Images/Destinations/");
            }
            
            var modelImagesAccueil = new ImagesAccueilModel(url);

            return View("Index", modelImagesAccueil);
        }
    }
}
