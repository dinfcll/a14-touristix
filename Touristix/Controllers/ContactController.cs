using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Touristix.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/

        [HttpGet]
        public ActionResult ContactForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactForm(string Nom)
        {
            return View();
        }

    }
}
