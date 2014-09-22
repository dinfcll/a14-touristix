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
        [Authorize]
        public ActionResult ModifierDestination(int id = 0)
        {
            DestinationModel DestinationModelActif = db.Destinations.Find(id);
            if (DestinationModelActif == null)
            {
                return HttpNotFound();
            }

            //Récupérer les batiments liés.
            DestinationModfiable NouveauDestinationModfiable = new DestinationModfiable();
            NouveauDestinationModfiable.ListChaineBatimentId = new List<int>();

            NouveauDestinationModfiable.Destination = DestinationModelActif;
            int ProchainBatimentId = DestinationModelActif.ProchainBatimentId;
            ChaineBatiment ProchainBatiment = null;

            do
            {
                ProchainBatiment = db.ChaineBatiments.Find(ProchainBatimentId);

                if (ProchainBatiment == null)
                {
                    ProchainBatimentId = -1;
                    NouveauDestinationModfiable.ListChaineBatimentId.Add(0);
                }
                else
                {
                    ProchainBatimentId = ProchainBatiment.ProchainId;
                    NouveauDestinationModfiable.ListChaineBatimentId.Add(ProchainBatiment.BatimentId);
                }
            }
            while (ProchainBatiment != null);

            return View(NouveauDestinationModfiable);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ModifierDestination(DestinationModel Destination)
        {
            int DernierId = 0;
            if (db.ChaineBatiments.Count() > 0)
                DernierId = db.ChaineBatiments.ToList().Last().Id;

            if (ModelState.IsValid)
            {
                int DernierBatiment = Convert.ToInt32(Request["DernierBatiment"]);

                if (DernierBatiment >= 1)
                {
                    ChaineBatiment VieilleChaineBatiment = db.ChaineBatiments.Find(Destination.ProchainBatimentId);

                    if (VieilleChaineBatiment == null)
                    {
                        ++DernierId;
                        ChaineBatiment ChaineBatimentActive = new ChaineBatiment(DernierId, Convert.ToInt32(Request["Batiment0"]));
                        Destination.ProchainBatimentId = DernierId;
                        VieilleChaineBatiment = ChaineBatimentActive;
                        db.ChaineBatiments.Add(ChaineBatimentActive);
                    }

                    if (DernierId <= 0)
                    {
                        return HttpNotFound();
                    }

                    for (int B = 1; B < DernierBatiment; B++)
                    {
                        int Id = Convert.ToInt32(Request["Batiment" + B]);
                        ChaineBatiment ChaineBatimentActive = null;

                        if (VieilleChaineBatiment != null)
                        {
                            if (VieilleChaineBatiment.ProchainId >= 0)
                                ChaineBatimentActive = db.ChaineBatiments.Find(VieilleChaineBatiment.ProchainId);

                            if (ChaineBatimentActive == null)
                            {
                                ++DernierId;
                                ChaineBatimentActive = new ChaineBatiment(DernierId, Id);
                                db.ChaineBatiments.Add(ChaineBatimentActive);
                            }

                            VieilleChaineBatiment.ProchainId = ChaineBatimentActive.Id;
                        }

                        VieilleChaineBatiment = ChaineBatimentActive;
                    }
                }

                db.Entry(Destination).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View(Destination.Id);
        }

        [Authorize]
        public ActionResult ModifierBatiment(int id = 0)
        {
            BatimentModel BatimentModelActif = db.Batiments.Find(id);
            if (BatimentModelActif == null)
            {
                return HttpNotFound();
            }
            return View(BatimentModelActif);
        }

        [Authorize]
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

        [Authorize]
        public ActionResult ModifierActivite(int id = 0)
        {
            ActiviteModel ActiviteModelActif = db.Activites.Find(id);
            if (ActiviteModelActif == null)
            {
                return HttpNotFound();
            }
            return View(ActiviteModelActif);
        }

        [Authorize]
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
    }
}
