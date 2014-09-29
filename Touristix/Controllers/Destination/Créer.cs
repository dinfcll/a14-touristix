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
                DestinationModelActif.BatimentIds = "";
                int DernierBatiment = Convert.ToInt32(Request["DernierBatiment"]);
                if (DernierBatiment >= 1)
                {
                    for (int B = 0; B < DernierBatiment; B++)
                    {
                        int Id = Convert.ToInt32(Request["Batiment" + B]);
                        DestinationModelActif.BatimentIds += Id + ";";
                    }
                }

                DestinationModelActif.ActiviteIds = "";
                int DerniereActivite = Convert.ToInt32(Request["DerniereActivite"]);
                if (DerniereActivite >= 1)
                {
                    for (int B = 0; B < DerniereActivite; B++)
                    {
                        int Id = Convert.ToInt32(Request["Activite" + B]);
                        DestinationModelActif.ActiviteIds += Id + ";";
                    }
                }

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
