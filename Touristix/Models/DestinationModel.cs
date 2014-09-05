using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Touristix.Models
{
    public class DestinationModel
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Pays { get; set; }
        public string Region { get; set; }
        public string Ville { get; set; }
    }

    public class DestinationDBContext : DbContext
    {
        public DbSet<DestinationModel> Destinations { get; set; }
    }
}