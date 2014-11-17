using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Touristix.Controllers
{
    public class AdminController : Controller
    {
        const string MessageSuppressImage = "Image supprimée";
        const string MessageTeleversementEchoue = "Le téléversement a échoué";
        const string MessageTeleversementComplete = "Téléversement complété";

        public ActionResult Index()
        {
            return View();
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
            string[] Images = Directory.GetFiles(Server.MapPath("~/Images/" + Chemin + "/"), "*.*");

            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase Image = Request.Files[i];

                string CheminComplet = System.IO.Path.Combine(
                                       Server.MapPath("~/Images/" + Chemin + "/"), System.IO.Path.GetFileName(Image.FileName));

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
