using System.Linq;
using System.Web.Mvc;
using Touristix.Models;

namespace Touristix.Controllers
{
    public class AccueilController : Controller
    {
        private DestinationDBContext db = new DestinationDBContext();

        public ActionResult Index()
        {
            var modelImagesAccueil = new ImagesAccueilModel();

            modelImagesAccueil.ListALaUne = db.ALaUne.ToList();

            return View("Index", modelImagesAccueil);
        }
    }
}
