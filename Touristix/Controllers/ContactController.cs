﻿using System;
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
        private ContactDBContext db = new ContactDBContext();
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
              //  if (InsererContact(modele.Nom, modele.Courriel, modele.Pass, modele.Categorie, modele.Commentaires))
               // {
                    Create(modele);
                    TempData["notice"] = "Votre formulaire a été soumis";
                    return RedirectToAction("Index", "Home");
               // }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        private void Create(ContactModel modele)
        {
            ContactDB contact = new ContactDB();
            contact.nom = modele.Nom;
            contact.courriel = modele.Courriel;
            contact.categorie = modele.Categorie;
            contact.commentaires = modele.Commentaires;
            db.Contacts.Add(contact);
            db.SaveChanges();
        }

        public ActionResult Index()
        {
            return View(db.Contacts.ToList());
        }

        public ActionResult Effacer(int id = 0)
        {
            ContactDB contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        [HttpPost, ActionName("Effacer")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmationEffacer(int id)
        {
            ContactDB contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
