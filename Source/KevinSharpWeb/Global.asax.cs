using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace KevinSharpWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Serilog.Log.Logger.Information("Kevin Sharp application started");

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_End()
        {
            Serilog.Log.Logger.Information("Kevin Sharp application ended");
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Serilog.Log.Logger.Error(Server.GetLastError(), "Exception was thrown {Exception}");
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Serilog.Log.Logger.Information("Session {SessionId} started", Session.SessionID);
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Serilog.Log.Logger.Information("Session {SessionId} ended", Session.SessionID);
        }
    }
}
