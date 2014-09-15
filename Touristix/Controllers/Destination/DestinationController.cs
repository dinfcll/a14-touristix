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
        private DestinationDBContext db = new DestinationDBContext();

        //
        // GET: /Destination/
        public ActionResult Index(string DestinationNom = "", string DestinationPays = "", string DestinationRegion = "", int Trier = 0)
        {
            IQueryable<DestinationModel> Destinations = from m in db.Destinations
                                                        select m;

            if (!string.IsNullOrEmpty(DestinationNom))
                Destinations = Destinations.Where(s => s.Nom.Contains(DestinationNom));

            if (!string.IsNullOrEmpty(DestinationPays))
                Destinations = Destinations.Where(s => s.Pays.Contains(DestinationPays));

            if (!string.IsNullOrEmpty(DestinationRegion))
                Destinations = Destinations.Where(s => s.Region.Contains(DestinationRegion));

            IQueryable<IGrouping<string, DestinationModel>> DestinationRecu;

            switch (Trier)
            {
                case 0:
                default:
                DestinationRecu = Destinations.GroupBy(item => item.Pays);
                    break;
                case 1:
                DestinationRecu = Destinations.GroupBy(item => item.Region);
                    break;
                case 2:
                DestinationRecu = Destinations.GroupBy(item => item.Ville);
                    break;
            }

            DestinationModel[] Array5DerniereDestination = db.Destinations
                    .OrderByDescending(m => m.Nom)
                    .Take(5)
                    .ToArray();

            return View(new Tuple<DestinationModel[], IQueryable<IGrouping<string, DestinationModel>>>(Array5DerniereDestination, DestinationRecu));
        }

        //
        // GET: /Destination/Admin
        public ActionResult Admin()
        {
            AdminitrationList NouvelleList = new AdminitrationList();
            NouvelleList.ListDestinationModel = db.Destinations.ToList();
            NouvelleList.ListBatimentModel = db.Batiments.ToList();
            NouvelleList.ListActiviteModel = db.Activites.ToList();
            return View(NouvelleList);
        }
                
        public JsonResult ObtenirListeBatiment(string ID)
        {
            int IDNumber = Convert.ToInt32(ID);
            List<SelectListItem> ListBatiment = new List<SelectListItem>();

            IQueryable<BatimentModel> Batiments = from m in db.Batiments
                                                  select m;

            ListBatiment.Add(new SelectListItem { Text = "", Value = "" });

            foreach (BatimentModel BatimentActif in Batiments)
            {
                ListBatiment.Add(new SelectListItem { Text = BatimentActif.Nom, Value = BatimentActif.ID.ToString() });
            }
            return Json(new SelectList(ListBatiment, "Value", "Text"));
        }

        public JsonResult ObtenirBatiment(string ID)
        {
            BatimentModel Batiment = db.Batiments.Find(Convert.ToInt32(ID));

            return Json(Batiment);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}