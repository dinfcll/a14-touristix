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
        //
        // GET: /Téléversement/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Magic()
        {
            string[] Images = Directory.GetFiles(Server.MapPath("~/Images/Destinations/"), "*.*");

            for (int i = 0; i < Request.Files.Count; i++)
            {
                // for each file being sent over...
                HttpPostedFileBase Image = Request.Files[i];

                string CheminComplet = System.IO.Path.Combine(
                                       Server.MapPath("~/Images/Destinations/"), System.IO.Path.GetFileName(Image.FileName));

                if (System.IO.File.Exists(CheminComplet))
                    return Json("File upload failed");

                // file is uploaded
                Image.SaveAs(CheminComplet);
            }
            // after successfully uploading redirect the user
            return Json("File uploaded successfully");
        }
    }
}
