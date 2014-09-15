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
            var model = new ImagesAccueilModel();
            {
                model.TableauImagesAccueil = Directory.GetFiles(Server.MapPath("~/Images/ImagesAccueil/"), "*.png");

                for (int IndiceImage = 0; IndiceImage < model.TableauImagesAccueil.Length; IndiceImage++)
                {
                    string strCheminImage = model.TableauImagesAccueil[IndiceImage];

                    model.TableauImagesAccueil[IndiceImage] = Path.GetFileName(strCheminImage);
                }
                
            };

            return View(model);
        }
    }
}
