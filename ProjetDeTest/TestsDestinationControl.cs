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
    public class TestsDestinationControl
    {
        [TestMethod]
        public void TestOuvrirDestinationControlleur()
        {
            var controller = new DestinationController();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
