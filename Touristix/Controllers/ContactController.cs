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
                InsererContact(modele.Nom, modele.Courriel, modele.Categorie, modele.Commentaires);
                TempData["notice"] = "Votre formulaire a été soumis";
                return RedirectToAction("Index", "Home");
            }
            return View();
            
        }
        private void InsererContact(string nom, string courriel,  string categorie, string commentaires)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";                
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("touristix21@gmail.com", "qazedctgb");

                MailMessage message = new MailMessage(courriel, "lauwarrior@yahoo.ca", categorie, commentaires);
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                
                client.Send(message);
            }
            catch (SmtpException sex)
            {
                string a = sex.Message;
            }

                            
            
        }
    }
}
