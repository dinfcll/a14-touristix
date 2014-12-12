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
        private DestinationDBContext db = new DestinationDBContext();

        [TestMethod]
        public void TestOuvrirAccueilControlleur()
        {
            var controller = new AccueilController();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void TestAccueilRetourTableauImages()
        {
            var controller = new AccueilController();
            var result = controller.Index() as ViewResult;
            var images = (ImagesAccueilModel)result.ViewData.Model;

            var TestListALaUne = db.ALaUne.ToList();

            Assert.AreEqual(TestListALaUne.ToString(), images.ListALaUne.ToString());
        }
    }
}
