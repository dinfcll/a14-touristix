using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Touristix.Models;
using System.IO;

namespace Touristix.Controllers
{
    public partial class DestinationController : Controller
    {
        private DestinationDBContext db = new DestinationDBContext();

        public ActionResult Index(string DestinationNom = "", string DestinationPays = "", string DestinationVille = "",
                                    string DestinationRegion = "", int Trier = 0)
        {
            IQueryable<DestinationModel> Destinations = from m in db.Destinations
                                                        select m;

            var SelectPays = (from m in db.Destinations select m.Pays).Distinct().OrderBy(Pays=>Pays);

            var ListePays = new List<SelectListItem>(SelectPays.Count());

            Assistant.RemplirListe(ref ListePays, SelectPays);

            ViewBag.DestinationVille = new List<SelectListItem>();
            
            if (!string.IsNullOrEmpty(DestinationNom))
                Destinations = Destinations.Where(s => s.Nom.Contains(DestinationNom));

            if (!string.IsNullOrEmpty(DestinationPays))
                Destinations = Destinations.Where(s => s.Pays.Contains(DestinationPays));

            if (!string.IsNullOrEmpty(DestinationVille))
                Destinations = Destinations.Where(s => s.Ville.Contains(DestinationVille));

            if (!string.IsNullOrEmpty(DestinationRegion))
                Destinations = Destinations.Where(s => s.Region.Contains(DestinationRegion));

            object DestinationRecu;

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
                case 3:
                DestinationRecu = Destinations.OrderBy(item => item.Nom);
                break;
            }

            DestinationModel[] Array5DerniereDestination = db.Destinations
                    .OrderByDescending(m => m.Nom)
                    .Take(5)
                    .ToArray();

            return View("Index", new Tuple<DestinationModel[], object, List<SelectListItem>>
                                    (Array5DerniereDestination, DestinationRecu, ListePays));
        }

        [Authorize(Roles = "admin")]
        public ActionResult Admin(string urlDestination, string urlBatiment, string urlActivite)
        {
            AdministrationList NouvelleListe = new AdministrationList();
            NouvelleListe.ListDestinationModel = db.Destinations.ToList();
            NouvelleListe.ListBatimentModel = db.Batiments.ToList();
            NouvelleListe.ListActiviteModel = db.Activites.ToList();

            if (string.IsNullOrEmpty(urlDestination))
            {
                urlDestination = Server.MapPath("~/Images/Destinations/");
            }

            string[] ArrayDestinationImage = Directory.GetFiles(urlDestination, "*.*");
            NouvelleListe.ArrayDestinationImage = new string[ArrayDestinationImage.Length];
            for (int D = ArrayDestinationImage.Length - 1; D >= 0 ; --D)
            {
                NouvelleListe.ArrayDestinationImage[D] = Path.GetFileName(ArrayDestinationImage[D]);
            }

            if (string.IsNullOrEmpty(urlBatiment))
            {
                urlBatiment = Server.MapPath("~/Images/Batiments/");
            }

            string[] ArrayBatimentImage = Directory.GetFiles(urlBatiment, "*.*");
            NouvelleListe.ArrayBatimentImage = new string[ArrayBatimentImage.Length];
            for (int D = ArrayBatimentImage.Length - 1; D >= 0; --D)
            {
                NouvelleListe.ArrayBatimentImage[D] = Path.GetFileName(ArrayBatimentImage[D]);
            }

            if (string.IsNullOrEmpty(urlActivite))
            {
                urlBatiment = Server.MapPath("~/Images/Batiments/");
            }

            string[]  ArrayActiviteImage = Directory.GetFiles(urlActivite, "*.*");
            NouvelleListe.ArrayActiviteImage = new string[ArrayActiviteImage.Length];
            for (int D = ArrayActiviteImage.Length - 1; D >= 0; --D)
            {
                NouvelleListe.ArrayActiviteImage[D] = Path.GetFileName(ArrayActiviteImage[D]);
            }

            return View("Admin", NouvelleListe);
        }

        public void MettreAJourDestination(DestinationModel DestinationModelActif)
        {
            DestinationModelActif.BatimentIds = "";
            int DernierBatiment = Convert.ToInt32(Request["DernierBatiment"]);
            if (DernierBatiment >= 1)
            {
                for (int B = 0; B < DernierBatiment; B++)
                {
                    int Id = Convert.ToInt32(Request["Batiment" + B]);
                    DestinationModelActif.BatimentIds += Id + ";";
                }
            }

            DestinationModelActif.ActiviteIds = "";
            int DerniereActivite = Convert.ToInt32(Request["DerniereActivite"]);
            if (DerniereActivite >= 1)
            {
                for (int B = 0; B < DerniereActivite; B++)
                {
                    int Id = Convert.ToInt32(Request["Activite" + B]);
                    DestinationModelActif.ActiviteIds += Id + ";";
                }
            }
        }

        public void MettreAJourBatiment(BatimentModel BatimentModelActif)
        {
            if (BatimentModelActif.TypeURL == "http")
            {
                BatimentModelActif.URL = BatimentModelActif.URL.Insert(0, "http://");
            }
            else
            {
                BatimentModelActif.URL = BatimentModelActif.URL.Insert(0, "https://");
            }
        }

        #region Fonctions AJAX

        public JsonResult ObtenirListeBatiment(string Id)
        {
            int IdNumber = Convert.ToInt32(Id);
            List<SelectListItem> ListBatiment = new List<SelectListItem>();

            IQueryable<BatimentModel> Batiments = from m in db.Batiments
                                                  select m;

            ListBatiment.Add(new SelectListItem { Text = "", Value = "" });

            foreach (BatimentModel BatimentActif in Batiments)
            {
                ListBatiment.Add(new SelectListItem { Text = BatimentActif.Nom, Value = BatimentActif.Id.ToString() });
            }
            return Json(new SelectList(ListBatiment, "Value", "Text"));
        }

        public JsonResult ObtenirBatiment(string Id)
        {
            BatimentModel Batiment = db.Batiments.Find(Convert.ToInt32(Id));

            return Json(Batiment);
        }

        public JsonResult ObtenirListeActivite(string Id)
        {
            int IdNumber = Convert.ToInt32(Id);
            List<SelectListItem> ListActivite = new List<SelectListItem>();

            IQueryable<ActiviteModel> Activites = from m in db.Activites
                                                  select m;

            ListActivite.Add(new SelectListItem { Text = "", Value = "" });

            foreach (ActiviteModel ActiviteActif in Activites)
            {
                ListActivite.Add(new SelectListItem { Text = ActiviteActif.Nom, Value = ActiviteActif.Id.ToString() });
            }
            return Json(new SelectList(ListActivite, "Value", "Text"));
        }

        public JsonResult ObtenirActivite(string Id)
        {
            ActiviteModel Activite = db.Activites.Find(Convert.ToInt32(Id));

            return Json(Activite);
        }

        public JsonResult ObtenirVille(string strPays)
        {
            IQueryable<String> SelectVilles;

            if (strPays != "")
            {
                SelectVilles = (from m in db.Destinations where m.Pays == strPays select m.Ville).Distinct().OrderBy(Ville => Ville);
            }
            else
            {
                SelectVilles = (from m in db.Destinations select m.Ville).Distinct().OrderBy(Ville => Ville);
            }

            var ListeVilles = new List<SelectListItem>();

            Assistant.RemplirListe(ref ListeVilles, SelectVilles);

            return Json(ListeVilles, JsonRequestBehavior.AllowGet);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
