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
    public class TestCreerDestinationControl
    {
        [TestMethod]
        public void TestOuvrirCreerDestination()
        {
            var controller = new DestinationController();
            var result = controller.CreerDestination() as ViewResult;
            Assert.AreEqual("CreerDestination", result.ViewName);
        }

        [TestMethod]
        public void TestOuvrirCreerBatiment()
        {
            var controller = new DestinationController();
            var result = controller.CreerBatiment() as ViewResult;
            Assert.AreEqual("CreerBatiment", result.ViewName);
        }

        [TestMethod]
        public void TestOuvrirCreerActivite()
        {
            var controller = new DestinationController();
            var result = controller.CreerActivite() as ViewResult;
            Assert.AreEqual("CreerActivite", result.ViewName);
        }
    }
}
