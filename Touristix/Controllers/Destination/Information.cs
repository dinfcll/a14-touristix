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
        // GET: /Destination/InformationDestination/
        public ActionResult InformationDestination(int id = 0)
        {
            DestinationModel DestinationModelActif = db.Destinations.Find(id);
            return View(DestinationModelActif);
        }

        //
        // GET: /Destination/InformationBatiment/
        public ActionResult InformationBatiment(int id = 0)
        {
            BatimentModel BatimentModelActif = db.Batiments.Find(id);
            if (BatimentModelActif == null)
            {
                return HttpNotFound();
            }
            return View(BatimentModelActif);
        }

        //
        // GET: /Destination/InformationDestination/
        public ActionResult InformationActivite(int id = 0)
        {
            ActiviteModel ActiviteModelActif = db.Activites.Find(id);
            if (ActiviteModelActif == null)
            {
                return HttpNotFound();
            }
            return View(ActiviteModelActif);
        }
    }
}