using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Touristix.Controllers;
using System.Web.Mvc;
using Touristix.Models;
using System.IO;

namespace ProjetDeTest
{
    [TestClass]
    public class TestsAccueil
    {
        const string urlImagesDestinations = "../../../Touristix/Images/Destinations/";
        [TestMethod]
        public void TestOuvrirAccueilControlleur()
        {
            var controller = new AccueilController();
            var result = controller.Index(urlImagesDestinations) as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void TestAccueilRetourTableauImages()
        {
            var controller = new AccueilController();
            var result = controller.Index(urlImagesDestinations) as ViewResult;
            var images = (ImagesAccueilModel)result.ViewData.Model;

            string[] ExtensionsRecherche = { ".png", ".jpg", ".bmp" };

            string[] TestTableauImagesAccueil = Directory.GetFiles(urlImagesDestinations, "*.*")
                .Where(f => ExtensionsRecherche.Contains(new FileInfo(f).Extension.ToLower())).ToArray();

            for (int i = 0; i < TestTableauImagesAccueil.Length; i++)
            {
                string strCheminImage = TestTableauImagesAccueil[i];

                TestTableauImagesAccueil[i] = Path.GetFileName(strCheminImage);
            }

            for (int i = 0; i < TestTableauImagesAccueil.Length; i++)
            {
                Assert.AreEqual(TestTableauImagesAccueil[i], images.TableauImagesAccueil[i]);
            }
        }
    }
}
