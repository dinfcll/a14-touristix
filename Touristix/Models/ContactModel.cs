using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

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
}