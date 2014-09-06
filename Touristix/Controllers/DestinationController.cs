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

        public ActionResult Index()
        {
            return View(db.Destinations.ToList());
        }

        public ActionResult Recherche(string DestinationNom, string DestinationPays, string DestinationRegion)
        {
            var Destinations = from m in db.Destinations
                         select m;

            if (!string.IsNullOrEmpty(DestinationNom))
                RechercheParNom(Destinations, DestinationNom);

            if (!string.IsNullOrEmpty(DestinationPays))
                RechercheParPays(Destinations, DestinationPays);

            if (!string.IsNullOrEmpty(DestinationRegion))
                RechercheParRegion(Destinations, DestinationRegion);

            return View(Destinations);
        }

        public IQueryable<DestinationModel> RechercheParNom(IQueryable<DestinationModel> Destinations, string DestinationNom)
        {
            if (!String.IsNullOrEmpty(DestinationNom))
            {
                Destinations = Destinations.Where(s => s.Nom.Contains(DestinationNom));
            }

            return Destinations;
        }

        public IQueryable<DestinationModel> RechercheParPays(IQueryable<DestinationModel> Destinations, string DestinationPays)
        {
            if (!String.IsNullOrEmpty(DestinationPays))
            {
                Destinations = Destinations.Where(s => s.Pays.Contains(DestinationPays));
            }

            return Destinations;
        }

        public IQueryable<DestinationModel> RechercheParRegion(IQueryable<DestinationModel> Destinations, string DestinationRegion)
        {
            if (!String.IsNullOrEmpty(DestinationRegion))
            {
                Destinations = Destinations.Where(s => s.Region.Contains(DestinationRegion));
            }

            return Destinations;
        }

        //
        // GET: /Destination/Details/5

        public ActionResult Details(int id = 0)
        {
            DestinationModel destinationmodel = db.Destinations.Find(id);
            if (destinationmodel == null)
            {
                return HttpNotFound();
            }
            return View(destinationmodel);
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