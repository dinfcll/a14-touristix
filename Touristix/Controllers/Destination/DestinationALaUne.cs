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
        public ActionResult CreerDestinationALaUne(DestinationModel DestinationModelActif)
        {
            CreerDestinationALaUneModel DestinationALaUne = new CreerDestinationALaUneModel(new ALaUneModel(), DestinationModelActif);
            return View("CreerDestinationALaUne", DestinationALaUne);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult CreerDestinationALaUne(CreerDestinationALaUneModel CreerALaUneModelActif)
        {
            CreerALaUneModelActif.ALaUne.Id = CreerALaUneModelActif.Destination.Id;
            CreerALaUneModelActif.ALaUne.DestinationModel = CreerALaUneModelActif.Destination;

            db.ALaUne.Add(CreerALaUneModelActif.ALaUne);
            db.SaveChanges();
            return View("Admin");
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
