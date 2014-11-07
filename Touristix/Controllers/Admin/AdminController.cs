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

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ReceptionImage()
        {
            string[] Images = Directory.GetFiles(Server.MapPath("~/Images/Destinations/"), "*.*");

            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase Image = Request.Files[i];

                string CheminComplet = System.IO.Path.Combine(
                                       Server.MapPath("~/Images/Destinations/"), System.IO.Path.GetFileName(Image.FileName));

                if (System.IO.File.Exists(CheminComplet))
                {
                    return Json("Le téléversement a échoué");
                }

                Image.SaveAs(CheminComplet);
            }
            return Json("Téléversement complété");
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
