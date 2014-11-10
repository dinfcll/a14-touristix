using Microsoft.VisualStudio.TestTools.UnitTesting;
using Touristix.Controllers;
using System.Web.Mvc;

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

        [TestMethod]
        public void TestOuvrirDestinationAdminControlleur()
        {
            var controller = new DestinationController();
            var result = controller.Admin("../../../Touristix/Images/Destinations/", "../../../Touristix/Images/Batiments/") as ViewResult;
            Assert.AreEqual("Admin", result.ViewName);
        }
    }
}
