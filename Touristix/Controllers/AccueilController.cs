using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Touristix.Models;

namespace Touristix.Controllers
{
    public class AccueilController : Controller
    {
        //
        // GET: /Accueil/

        public ActionResult Index()
        {
            var modelImagesAccueil = new ImagesAccueilModel();
            {

                string[] ExtensionsRecherche = { ".png", ".jpg", ".bmp" };

                modelImagesAccueil.TableauImagesAccueil = Directory.GetFiles(Server.MapPath("~/Images/ImagesAccueil/"), "*.*")
                    .Where(f => ExtensionsRecherche.Contains(new FileInfo(f).Extension.ToLower())).ToArray();

                for (int IndiceImage = 0; IndiceImage < modelImagesAccueil.TableauImagesAccueil.Length; IndiceImage++)
                {
                    string strCheminImage = modelImagesAccueil.TableauImagesAccueil[IndiceImage];

                    modelImagesAccueil.TableauImagesAccueil[IndiceImage] = Path.GetFileName(strCheminImage);
                }
                
            };

            return View(modelImagesAccueil);
        }
    }
}
