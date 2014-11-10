using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
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
            var controller = new DestinationController();
            var result = controller.ModifierDestination() as ViewResult;
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void TestOuvrirModifierDestination()
        {
            var controller = new DestinationController();
            var result = controller.ModifierDestination(1) as ViewResult;
            Assert.AreEqual("ModifierDestination", result.ViewName);
        }

        [TestMethod]
        public void TestOuvrirModifierBatimentErreur()
        {
            var controller = new DestinationController();
            var result = controller.ModifierBatiment() as ViewResult;
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void TestOuvrirModifierBatiment()
        {
            var controller = new DestinationController();
            var result = controller.ModifierBatiment(1) as ViewResult;
            Assert.AreEqual("ModifierBatiment", result.ViewName);
        }

        [TestMethod]
        public void TestOuvrirModifierActiviteErreur()
        {
            var controller = new DestinationController();
            var result = controller.ModifierActivite() as ViewResult;
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void TestOuvrirModifierActivite()
        {
            var controller = new DestinationController();
            var result = controller.ModifierActivite(1) as ViewResult;
            Assert.AreEqual("ModifierActivite", result.ViewName);
        }
    }
}
