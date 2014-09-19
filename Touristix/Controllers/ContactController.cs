using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Touristix.Models;
using System.Net.Mail;

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
        public ActionResult ContactForm(ContactModel modele)
        {
            if (ModelState.IsValid)
            {
                if (InsererContact(modele.Nom, modele.Courriel, modele.Pass, modele.Categorie, modele.Commentaires))
                {
                    TempData["notice"] = "Votre formulaire a été soumis";
                    return RedirectToAction("Index", "Home");
                }
                
            }
            return View();
            
        }
        private bool InsererContact(string nom, string courriel, string pass, string categorie, string commentaires)
        {
            bool valide = true;
            try
            {
                SmtpClient client = new SmtpClient();
                string chaine;

                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(courriel, pass);

                chaine = "Ceci est un message de: " + nom + "\n\n" + commentaires;

                MailMessage message = new MailMessage(courriel, "e_casault@hotmail.com", categorie, chaine); //lauwarrior@yahoo.ca
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(message);
            }
            catch (Exception)
            {
                valide = false;
            }
            return valide;     

        }
    }
}
