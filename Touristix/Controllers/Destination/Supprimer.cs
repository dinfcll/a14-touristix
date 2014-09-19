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
        public ActionResult SupprimerDestination(int id = 0)
        {
            DestinationModel destinationmodel = db.Destinations.Find(id);
            if (destinationmodel == null)
            {
                return HttpNotFound();
            }
            return View(destinationmodel);
        }

        [HttpPost, ActionName("SupprimerDestination")]
        public ActionResult ConfirmationSupprimerDestination(int id)
        {
            DestinationModel destinationmodel = db.Destinations.Find(id);
            db.Destinations.Remove(destinationmodel);

            ChaineBatiment ProchainBatiment = db.ChaineBatiments.Find(destinationmodel.ProchainBatimentId);
            do
            {
                BatimentModel BatimentModelActif = db.Batiments.Find(ProchainBatiment.BatimentId);
                db.ChaineBatiments.Remove(ProchainBatiment);

                ProchainBatiment = db.ChaineBatiments.Find(ProchainBatiment.ProchainId);
            }
            while (ProchainBatiment != null);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SupprimerBatiment(int id = 0)
        {
            BatimentModel BatimentModelActif = db.Batiments.Find(id);
            if (BatimentModelActif == null)
            {
                return HttpNotFound();
            }
            return View(BatimentModelActif);
        }

        [HttpPost, ActionName("SupprimerBatiment")]
        public ActionResult ConfirmationSupprimerBatiment(int id)
        {
            BatimentModel BatimentModelActif = db.Batiments.Find(id);
            db.Batiments.Remove(BatimentModelActif);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SupprimerActivite(int id = 0)
        {
            ActiviteModel ActiviteModelActif = db.Activites.Find(id);
            if (ActiviteModelActif == null)
            {
                return HttpNotFound();
            }
            return View(ActiviteModelActif);
        }

        [HttpPost, ActionName("SupprimerActivite")]
        public ActionResult ConfirmationSupprimerActivite(int id)
        {
            ActiviteModel ActiviteModelActif = db.Activites.Find(id);
            db.Activites.Remove(ActiviteModelActif);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
