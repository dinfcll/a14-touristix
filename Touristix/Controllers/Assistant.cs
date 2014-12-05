using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Touristix.Models;

namespace Touristix.Controllers
{
    public static class Assistant
    {
        public static void MettreAJourDestination(DestinationModel DestinationModelActif, HttpRequestBase Request)
        {
            DestinationModelActif.BatimentIds = "";
            int DernierBatiment = Convert.ToInt32(Request["DernierBatiment"]);
            if (DernierBatiment >= 1)
            {
                for (int B = 0; B < DernierBatiment; B++)
                {
                    int Id = Convert.ToInt32(Request["Batiment" + B]);
                    DestinationModelActif.BatimentIds += Id + ";";
                }
            }

            DestinationModelActif.ActiviteIds = "";
            int DerniereActivite = Convert.ToInt32(Request["DerniereActivite"]);
            if (DerniereActivite >= 1)
            {
                for (int B = 0; B < DerniereActivite; B++)
                {
                    int Id = Convert.ToInt32(Request["Activite" + B]);
                    DestinationModelActif.ActiviteIds += Id + ";";
                }
            }
        }

        public static void MettreAJourBatiment(BatimentModel BatimentModelActif)
        {
            BatimentModelActif.URL = BatimentModelActif.URL.Insert(0, BatimentModelActif.TypeURL == "http" ? "http://" : "https://");
        }

        public static void RemplirListe(ref List<SelectListItem> ListeItem, IQueryable<String> SelectElem)
        {
            ListeItem.Add(new SelectListItem { Text = "", Value = "" });

            foreach (string strElem in SelectElem)
            {
                ListeItem.Add(new SelectListItem { Text = strElem, Value = strElem });
            }
        }
    }
}
