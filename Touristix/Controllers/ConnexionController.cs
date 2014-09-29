using System;
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
    [Authorize]
    [InitializeSimpleMembership]
    public class ConnexionController : Controller
    {
        //
        // GET: /Connexion/

        public ActionResult ConnexionAdmin()
        {
            return View();
        }

        public ActionResult Login( string UrlRetour)
        {
            ViewBag.UrlRetour = UrlRetour;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string UrlRetour)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToLocal(UrlRetour);
            }

            // Si nous sommes arrivés là, quelque chose a échoué, réafficher le formulaire
            ModelState.AddModelError("", "Le nom d'utilisateur ou mot de passe fourni est incorrect.");
            return View(model);
        }
    }
}
