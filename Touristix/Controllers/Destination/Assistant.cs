﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Touristix.Models;

namespace Touristix.Controllers
{
    public static class Assistant
    {
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