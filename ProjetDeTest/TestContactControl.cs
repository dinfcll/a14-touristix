﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Touristix.Controllers;
using System.Web.Mvc;

namespace ProjetDeTest
{
    [TestClass]
    public class TestContactControl
    {
        [TestMethod]
        public void TestOuvertureContactForm()
        {
            var controller = new ContactController();
            var result = controller.ContactForm() as ViewResult;
            Assert.AreEqual("ContactForm", result.ViewName);
        }

        [TestMethod]
        public void TestOuvertureIndex()
        {
            var controller = new ContactController();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
