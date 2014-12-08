using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Touristix.Models
{
    public class ContactModel
    {
        [Required(ErrorMessage = "Requis")]
        public string Nom { get; set; }        

        [Required(ErrorMessage = "Requis")]
        [RegularExpression(@".*@.*", ErrorMessage = "Doit être un courriel valide.")]
        public string Courriel { get; set; }       

        [Required(ErrorMessage = "Requis")]
        public string Categorie { get; set; }

        [Required(ErrorMessage = "Requis")]
        public string Commentaires { get; set; }
    }

    public class ContactDB
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string courriel { get; set; }
        public string categorie { get; set;}
        public string commentaires { get; set;}
    }

    public class ContactMessage
    {
        public List <ContactDB> contacts{ get; set; }
    }

    public class ContactDBContext : DbContext
    {
        public DbSet<ContactDB> Contacts { get; set; }
    }
}
