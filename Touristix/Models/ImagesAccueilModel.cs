﻿using System.IO;
using System.Linq;

namespace Touristix.Models
{
    public class ImagesAccueilModel
    {
        public string[] TableauImagesAccueil { get; set; }

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
