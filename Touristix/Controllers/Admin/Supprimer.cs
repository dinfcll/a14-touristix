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
        [HttpPost]
        public void SupprimerDestination(int id)
        {
            DestinationModel destinationmodel = db.Destinations.Find(id);
            db.Destinations.Remove(destinationmodel);
            db.SaveChanges();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public void ConfirmationSupprimerBatiment(int id)
        {
            BatimentModel BatimentModelActif = db.Batiments.Find(id);         
            db.Batiments.Remove(BatimentModelActif);
            db.SaveChanges();            
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public void ConfirmationSupprimerActivite(int id)
        {
            ActiviteModel ActiviteModelActif = db.Activites.Find(id);            
            db.Activites.Remove(ActiviteModelActif);
            db.SaveChanges();            
        }
    }
}
