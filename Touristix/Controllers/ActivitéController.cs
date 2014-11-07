using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Touristix.Models;

namespace Touristix.Controllers
{
    public class ActivitéController : Controller
    {
        private DestinationDBContext db = new DestinationDBContext();

        public ActionResult Index(string ActiviteNom = "", string ActivitePays = "", string ActiviteVille = "",
                                    string ActiviteRegion = "", int Trier = 0)
        {
            IQueryable<DestinationModel> Destinations = from m in db.Destinations
                                                        select m;

            IQueryable<ActiviteModel> Activites = from m in db.Activites
                                                  select m;

            var SelectPays = (from m in db.Destinations select m.Pays).Distinct().OrderBy(Pays => Pays);

            var ListePays = new List<SelectListItem>(SelectPays.Count());

            Assistant.RemplirListe(ref ListePays, SelectPays);

            var ListeVilles = new List<SelectListItem>();

            ViewBag.DestinationVille = ListeVilles;

            if (!string.IsNullOrEmpty(ActiviteNom))
                Destinations = Destinations.Where(s => s.Nom.Contains(ActiviteNom));

            if (!string.IsNullOrEmpty(ActivitePays))
                Destinations = Destinations.Where(s => s.Pays.Contains(ActivitePays));

            if (!string.IsNullOrEmpty(ActiviteVille))
                Destinations = Destinations.Where(s => s.Ville.Contains(ActiviteVille));

            if (!string.IsNullOrEmpty(ActiviteRegion))
                Destinations = Destinations.Where(s => s.Region.Contains(ActiviteRegion));

            object ResultatRecherche;
            IQueryable<IGrouping<string, DestinationModel>> DestinationRecu;
            Dictionary<string, List<ActiviteModel>> DicTrieActivite;

            switch (Trier)
            {
                #region Tri par Pays

                case 0:
                default:
                    DestinationRecu = Destinations.GroupBy(item => item.Pays);
                    DicTrieActivite = new Dictionary<string, List<ActiviteModel>>(DestinationRecu.Count());
                    foreach (IGrouping<string, DestinationModel> Groupe in DestinationRecu)
                    {
                        List<ActiviteModel> ListActivite = new List<ActiviteModel>(Groupe.Count());
                        List<DestinationModel> GroupeList = Groupe.ToList();

                        for (int i = 0; i < GroupeList.Count; i++)
                        {
                            string[] ArrayParametreActivite = new string[1];
                            ArrayParametreActivite[0] = ";";

                            string[] ArrayActiviteIds = GroupeList[i].ActiviteIds.Split(ArrayParametreActivite, StringSplitOptions.RemoveEmptyEntries);

                            for (int B = 0; B < ArrayActiviteIds.Length; B++)
                            {
                                ActiviteModel ActiviteActif = db.Activites.Find(Convert.ToInt32(ArrayActiviteIds[B]));
                                ListActivite.Add(ActiviteActif);
                            }
                        }
                        DicTrieActivite.Add(Groupe.Key, ListActivite);
                    }
                    ResultatRecherche = DicTrieActivite;
                    break;

                #endregion

                #region Tri par Region

                case 1:
                    DestinationRecu = Destinations.GroupBy(item => item.Region);
                    DicTrieActivite = new Dictionary<string, List<ActiviteModel>>(DestinationRecu.Count());
                    foreach (IGrouping<string, DestinationModel> Groupe in DestinationRecu)
                    {
                        List<ActiviteModel> ListActivite = new List<ActiviteModel>(Groupe.Count());
                        List<DestinationModel> GroupeList = Groupe.ToList();

                        for (int i = 0; i < GroupeList.Count; i++)
                        {
                            string[] ArrayParametreActivite = new string[1];
                            ArrayParametreActivite[0] = ";";

                            string[] ArrayActiviteIds = GroupeList[i].ActiviteIds.Split(ArrayParametreActivite, StringSplitOptions.RemoveEmptyEntries);

                            for (int B = 0; B < ArrayActiviteIds.Length; B++)
                            {
                                ActiviteModel ActiviteActif = db.Activites.Find(Convert.ToInt32(ArrayActiviteIds[B]));
                                ListActivite.Add(ActiviteActif);
                            }
                        }
                        DicTrieActivite.Add(Groupe.Key, ListActivite);
                    }
                    ResultatRecherche = DicTrieActivite;
                    break;

                #endregion

                #region Tri par Ville

                case 2:
                    DestinationRecu = Destinations.GroupBy(item => item.Ville);
                    DicTrieActivite = new Dictionary<string, List<ActiviteModel>>(DestinationRecu.Count());
                    foreach (IGrouping<string, DestinationModel> Groupe in DestinationRecu)
                    {
                        List<ActiviteModel> ListActivite = new List<ActiviteModel>(Groupe.Count());
                        List<DestinationModel> GroupeList = Groupe.ToList();

                        for (int i = 0; i < GroupeList.Count; i++)
                        {
                            string[] ArrayParametreActivite = new string[1];
                            ArrayParametreActivite[0] = ";";

                            string[] ArrayActiviteIds = GroupeList[i].ActiviteIds.Split(ArrayParametreActivite, StringSplitOptions.RemoveEmptyEntries);

                            for (int B = 0; B < ArrayActiviteIds.Length; B++)
                            {
                                ActiviteModel ActiviteActif = db.Activites.Find(Convert.ToInt32(ArrayActiviteIds[B]));
                                ListActivite.Add(ActiviteActif);
                            }
                        }
                        DicTrieActivite.Add(Groupe.Key, ListActivite);
                    }
                    ResultatRecherche = DicTrieActivite;
                    break;

                #endregion

                case 3:
                    ResultatRecherche = Activites.OrderBy(item => item.Nom);
                    break;
            }

            ActiviteModel[] Array5DerniereActiviteModel = db.Activites
                    .OrderByDescending(m => m.Nom)
                    .Take(5)
                    .ToArray();

            return View(new Tuple<ActiviteModel[], object, List<SelectListItem>>(Array5DerniereActiviteModel, ResultatRecherche, ListePays));
        }
    }
}
