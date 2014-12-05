using System.Web.Mvc;
using System.Web.Routing;

namespace Touristix
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Accueil",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Accueil", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}