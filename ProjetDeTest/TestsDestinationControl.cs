using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Touristix.Controllers;
using System.Web.Mvc;
using Touristix.Models;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace ProjetDeTest
{
    [TestClass]
    public class TestsDestinationControl
    {
        private DestinationDBContext db = new DestinationDBContext();

        [TestMethod]
        public void TestOuvrirDestinationControlleur()
        {
            var controller = new DestinationController();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void TestDestinationIndexRetourModelTupleItem1Array5DerniereDestination()
        {
            var controller = new DestinationController();
            var result = controller.Index() as ViewResult;
            var tuple = (Tuple<Touristix.Models.DestinationModel[], object, List<SelectListItem>>)result.ViewData.Model;

            var TestDestinations = from m in db.Destinations
                                   select m;

            DestinationModel[] TestArray5DerniereDestination = db.Destinations
                    .OrderByDescending(m => m.Nom)
                    .Take(5)
                    .ToArray();

            Assert.AreEqual(TestArray5DerniereDestination.ToString(), tuple.Item1.ToString());
        }

        [TestMethod]
        public void TestDestinationIndexRetourModelTupleItem2DestinationRecu()
        {
            var controller = new DestinationController();
            var result = controller.Index() as ViewResult;
            var tuple = (Tuple<Touristix.Models.DestinationModel[], object, List<SelectListItem>>)result.ViewData.Model;

            var TestDestinations = from m in db.Destinations
                                   select m;

            object TestDestinationRecu = TestDestinations.GroupBy(item => item.Pays);

            Assert.AreEqual(TestDestinationRecu.ToString(), tuple.Item2.ToString());
        }

        [TestMethod]
        public void TestDestinationIndexRetourModelTupleItem3ListePays()
        {
            var controller = new DestinationController();
            var result = controller.Index() as ViewResult;
            var tuple = (Tuple<Touristix.Models.DestinationModel[], object, List<SelectListItem>>)result.ViewData.Model;

            var TestDestinations = from m in db.Destinations
                                   select m;

            var TestSelectPays = (from m in db.Destinations select m.Pays).Distinct().OrderBy(Pays => Pays);

            var TestListePays = new List<SelectListItem>(TestSelectPays.Count());

            Assistant.RemplirListe(ref TestListePays, TestSelectPays);

            Assert.AreEqual(TestListePays.ToString(), tuple.Item3.ToString());
        }

        [TestMethod]
        public void TestOuvrirDestinationAdminControlleur()
        {
            var controller = new DestinationController();
            var result = controller.Admin("../../../Touristix/Images/Destinations/", "../../../Touristix/Images/Batiments/", "../../../Touristix/Images/Activités/") as ViewResult;
            Assert.AreEqual("Admin", result.ViewName);
        }

        [TestMethod]
        public void TestDestinationAdminRetourModelListDestinationModel()
        {
            var controller = new DestinationController();
            var result = controller.Admin("../../../Touristix/Images/Destinations/", "../../../Touristix/Images/Batiments/", "../../../Touristix/Images/Activités/") as ViewResult;
            var liste = (AdministrationList)result.ViewData.Model;

            List<DestinationModel> ListDestinationModel = db.Destinations.ToList();

            Assert.AreEqual(ListDestinationModel.ToString(), liste.ListDestinationModel.ToString());
        }

        [TestMethod]
        public void TestDestinationAdminRetourModelListBatimentModel()
        {
            var controller = new DestinationController();
            var result = controller.Admin("../../../Touristix/Images/Destinations/", "../../../Touristix/Images/Batiments/", "../../../Touristix/Images/Activités/") as ViewResult;
            var liste = (AdministrationList)result.ViewData.Model;

            List<BatimentModel> ListBatimentModel = db.Batiments.ToList();

            Assert.AreEqual(ListBatimentModel.ToString(), liste.ListBatimentModel.ToString());
        }

        [TestMethod]
        public void TestDestinationAdminRetourModelListActiviteModel()
        {
            var controller = new DestinationController();
            var result = controller.Admin("../../../Touristix/Images/Destinations/", "../../../Touristix/Images/Batiments/", "../../../Touristix/Images/Activités/") as ViewResult;
            var liste = (AdministrationList)result.ViewData.Model;

            List<BatimentModel> ListBatimentModel = db.Batiments.ToList();

            Assert.AreEqual(ListBatimentModel.ToString(), liste.ListBatimentModel.ToString());
        }

        [TestMethod]
        public void TestDestinationAdminRetourModelListArrayDestinationImage()
        {
            var controller = new DestinationController();
            var result = controller.Admin("../../../Touristix/Images/Destinations/", "../../../Touristix/Images/Batiments/", "../../../Touristix/Images/Activités/") as ViewResult;
            var liste = (AdministrationList)result.ViewData.Model;

            string[] ArrayDestinationImage = Directory.GetFiles("../../../Touristix/Images/Destinations/", "*.*");
            string TestDestinationImage = Path.GetFileName(ArrayDestinationImage[0]);

            Assert.AreEqual(TestDestinationImage, liste.ArrayDestinationImage[0]);
        }

        [TestMethod]
        public void TestDestinationAdminRetourModelListArrayBatimentImage()
        {
            var controller = new DestinationController();
            var result = controller.Admin("../../../Touristix/Images/Destinations/", "../../../Touristix/Images/Batiments/", "../../../Touristix/Images/Activités/") as ViewResult;
            var liste = (AdministrationList)result.ViewData.Model;

            string[] ArrayBatimentImage = Directory.GetFiles("../../../Touristix/Images/Batiments/", "*.*");
            string TestBatimentImage = Path.GetFileName(ArrayBatimentImage[0]);

            Assert.AreEqual(TestBatimentImage, liste.ArrayBatimentImage[0]);
        }

        [TestMethod]
        public void TestDestinationAdminRetourModelListArrayActiviteImage()
        {
            var controller = new DestinationController();
            var result = controller.Admin("../../../Touristix/Images/Destinations/", "../../../Touristix/Images/Batiments/", "../../../Touristix/Images/Activités/") as ViewResult;
            var liste = (AdministrationList)result.ViewData.Model;

            string[] ArrayActiviteImage = new string[0];

            Assert.AreEqual(ArrayActiviteImage.ToString(), liste.ArrayActiviteImage.ToString());
        }
    }
}
