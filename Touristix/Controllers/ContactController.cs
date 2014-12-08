using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using Touristix.Models;

namespace Touristix.Controllers
{
    public class ContactController : Controller
    {
        private ContactDBContext db = new ContactDBContext();       

        [HttpGet]
        public ActionResult ContactForm()
        {
            return View("ContactForm");
        }

        [HttpPost]
        public ActionResult ContactForm(ContactModel modele)
        {
            ViewData["Verif"] = "";
            if (ModelState.IsValid)
            {
                if (InsererContact(modele.Nom, modele.Courriel, modele.Categorie, modele.Commentaires))
                {
                    Create(modele);
                    TempData["notice"] = "Votre formulaire a été soumis";
                    ViewData["Verif"] = "";
                    return RedirectToAction("Index", "Accueil");
                }
                ViewData["Verif"] = "Erreur";
            }
            return View();
        }

        private bool InsererContact(string nom, string courriel, string categorie, string commentaires)
        {
            bool valide = true;
            const string mailto = "touristix21@gmail.com";
            try
            {
                SmtpClient client = new SmtpClient
                {
                    Port = 587,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("touristix21@gmail.com", "qazedctgb")
                };


                MailMessage email = new MailMessage(courriel, mailto)
                {
                    Subject = categorie,
                    BodyEncoding = Encoding.UTF8,
                    IsBodyHtml = true,
                    DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure,
                    Body = "<html>"
                           + "<head>"
                           + "<meta charset='utf-8' />"
                           + "<style>"
                           + "body {background-color:lightgray}"
                           + "h2 {color:blue}"
                           + "</style>"
                           + "</head>"
                           + "<body>"
                           + "<h2>Ceci est un message automatique de: " + nom + " </h2>"
                           + "<p>" + commentaires + "</p>"
                           + "</body>"
                           + "</html>"
                };


                client.Send(email);
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
            ContactDB contact = new ContactDB
            {
                nom = modele.Nom,
                courriel = modele.Courriel,
                categorie = modele.Categorie,
                commentaires = modele.Commentaires
            };
            db.Contacts.Add(contact);
            db.SaveChanges();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            ViewData["query"] = "";
            ContactMessage mess = new ContactMessage();
            return View("Index", new Tuple<ContactMessage, IEnumerable<ContactDB>>(mess, db.Contacts.ToList()));
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Index(string tri)
        {
            ContactMessage contact = new ContactMessage {contacts = new List<ContactDB>()};

            if (tri != "Aucun")
            {
                var cat = db.Contacts.Where(s => s.categorie.Contains(tri));

                ViewData["query"] = "vrai";

                foreach (ContactDB message in cat)
                {
                    var unepersonne = new ContactDB
                    {
                        id = message.id,
                        nom = message.nom,
                        courriel = message.courriel,
                        categorie = message.categorie,
                        commentaires = message.commentaires
                    };
                    contact.contacts.Add(unepersonne);
                }
            }
            else
            {
                ViewData["query"] = "";
            }
            return View(new Tuple<ContactMessage, IEnumerable<ContactDB>>(contact, db.Contacts.ToList()));
        }
     
        [Authorize(Roles = "admin")] 
        [HttpPost]
        public void ConfirmationEffacer(int id)       
        {
            ContactDB contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();            
        }
    }
}
