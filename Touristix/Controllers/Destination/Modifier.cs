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
        public ActionResult ModifierDestination(int id = 0)
        {
            DestinationModel DestinationModelActif = db.Destinations.Find(id);
            if (DestinationModelActif == null)
            {
                return HttpNotFound();
            }

            //Récupérer les batiments liés.
            DestinationModfiable NouveauDestinationModfiable = new DestinationModfiable();
            NouveauDestinationModfiable.ListChaineBatimentID = new List<int>();

            NouveauDestinationModfiable.Destination = DestinationModelActif;
            int ProchainBatimentID = DestinationModelActif.ProchainBatimentID;
            ChaineBatiment ProchainBatiment = null;

            //Ajoute les batiments trouvé à la liste et complète la liste avec -1 pour un dropdownlist supplémentaire vide.
            do
            {
                ProchainBatiment = db.ChaineBatiments.Find(ProchainBatimentID);

                if (ProchainBatiment == null)
                {
                    ProchainBatimentID = -1;
                    NouveauDestinationModfiable.ListChaineBatimentID.Add(0);
                }
                else
                {
                    ProchainBatimentID = ProchainBatiment.ProchainID;
                    NouveauDestinationModfiable.ListChaineBatimentID.Add(ProchainBatiment.BatimentID);
                }
            }
            while (ProchainBatiment != null);

            return View(NouveauDestinationModfiable);
        }

        [HttpPost]
        public ActionResult ModifierDestination(DestinationModel Destination)
        {
            int DernierID = 0;
            if (db.ChaineBatiments.Count() > 0)
                DernierID = db.ChaineBatiments.ToList().Last().ID;

            if (ModelState.IsValid)
            {
                int DernierBatiment = Convert.ToInt32(Request["DernierBatiment"]);

                if (DernierBatiment >= 1)
                {
                    ChaineBatiment VieilleChaineBatiment = db.ChaineBatiments.Find(Destination.ProchainBatimentID);

                    if (VieilleChaineBatiment == null)
                    {
                        ++DernierID;
                        ChaineBatiment ChaineBatimentActive = new ChaineBatiment(DernierID, Convert.ToInt32(Request["Batiment0"]));
                        Destination.ProchainBatimentID = DernierID;
                        VieilleChaineBatiment = ChaineBatimentActive;
                        db.ChaineBatiments.Add(ChaineBatimentActive);
                    }

                    if (DernierID <= 0)
                    {
                        return HttpNotFound();
                    }

                    for (int B = 1; B < DernierBatiment; B++)
                    {
                        int ID = Convert.ToInt32(Request["Batiment" + B]);
                        ChaineBatiment ChaineBatimentActive = null;

                        if (VieilleChaineBatiment != null)
                        {
                            if (VieilleChaineBatiment.ProchainID >= 0)
                                ChaineBatimentActive = db.ChaineBatiments.Find(VieilleChaineBatiment.ProchainID);

                            if (ChaineBatimentActive == null)
                            {
                                ++DernierID;
                                ChaineBatimentActive = new ChaineBatiment(DernierID, ID);
                                db.ChaineBatiments.Add(ChaineBatimentActive);
                            }

                            VieilleChaineBatiment.ProchainID = ChaineBatimentActive.ID;
                        }

                        VieilleChaineBatiment = ChaineBatimentActive;
                    }
                }

                db.Entry(Destination).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Admin");
            }
            return View(Destination.ID);
        }

        public ActionResult ModifierBatiment(int id = 0)
        {
            BatimentModel BatimentModelActif = db.Batiments.Find(id);
            if (BatimentModelActif == null)
            {
                return HttpNotFound();
            }
            return View(BatimentModelActif);
        }

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

        public ActionResult ModifierActivite(int id = 0)
        {
            ActiviteModel ActiviteModelActif = db.Activites.Find(id);
            if (ActiviteModelActif == null)
            {
                return HttpNotFound();
            }
            return View(ActiviteModelActif);
        }

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
