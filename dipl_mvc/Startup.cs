using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Owin;
using Microsoft.Owin;
[assembly: OwinStartup(typeof(dipl_mvc.Startup))]

namespace dipl_mvc
{
          public class Startup
          {
                    public void Configuration(IAppBuilder app)
                    {
                        app.MapSignalR();
                    }
          }
}