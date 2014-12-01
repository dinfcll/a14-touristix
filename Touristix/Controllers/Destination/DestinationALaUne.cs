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
            CreerDestinationALaUneModel DestinationALaUne = new CreerDestinationALaUneModel(new ALaUneModel(), DestinationModelActif);
            return View("CreerDestinationALaUne", DestinationALaUne);
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
            if (ModelState.IsValid)
            {
                db.Entry(ALaUneModelActif).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View("ModifierALaUne", ALaUneModelActif.ALaUneId);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public void ConfirmerSupprimerDestinationALaUne(int id)
        {
            ALaUneModel ALaUneModelActif = db.ALaUne.Find(id);
            db.ALaUne.Remove(ALaUneModelActif);
            db.SaveChanges();
        }
    }
}
