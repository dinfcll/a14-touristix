using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Touristix.Models
{
    public class DestinationModel
    {
        public int ID { get; set; }

        [Required]
        public string Nom { get; set; }
        [Required]
        public string Pays { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string Ville { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Apercu { get; set; }
        [Required]
        public string Details { get; set; }
    }

    public class BatimentModel
    {
        public int ID { get; set; }

        [Required]
        public string Nom { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Apercu { get; set; }
        [Required]
        public string Details { get; set; }

        public string URL { get; set; }
    }

    public class ActiviteModel
    {
        public int ID { get; set; }

        [Required]
        public string Nom { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Lieu { get; set; }
        [Required]
        public string Apercu { get; set; }
        [Required]
        public string Details { get; set; }
    }

    public class AdminitrationList
    {
        public List<DestinationModel> ListDestinationModel;
        public List<BatimentModel> ListBatimentModel;
        public List<ActiviteModel> ListActiviteModel;
    }

    public class DestinationDBContext : DbContext
    {
        public DbSet<DestinationModel> Destinations { get; set; }
        public DbSet<BatimentModel> Batiments { get; set; }
        public DbSet<ActiviteModel> Activites { get; set; }
    }
}