using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Touristix.Controllers;
using System.Web.Mvc;
using Touristix.Models;
using Rhino.Mocks;

namespace ProjetDeTest
{
    [TestClass]
    public class TestsAccueil
    {
        [TestMethod]
        public void TestOuvrirAccueilControlleur()
        {
            var controller = new AccueilController();
            var result = controller.Index() as ViewResult;
            var images = (ImagesAccueilModel)result.ViewData.Model;
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void TestAccueilRetourTableauImages()
        {
            var controller = new AccueilController();
            var result = controller.Index() as ViewResult;
            var images = (ImagesAccueilModel)result.ViewData.Model;
            Assert.AreEqual("land1.png", images.TableauImagesAccueil[0]);
        }
    }
}
