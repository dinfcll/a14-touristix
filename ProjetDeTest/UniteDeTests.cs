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
    public class UniteDeTests
    {
        [TestMethod]
        public void TestOuvrirAccueilControlleur()
        {
            var controller = new AccueilController();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
