﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using Touristix.Filters;
using Touristix.Models;

namespace Touristix.Controllers
{
    public class ConnexionController : Controller
    {
        //
        // GET: /Connexion/

        public ActionResult ConnexionAdmin()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}
