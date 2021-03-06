﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Touristix.Controllers;
using System.Web.Mvc;

namespace ProjetDeTest
{
    [TestClass]
    public class TestCreerDestinationControl
    {
        [TestMethod]
        public void TestOuvrirCreerDestination()
        {
            var controller = new AdminController();
            var result = controller.CreerDestination() as ViewResult;
            Assert.AreEqual("CreerDestination", result.ViewName);
        }

        [TestMethod]
        public void TestOuvrirCreerBatiment()
        {
            var controller = new AdminController();
            var result = controller.CreerBatiment() as ViewResult;
            Assert.AreEqual("CreerBatiment", result.ViewName);
        }

        [TestMethod]
        public void TestOuvrirCreerActivite()
        {
            var controller = new AdminController();
            var result = controller.CreerActivite() as ViewResult;
            Assert.AreEqual("CreerActivite", result.ViewName);
        }
    }
}
