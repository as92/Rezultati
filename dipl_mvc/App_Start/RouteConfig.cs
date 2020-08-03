using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace dipl_mvc
{
          public class RouteConfig
          {
                    public static void RegisterRoutes(RouteCollection routes)
                    {
                              routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
                              //routes.MapRoute(

                              //url: "{controler}/

                              //);

                              routes.MapRoute(
                              name: "Default",
                              url: "{controller}/{action}",
                              defaults: new { controller = "Login", action = "Index"  });
  
                              routes.MapRoute("Registracija","{controller}/{action}", 
                              new { controller = "Users", action = "Registracija"});

                              routes.MapRoute("Home","{controller}/{action}",new { controller = "Home", action = "Index" });

                              routes.MapRoute("Login", "{controller}/{action}", new { controller = "Login", action = "Index" });
                              routes.MapRoute("Admin", "{controller}/{action}", new { controller = "Admin", action = "DodajUtakmicu" });


                    }
          }
}
