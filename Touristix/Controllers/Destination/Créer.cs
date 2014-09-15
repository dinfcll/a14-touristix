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
        //
        // GET: /Destination/CreerDestination
        public ActionResult CreerDestination()
        {
            return View();
        }

        //
        // POST: /Destination/CreerDestination
        [HttpPost]
        public ActionResult CreerDestination(DestinationModel DestinationModelActif)
        {
            if (ModelState.IsValid)
            {
                db.Destinations.Add(DestinationModelActif);
                db.SaveChanges();
                return RedirectToAction("Admin");
            }

            return View(DestinationModelActif);
        }

        //
        // GET: /Destination/CreerBatiment
        public ActionResult CreerBatiment()
        {
            return View();
        }

        //
        // POST: /Destination/CreerBatiment
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

        //
        // GET: /Destination/CreerActivite
        public ActionResult CreerActivite()
        {
            return View();
        }

        //
        // POST: /Destination/CreerActivite
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