using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Touristix.Models
{
    public class ImagesAccueilModel
    {
        public string[] TableauImagesAccueil { get; set; }

        public List<ALaUneModel> ListALaUne { get; set; }

        public ImagesAccueilModel(string url)
        {
            string[] ExtensionsRecherche = { ".png", ".jpg", ".bmp" };

            TableauImagesAccueil = Directory.GetFiles(url, "*.*")
                .Where(f => ExtensionsRecherche.Contains(new FileInfo(f).Extension.ToLower())).ToArray();

            for (int i = 0; i < TableauImagesAccueil.Length; i++)
            {
                string strCheminImage = TableauImagesAccueil[i];

                TableauImagesAccueil[i] = Path.GetFileName(strCheminImage);
            }
        }
    }
}
