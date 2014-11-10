﻿using System;
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
                MettreAJourDestination(DestinationModelActif);

                db.Destinations.Add(DestinationModelActif);
                db.SaveChanges();
                return RedirectToAction("Admin");
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
                MettreAJourBatiment(BatimentModelActif);

                db.Batiments.Add(BatimentModelActif);
                db.SaveChanges();
                return RedirectToAction("admin");
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
                return RedirectToAction("Admin");
            }

            return View("CreerActivite", ActiviteModelActif);
        }
    }
}
