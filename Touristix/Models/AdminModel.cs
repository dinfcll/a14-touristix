using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Touristix.Models
{
    public class AdminModel
    {

    }

    public class Utilisateurs : DbContext
    {
        public Utilisateurs()
            : base("DefaultConnection")
        {
        }

        public DbSet<ProfilUtilisateur> ProfilsUtilisateurs { get; set; }
    }

    [Table("ProfilUtilisateur")]
    public class ProfilUtilisateur
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdUtilisateur { get; set; }
        public string NomUtilisateur { get; set; }
    }

    public class MotDePasseModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe actuel")]
        public string VieuxMotPasse { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nouveau mot de passe")]
        public string NouveauMotPasse { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le nouveau mot de passe")]
        [Compare("NouveauMotPasse", ErrorMessage = "Le nouveau mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmationMotPasse { get; set; }
    }

    public class ConnecterModel
    {
        [Required]
        [Display(Name = "Nom d'utilisateur")]
        public string NomUtilisateur { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string MotPasse { get; set; }
    }

    public class EnregistrerModel
    {
        [Required]
        [Display(Name = "Nom d'utilisateur")]
        public string NomUtilisateur { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string MotPasse { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe ")]
        [Compare("MotPasse", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmationMotPasse { get; set; }
    }
}