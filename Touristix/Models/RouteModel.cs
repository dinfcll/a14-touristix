using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Touristix.Models
{
    public class RouteModel
    {
        public List<Route> routes; 
    }

    public class Route
    {
        public Bound bunds;
        public string copyrights;
        public List<Pas> legs;
    }

    public class Bound
    {
        public NordEst northeast;
        public SudOuest southwest;
    }

    public class NordEst
    {
        double lat;
        double lng;
    }

    public class SudOuest
    {
        double lat;
        double lng;
    }

    public class Pas
    {

        "distance" : {
            "text" : "793 km",
            "value" : 793001
         },
         "duration" : {
            "text" : "7 heures 32 minutes",
            "value" : 27106
         },
         "end_address" : "Lévis, QC, Canada",
         "end_location" : {
            "lat" : 46.7405473,
            "lng" : -71.2510537
         },
         "start_address" : "Toronto, ON, Canada",
         "start_location" : {
            "lat" : 43.6533103,
            "lng" : -79.3827675
         },
         "steps" 
    }

    public class Distance
    {
        string text;
        int value;
    }

    public class Duree
    {
        string text;
        int value;
    }

    public class LieuArrivee
    {
        string text;
        int value;
    }

    public class LieuDepart
    {
        string text;
        int value;
    }
}


/*  {
"routes" : [
{
   "bounds" : {
      "northeast" : {
         "lat" : 46.7405473,
         "lng" : -71.2510537
      },
      "southwest" : {
         "lat" : 43.6533103,
         "lng" : -79.3827675
      }
   },
   "copyrights" : "Données cartographiques ©2014 Google",
   "legs" : [
      {
         "distance" : {
            "text" : "793 km",
            "value" : 793001
         },
         "duration" : {
            "text" : "7 heures 32 minutes",
            "value" : 27106
         },
         "end_address" : "Lévis, QC, Canada",
         "end_location" : {
            "lat" : 46.7405473,
            "lng" : -71.2510537
         },
         "start_address" : "Toronto, ON, Canada",
         "start_location" : {
            "lat" : 43.6533103,
            "lng" : -79.3827675
         },
         "steps" : [
            {
               "distance" : {
                  "text" : "0,3 km",
                  "value" : 280
               },
               "duration" : {
                  "text" : "1 minute",
                  "value" : 37
               },
               "end_location" : {
                  "lat" : 43.6557259,
                  "lng" : -79.38373319999999
               },
               "html_instructions" : "Prendre la direction \u003cb\u003enord\u003c/b\u003e sur \u003cb\u003eBay Street\u003c/b\u003e vers \u003cb\u003eHagerman St\u003c/b\u003e",
               "polyline" : {
                  "points" : "e`miGhmocNgBf@cBb@KBoFdB[H"
               },
               "start_location" : {
                  "lat" : 43.6533103,
                  "lng" : -79.3827675
               },
               "travel_mode" : "DRIVING"
            },
}*/