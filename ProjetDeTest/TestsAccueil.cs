using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Touristix.Controllers;
using System.Web.Mvc;
using Touristix.Models;
using Rhino.Mocks;
using System.IO;

namespace ProjetDeTest
{
    [TestClass]
    public class TestsAccueil
    {
        [TestMethod]
        public void TestOuvrirAccueilControlleur()
        {
            var controller = new AccueilController();
            var result = controller.Index("../../../Touristix/Images/ImagesAccueil/") as ViewResult;
            var images = (ImagesAccueilModel)result.ViewData.Model;
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void TestAccueilRetourTableauImages()
        {
            var controller = new AccueilController();
            var result = controller.Index("../../../Touristix/Images/ImagesAccueil/") as ViewResult;
            var images = (ImagesAccueilModel)result.ViewData.Model;

            string[] ExtensionsRecherche = { ".png", ".jpg", ".bmp" };

            string[] TestTableauImagesAccueil = Directory.GetFiles("../../../Touristix/Images/ImagesAccueil/", "*.*")
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
