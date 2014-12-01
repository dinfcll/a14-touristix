using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Touristix.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Touristix.Controllers
{
    public partial class DestinationController : Controller
    {
        [Authorize(Roles = "admin")]
        public ActionResult CreerDestinationALaUne(DestinationModel DestinationModelActif)
        {
            ViewBag.Destination = DestinationModelActif;
            return View("CreerDestinationALaUne", new ALaUneModel());
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult CreerDestinationALaUne(ALaUneModel ALaUneModelActif)
        {
            if (ModelState.IsValid)
            {
                db.ALaUne.Add(ALaUneModelActif);
                db.SaveChanges();

                return RedirectToAction("Admin");
            }

            return View("CreerDestinationALaUne", ALaUneModelActif);
        }

        [Authorize(Roles = "admin")]
        public ActionResult ModifierALaUne(int id = 0)
        {
            ALaUneModel ALaUneModelActif = db.ALaUne.Find(id);
            if (ALaUneModelActif == null)
            {
                return HttpNotFound();
            }

            return View("ModifierALaUne", ALaUneModelActif);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult ModifierALaUne(ALaUneModel ALaUneModelActif)
        {
            var AlaUneDB = db.ALaUne.Where(a => a.ALaUneId == ALaUneModelActif.ALaUneId).First();
            ALaUneModelActif.Id = AlaUneDB.Id;
            ALaUneModelActif.DestinationModel = AlaUneDB.DestinationModel;

            if (ModelState.IsValid)
            {
                db.Entry(AlaUneDB).CurrentValues.SetValues(ALaUneModelActif);
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View("ModifierALaUne", ALaUneModelActif.ALaUneId);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public void ConfirmerSupprimerDestinationALaUne(int id)
        {
            var ALaUneModelActif = db.ALaUne.Find(id);
            db.ALaUne.Remove(ALaUneModelActif);
            db.SaveChanges();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public void ConfirmerSupprimerDestinationALaUneDepuisCleEtrangere(int id)
        {
            var ALaUneModelActif = db.ALaUne.Where(a => a.Id == id).First();
            db.ALaUne.Remove(ALaUneModelActif);
            db.SaveChanges();
        }
    }
}
