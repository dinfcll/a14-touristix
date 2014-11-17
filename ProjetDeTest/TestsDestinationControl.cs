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
            var result = controller.Admin("../../../Touristix/Images/Destinations/", "../../../Touristix/Images/Batiments/") as ViewResult;
            Assert.AreEqual("Admin", result.ViewName);
        }

        [TestMethod]
        public void TestDestinationAdminRetourModelList()
        {
            var controller = new DestinationController();
            var result = controller.Admin("../../../Touristix/Images/Destinations/", "../../../Touristix/Images/Batiments/") as ViewResult;
            var liste = (AdministrationList)result.ViewData.Model;

            AdministrationList TestListAdmin = new AdministrationList();
            TestListAdmin.ListDestinationModel = db.Destinations.ToList();
            TestListAdmin.ListBatimentModel = db.Batiments.ToList();
            TestListAdmin.ListActiviteModel = db.Activites.ToList();

            string[] ArrayDestinationImage = Directory.GetFiles("../../../Touristix/Images/Destinations/", "*.*");
            TestListAdmin.ArrayDestinationImage = new string[ArrayDestinationImage.Length];
            for (int D = ArrayDestinationImage.Length - 1; D >= 0; --D)
            {
                TestListAdmin.ArrayDestinationImage[D] = Path.GetFileName(ArrayDestinationImage[D]);
            }

            string[] ArrayBatimentImage = Directory.GetFiles("../../../Touristix/Images/Batiments/", "*.*");
            TestListAdmin.ArrayBatimentImage = new string[ArrayBatimentImage.Length];
            for (int D = ArrayBatimentImage.Length - 1; D >= 0; --D)
            {
                TestListAdmin.ArrayBatimentImage[D] = Path.GetFileName(ArrayBatimentImage[D]);
            }

            TestListAdmin.ArrayActiviteImage = new string[0];

            for (int i = 0; i < TestListAdmin.ListDestinationModel.Count; i++)
            {
                Assert.AreEqual(TestListAdmin.ListDestinationModel[i].Nom, liste.ListDestinationModel[i].Nom);
            }

            for (int i = 0; i < TestListAdmin.ListBatimentModel.Count; i++)
            {
                Assert.AreEqual(TestListAdmin.ListBatimentModel[i].Nom, liste.ListBatimentModel[i].Nom);
            }

            for (int i = 0; i < TestListAdmin.ListActiviteModel.Count; i++)
            {
                Assert.AreEqual(TestListAdmin.ListActiviteModel[i].Nom, liste.ListActiviteModel[i].Nom);
            }

            for (int i = 0; i < TestListAdmin.ArrayDestinationImage.Length; i++)
            {
                Assert.AreEqual(TestListAdmin.ArrayDestinationImage[i], liste.ArrayDestinationImage[i]);
            }

            for (int i = 0; i < TestListAdmin.ArrayBatimentImage.Length; i++)
            {
                Assert.AreEqual(TestListAdmin.ArrayBatimentImage[i], liste.ArrayBatimentImage[i]);
            }

            for (int i = 0; i < TestListAdmin.ArrayActiviteImage.Length; i++)
            {
                Assert.AreEqual(TestListAdmin.ArrayActiviteImage[i], liste.ArrayActiviteImage[i]);
            }
        }
    }
}
