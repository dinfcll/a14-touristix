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
    public partial class AdminController : Controller
    {
        [Authorize(Roles = "admin")]
        public ActionResult ModifierDestination(int id = 0)
        {
            DestinationModel DestinationModelActif = db.Destinations.Find(id);
            if (DestinationModelActif == null)
            {
                return HttpNotFound();
            }

            return View("ModifierDestination", DestinationModelActif);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult ModifierDestination(DestinationModel DestinationModelActif)
        {
            if (ModelState.IsValid)
            {
                Assistant.MettreAJourDestination(DestinationModelActif, Request);

                db.Entry(DestinationModelActif).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("ModifierDestination", DestinationModelActif.Id);
        }

        [Authorize(Roles = "admin")]
        public ActionResult ModifierBatiment(int id = 0)
        {
            BatimentModel BatimentModelActif = db.Batiments.Find(id);

            if (BatimentModelActif == null)
            {
                return HttpNotFound();
            }

            if (BatimentModelActif.TypeURL == "http")
            {
                BatimentModelActif.URL = BatimentModelActif.URL.Remove(0, 7);
            }
            else
            {
                BatimentModelActif.URL = BatimentModelActif.URL.Remove(0, 8);
            }

            return View("ModifierBatiment", BatimentModelActif);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult ModifierBatiment(BatimentModel BatimentModelActif)
        {
            if (ModelState.IsValid)
            {
                Assistant.MettreAJourBatiment(BatimentModelActif);

                db.Entry(BatimentModelActif).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("ModifierBatiment", BatimentModelActif);
        }

        [Authorize(Roles = "admin")]
        public ActionResult ModifierActivite(int id = 0)
        {
            ActiviteModel ActiviteModelActif = db.Activites.Find(id);
            if (ActiviteModelActif == null)
            {
                return HttpNotFound();
            }
            return View("ModifierActivite", ActiviteModelActif);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult ModifierActivite(ActiviteModel ActiviteModelActif)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ActiviteModelActif).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("ModifierActivite", ActiviteModelActif);
        }
    }
}
