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
        [Authorize(Roles = "admin")]
        public ActionResult ModifierDestination(int id = 0)
        {
            DestinationModel DestinationModelActif = db.Destinations.Find(id);
            if (DestinationModelActif == null)
            {
                return HttpNotFound();
            }

            return View(DestinationModelActif);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult ModifierDestination(DestinationModel Destination)
        {
            if (ModelState.IsValid)
            {
                Destination.BatimentIds = "";
                int DernierBatiment = Convert.ToInt32(Request["DernierBatiment"]);
                if (DernierBatiment >= 1)
                {
                    for (int B = 0; B < DernierBatiment; B++)
                    {
                        int Id = Convert.ToInt32(Request["Batiment" + B]);
                        Destination.BatimentIds += Id + ";";
                    }
                }

                db.Entry(Destination).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View(Destination.Id);
        }

        [Authorize(Roles = "admin")]
        public ActionResult ModifierBatiment(int id = 0)
        {
            BatimentModel BatimentModelActif = db.Batiments.Find(id);
            if (BatimentModelActif == null)
            {
                return HttpNotFound();
            }
            return View(BatimentModelActif);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult ModifierBatiment(BatimentModel BatimentModelActif)
        {
            if (ModelState.IsValid)
            {
                db.Entry(BatimentModelActif).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View(BatimentModelActif);
        }

        [Authorize(Roles = "admin")]
        public ActionResult ModifierActivite(int id = 0)
        {
            ActiviteModel ActiviteModelActif = db.Activites.Find(id);
            if (ActiviteModelActif == null)
            {
                return HttpNotFound();
            }
            return View(ActiviteModelActif);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult ModifierActivite(ActiviteModel ActiviteModelActif)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ActiviteModelActif).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View(ActiviteModelActif);
        }
    }
}
