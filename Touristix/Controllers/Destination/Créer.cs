using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Touristix.Models;

namespace Touristix.Controllers
{
    public partial class DestinationController : Controller
    {
        public ActionResult CreerDestination()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreerDestination(DestinationModel DestinationModelActif)
        {
            if (ModelState.IsValid)
            {
                MettreAJourDestination(DestinationModelActif);

                db.Destinations.Add(DestinationModelActif);
                db.SaveChanges();
                return RedirectToAction("Admin");
            }

            return View(DestinationModelActif);
        }

        public ActionResult CreerBatiment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreerBatiment(BatimentModel BatimentModelActif)
        {
            if (ModelState.IsValid)
            {
                db.Batiments.Add(BatimentModelActif);
                db.SaveChanges();
                return RedirectToAction("Admin");
            }

            return View(BatimentModelActif);
        }

        public ActionResult CreerActivite()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreerActivite(ActiviteModel ActiviteModelActif)
        {
            if (ModelState.IsValid)
            {
                db.Activites.Add(ActiviteModelActif);
                db.SaveChanges();
                return RedirectToAction("Admin");
            }

            return View(ActiviteModelActif);
        }
    }
}
