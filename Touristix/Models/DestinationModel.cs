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
    }

    public class DestinationDBContext : DbContext
    {
        public DbSet<DestinationModel> Destinations { get; set; }
    }
}