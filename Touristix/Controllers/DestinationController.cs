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
                RechercheParNom(ref Destinations, DestinationNom);

            if (!string.IsNullOrEmpty(DestinationPays))
                RechercheParPays(ref Destinations, DestinationPays);

            if (!string.IsNullOrEmpty(DestinationRegion))
                RechercheParRegion(ref Destinations, DestinationRegion);

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

        public IQueryable<DestinationModel> RechercheParNom(ref IQueryable<DestinationModel> Destinations, string DestinationNom)
        {
            if (!String.IsNullOrEmpty(DestinationNom))
            {
                Destinations = Destinations.Where(s => s.Nom.Contains(DestinationNom));
            }

            return Destinations;
        }

        public IQueryable<DestinationModel> RechercheParPays(ref IQueryable<DestinationModel> Destinations, string DestinationPays)
        {
            if (!String.IsNullOrEmpty(DestinationPays))
            {
                Destinations = Destinations.Where(s => s.Pays.Contains(DestinationPays));
            }

            return Destinations;
        }

        public IQueryable<DestinationModel> RechercheParRegion(ref IQueryable<DestinationModel> Destinations, string DestinationRegion)
        {
            if (!String.IsNullOrEmpty(DestinationRegion))
            {
                Destinations = Destinations.Where(s => s.Region.Contains(DestinationRegion));
            }

            return Destinations;
        }

        //
        // GET: /Destination/Admin

        public ActionResult Admin()
        {
            return View(db.Destinations.ToList());
        }

        //
        // GET: /Destination/Information/

        public ActionResult Information(int id = 0)
        {
            DestinationModel destinationmodel = db.Destinations.Find(id);
            if (destinationmodel == null)
            {
                return HttpNotFound();
            }
            return View(destinationmodel);
        }

        //
        // GET: /Destination/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Destination/Create

        [HttpPost]
        public ActionResult Create(DestinationModel destinationmodel)
        {
            if (ModelState.IsValid)
            {
                db.Destinations.Add(destinationmodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(destinationmodel);
        }

        //
        // GET: /Destination/Edit/5

        public ActionResult Edit(int id = 0)
        {
            DestinationModel destinationmodel = db.Destinations.Find(id);
            if (destinationmodel == null)
            {
                return HttpNotFound();
            }
            return View(destinationmodel);
        }

        //
        // POST: /Destination/Edit/5

        [HttpPost]
        public ActionResult Edit(DestinationModel destinationmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(destinationmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(destinationmodel);
        }

        //
        // GET: /Destination/Delete/5

        public ActionResult Delete(int id = 0)
        {
            DestinationModel destinationmodel = db.Destinations.Find(id);
            if (destinationmodel == null)
            {
                return HttpNotFound();
            }
            return View(destinationmodel);
        }

        //
        // POST: /Destination/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DestinationModel destinationmodel = db.Destinations.Find(id);
            db.Destinations.Remove(destinationmodel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}