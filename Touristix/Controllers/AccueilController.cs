﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Touristix.Controllers
{
    public class AccueilController : Controller
    {
        //
        // GET: /Accueil/

        public ActionResult Index()
        {
            string image1 = "Z:\\a14-touristix\\Touristix\\Images\\Accueil";


            return View();
        }

        
    }
}
