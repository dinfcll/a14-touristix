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
    public class DestinationController : Controller
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

        //
        // GET: /Destination/InformationDestination/
        public ActionResult InformationDestination(int id = 0)
        {
            DestinationModel DestinationModelActif = db.Destinations.Find(id);
            if (DestinationModelActif == null)
            {
                return HttpNotFound();
            }
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

        #region Créer

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

        #endregion

        #region Modifier

        //
        // GET: /Destination/ModifierDestination
        public ActionResult ModifierDestination(int id = 0)
        {
            DestinationModel DestinationModelActif = db.Destinations.Find(id);
            if (DestinationModelActif == null)
            {
                return HttpNotFound();
            }
            return View(DestinationModelActif);
        }

        //
        // POST: /Destination/ModifierDestination
        [HttpPost]
        public ActionResult ModifierDestination(DestinationModel DestinationModelActif)
        {
            if (ModelState.IsValid)
            {
                db.Entry(DestinationModelActif).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View(DestinationModelActif);
        }

        //
        // GET: /Destination/ModifierBatiment
        public ActionResult ModifierBatiment(int id = 0)
        {
            BatimentModel BatimentModelActif = db.Batiments.Find(id);
            if (BatimentModelActif == null)
            {
                return HttpNotFound();
            }
            return View(BatimentModelActif);
        }

        //
        // POST: /Destination/ModifierDestination
        [HttpPost]
        public ActionResult ModifierBatiment(BatimentModel BatimentModelActif)
        {
            if (ModelState.IsValid)
            {
                db.Entry(BatimentModelActif).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View(BatimentModelActif);
        }

        //
        // GET: /Destination/ModifierActivites
        public ActionResult ModifierActivite(int id = 0)
        {
            ActiviteModel ActiviteModelActif = db.Activites.Find(id);
            if (ActiviteModelActif == null)
            {
                return HttpNotFound();
            }
            return View(ActiviteModelActif);
        }

        //
        // POST: /Destination/ModifierActivites
        [HttpPost]
        public ActionResult ModifierActivite(ActiviteModel ActiviteModelActif)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ActiviteModelActif).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View(ActiviteModelActif);
        }

        #endregion

        #region Supprimer

        //
        // GET: /Destination/SupprimerDestination
        public ActionResult SupprimerDestination(int id = 0)
        {
            DestinationModel destinationmodel = db.Destinations.Find(id);
            if (destinationmodel == null)
            {
                return HttpNotFound();
            }
            return View(destinationmodel);
        }

        //
        // POST: /Destination/SupprimerDestination
        [HttpPost, ActionName("SupprimerDestination")]
        public ActionResult ConfirmationSupprimerDestination(int id)
        {
            DestinationModel destinationmodel = db.Destinations.Find(id);
            db.Destinations.Remove(destinationmodel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Destination/SupprimerBatiment
        public ActionResult SupprimerBatiment(int id = 0)
        {
            BatimentModel BatimentModelActif = db.Batiments.Find(id);
            if (BatimentModelActif == null)
            {
                return HttpNotFound();
            }
            return View(BatimentModelActif);
        }

        //
        // POST: /Destination/SupprimerBatiment
        [HttpPost, ActionName("SupprimerBatiment")]
        public ActionResult ConfirmationSupprimerBatiment(int id)
        {
            BatimentModel BatimentModelActif = db.Batiments.Find(id);
            db.Batiments.Remove(BatimentModelActif);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Destination/SupprimerActivite
        public ActionResult SupprimerActivite(int id = 0)
        {
            ActiviteModel ActiviteModelActif = db.Activites.Find(id);
            if (ActiviteModelActif == null)
            {
                return HttpNotFound();
            }
            return View(ActiviteModelActif);
        }

        //
        // POST: /Destination/SupprimerActivite
        [HttpPost, ActionName("SupprimerActivite")]
        public ActionResult ConfirmationSupprimerActivite(int id)
        {
            ActiviteModel ActiviteModelActif = db.Activites.Find(id);
            db.Activites.Remove(ActiviteModelActif);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}