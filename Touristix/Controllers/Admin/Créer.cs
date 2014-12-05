using System.Web.Mvc;
using Touristix.Models;

namespace Touristix.Controllers
{
    public partial class AdminController
    {
        [Authorize(Roles="admin")]
        public ActionResult CreerDestination()
        {
            return View("CreerDestination");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult CreerDestination(DestinationModel DestinationModelActif)
        {
            if (ModelState.IsValid)
            {
                Assistant.MettreAJourDestination(DestinationModelActif, Request);

                db.Destinations.Add(DestinationModelActif);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("CreerDestination", DestinationModelActif);
        }

        [Authorize(Roles = "admin")]
        public ActionResult CreerBatiment()
        {
            return View("CreerBatiment");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult CreerBatiment(BatimentModel BatimentModelActif)
        {
            if (ModelState.IsValid)
            {
                Assistant.MettreAJourBatiment(BatimentModelActif);

                db.Batiments.Add(BatimentModelActif);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("CreerBatiment", BatimentModelActif);
        }

        [Authorize(Roles = "admin")]
        public ActionResult CreerActivite()
        {
            return View("CreerActivite");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult CreerActivite(ActiviteModel ActiviteModelActif)
        {
            if (ModelState.IsValid)
            {
                db.Activites.Add(ActiviteModelActif);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("CreerActivite", ActiviteModelActif);
        }
    }
}
