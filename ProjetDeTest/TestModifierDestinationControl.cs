using Microsoft.VisualStudio.TestTools.UnitTesting;
using Touristix.Controllers;
using System.Web.Mvc;
using Touristix.Models;

namespace ProjetDeTest
{
    [TestClass]
    public class TestNodifierDestinationControl
    {
        [TestMethod]
        public void TestOuvrirModifierDestinationErreur()
        {
            var controller = new AdminController();
            var result = controller.ModifierDestination() as ViewResult;
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void TestOuvrirModifierDestination()
        {
            var controller = new AdminController();
            var result = controller.ModifierDestination(1) as ViewResult;
            Assert.AreEqual("ModifierDestination", result.ViewName);
        }

        [TestMethod]
        public void TestRetourModelModifierDestination()
        {
            var controller = new AdminController();
            var result = controller.ModifierDestination(1) as ViewResult;
            var destinationModel = (DestinationModel)result.ViewData.Model;
            Assert.AreEqual(1,destinationModel.Id);
        }

        [TestMethod]
        public void TestOuvrirModifierBatimentErreur()
        {
            var controller = new AdminController();
            var result = controller.ModifierBatiment() as ViewResult;
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void TestOuvrirModifierBatiment()
        {
            var controller = new AdminController();
            var result = controller.ModifierBatiment(1) as ViewResult;
            Assert.AreEqual("ModifierBatiment", result.ViewName);
        }

        [TestMethod]
        public void TestRetourModelModifierBatiment()
        {
            var controller = new AdminController();
            var result = controller.ModifierBatiment(1) as ViewResult;
            var batimentModel = (BatimentModel)result.ViewData.Model;
            Assert.AreEqual(1, batimentModel.Id);
        }

        [TestMethod]
        public void TestOuvrirModifierActiviteErreur()
        {
            var controller = new AdminController();
            var result = controller.ModifierActivite() as ViewResult;
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void TestOuvrirModifierActivite()
        {
            var controller = new AdminController();
            var result = controller.ModifierActivite(1) as ViewResult;
            Assert.AreEqual("ModifierActivite", result.ViewName);
        }

        [TestMethod]
        public void TestRetourModelModifierActivite()
        {
            var controller = new AdminController();
            var result = controller.ModifierActivite(1) as ViewResult;
            var activiteModel = (ActiviteModel)result.ViewData.Model;
            Assert.AreEqual(1, activiteModel.Id);
        }
    }
}
