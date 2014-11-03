using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Touristix.Models;

namespace Touristix.Controllers
{
    public class BatimentController : Controller
    {
        private DestinationDBContext db = new DestinationDBContext();

        public ActionResult Index(string BatimentNom = "", string BatimentPays = "", string BatimentVille = "",
                                    string BatimentRegion = "", int Trier = 0)
        {
            IQueryable<DestinationModel> Destinations = from m in db.Destinations
                                                        select m;

            IQueryable<BatimentModel> Batiments = from m in db.Batiments
                                                        select m;

            var SelectPays = (from m in db.Destinations select m.Pays).Distinct().OrderBy(Pays => Pays);

            var ListePays = new List<SelectListItem>(SelectPays.Count());

            Assistant.RemplirListe(ref ListePays, SelectPays);

            var ListeVilles = new List<SelectListItem>();

            ViewBag.DestinationVille = ListeVilles;

            if (!string.IsNullOrEmpty(BatimentNom))
                Destinations = Destinations.Where(s => s.Nom.Contains(BatimentNom));

            if (!string.IsNullOrEmpty(BatimentPays))
                Destinations = Destinations.Where(s => s.Pays.Contains(BatimentPays));

            if (!string.IsNullOrEmpty(BatimentVille))
                Destinations = Destinations.Where(s => s.Ville.Contains(BatimentVille));

            if (!string.IsNullOrEmpty(BatimentRegion))
                Destinations = Destinations.Where(s => s.Region.Contains(BatimentRegion));

            object ResultatRecherche;
            IQueryable<IGrouping<string, DestinationModel>> DestinationRecu;

            switch (Trier)
            {
                case 0:
                default:
                    DestinationRecu = Destinations.GroupBy(item => item.Pays);
                    Dictionary<string, List<BatimentModel>> DicTrieBatiment = new Dictionary<string, List<BatimentModel>>(DestinationRecu.Count());
                    foreach (IGrouping<string, DestinationModel> Groupe in DestinationRecu)
                    {
                        List<BatimentModel> ListBatiment = new List<BatimentModel>(Groupe.Count());
                        List<DestinationModel> GroupeList = Groupe.ToList();

                        for (int i = 0; i < GroupeList.Count; i++)
                        {
                            string[] ArrayParametreBatiment = new string[1];
                            ArrayParametreBatiment[0] = ";";

                            string[] ArrayBatimentIds = GroupeList[i].BatimentIds.Split(ArrayParametreBatiment, StringSplitOptions.RemoveEmptyEntries);

                            for (int B = 0; B < ArrayBatimentIds.Length; B++)
                            {
                                BatimentModel BatimentActif = db.Batiments.Find(Convert.ToInt32(ArrayBatimentIds[B]));
                                ListBatiment.Add(BatimentActif);
                            }
                        }
                        DicTrieBatiment.Add(Groupe.Key, ListBatiment);
                    }
                    ResultatRecherche = DicTrieBatiment;
                    break;
                case 1:
                    DestinationRecu = Destinations.GroupBy(item => item.Region);
                    ResultatRecherche = DestinationRecu;
                    break;
                case 2:
                    DestinationRecu = Destinations.GroupBy(item => item.Ville);
                    ResultatRecherche = DestinationRecu;
                    break;
            }

            DestinationModel[] Array5DerniereDestination = db.Destinations
                    .OrderByDescending(m => m.Nom)
                    .Take(5)
                    .ToArray();

            return View(new Tuple<DestinationModel[], object, List<SelectListItem>>(Array5DerniereDestination, ResultatRecherche, ListePays));
        }
    }
}
