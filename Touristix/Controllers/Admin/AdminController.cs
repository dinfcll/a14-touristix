﻿using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Touristix.Models;

namespace Touristix.Controllers
{
    public partial class AdminController : Controller
    {
        private DestinationDBContext db = new DestinationDBContext();

        const string MessageSuppressImage = "Image supprimée";
        const string MessageTeleversementEchoue = "Le téléversement a échoué";
        const string MessageTeleversementComplete = "Téléversement complété";

        [Authorize(Roles = "admin")]
        public ActionResult Index(string urlDestination, string urlBatiment, string urlActivite)
        {
            AdministrationList NouvelleListe = new AdministrationList
            {
                ListDestinationModel = db.Destinations.ToList(),
                ListBatimentModel = db.Batiments.ToList(),
                ListActiviteModel = db.Activites.ToList(),
                ListALaUneModel = db.ALaUne.ToList()
            };

            if (string.IsNullOrEmpty(urlDestination))
            {
                urlDestination = Server.MapPath("~/Images/Destinations/");
            }

            string[] ArrayDestinationImage = Directory.GetFiles(urlDestination, "*.*");
            NouvelleListe.ArrayDestinationImage = new string[ArrayDestinationImage.Length];
            for (int D = ArrayDestinationImage.Length - 1; D >= 0; --D)
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
                urlActivite = Server.MapPath("~/Images/Activités/");
            }

            string[] ArrayActiviteImage = Directory.GetFiles(urlActivite, "*.*");
            NouvelleListe.ArrayActiviteImage = new string[ArrayActiviteImage.Length];
            for (int D = ArrayActiviteImage.Length - 1; D >= 0; --D)
            {
                NouvelleListe.ArrayActiviteImage[D] = Path.GetFileName(ArrayActiviteImage[D]);
            }

            NouvelleListe.ArrayALaUneDestinationId = NouvelleListe.ListALaUneModel.Select(model => model.Id).ToArray();

            return View("Index", NouvelleListe);
        }

        public JsonResult ReceptionImageDestination()
        {
            return ReceptionImage("Destinations");
        }

        public JsonResult ReceptionImageBatiment()
        {
            return ReceptionImage("Batiments");
        }

        public JsonResult ReceptionImageActivite()
        {
            return ReceptionImage("Activités");
        }

        private JsonResult ReceptionImage(string Chemin)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase Image = Request.Files[i];

                string CheminComplet = Path.Combine(
                                       Server.MapPath("~/Images/" + Chemin + "/"), Path.GetFileName(Image.FileName));

                if (System.IO.File.Exists(CheminComplet))
                {
                    return Json(MessageTeleversementEchoue);
                }

                Image.SaveAs(CheminComplet);
            }
            return Json(MessageTeleversementComplete);
        }

        public JsonResult SupprimerImageDestination(string Nom)
        {
            System.IO.File.Delete(Server.MapPath("~/Images/Destinations/" + Nom));
            return Json(MessageSuppressImage);
        }

        public JsonResult SupprimerImageBatiment(string Nom)
        {
            System.IO.File.Delete(Server.MapPath("~/Images/Batiments/" + Nom));
            return Json(MessageSuppressImage);
        }

        public JsonResult SupprimerImageActivite(string Nom)
        {
            System.IO.File.Delete(Server.MapPath("~/Images/Activités/" + Nom));
            return Json(MessageSuppressImage);
        }
    }
}
