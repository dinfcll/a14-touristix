using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Touristix.Models;
using System.Net.Mail;
using System.Net.Mime;

namespace Touristix.Controllers
{
    public class ContactController : Controller
    {
        private ContactDBContext db = new ContactDBContext();       

        [HttpGet]
        public ActionResult ContactForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactForm(ContactModel modele)
        {
            ViewData["Verif"] = "";
            if (ModelState.IsValid)
            {
                //if (InsererContact(modele.Nom, modele.Courriel, modele.Categorie, modele.Commentaires))
               // {
                    Create(modele);
                    TempData["notice"] = "Votre formulaire a été soumis";
                    ViewData["Verif"] = "";
                    return RedirectToAction("Index", "Accueil");
                //}
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
                SmtpClient client = new SmtpClient();                
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("touristix21@gmail.com", "qazedctgb");                            

                MailMessage email = new MailMessage(courriel, mailto);
                email.Subject = categorie;
                email.BodyEncoding = System.Text.Encoding.UTF8;
                email.IsBodyHtml = true;
                email.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                
                email.Body = "<html>"
                + "<head>"
	            + "<meta charset='utf-8' />"
                + "<style>"
                + "body {background-color:lightgray}"
                + "h2 {color:blue}"
                + "</style>"
	            + "</head>"
	            + "<body>"
                +"<h2>Ceci est un message automatique de: "+nom+" </h2>"
	            + "<p>"+commentaires+"</p>"
	            + "</body>"
	            + "</html>";

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
            ContactDB contact = new ContactDB();
            contact.nom = modele.Nom;
            contact.courriel = modele.Courriel;
            contact.categorie = modele.Categorie;
            contact.commentaires = modele.Commentaires;
            db.Contacts.Add(contact);
            db.SaveChanges();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            ViewData["query"] = "";
            ContactMessage mess = new ContactMessage();
            return View(new Tuple<Touristix.Models.ContactMessage, IEnumerable<Touristix.Models.ContactDB>>(mess, db.Contacts.ToList()));
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Index(string tri)
        {
            ContactMessage contact = new ContactMessage();
            contact.contacts = new List<ContactDB>();
            ContactDB unepersonne = new ContactDB();

            var asdf = db.Contacts.Where(s => s.categorie.Contains(tri));
            
            ViewData["query"] = "vrai";

            foreach (ContactDB message in asdf)
            {

                unepersonne = new ContactDB();
                unepersonne.id = message.id;
                unepersonne.nom = message.nom;
                unepersonne.courriel = message.courriel;
                unepersonne.categorie = message.categorie;
                unepersonne.commentaires = message.commentaires;
                contact.contacts.Add(unepersonne);
                //contact.contacts.Add

            }

            //var bla = (ContactDB)ViewData["asdf"];

            //ViewBag.numQuery = numQuery;

            return View(new Tuple<Touristix.Models.ContactMessage, IEnumerable<Touristix.Models.ContactDB>>(contact, db.Contacts.ToList()));
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
