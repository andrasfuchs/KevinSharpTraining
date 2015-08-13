using KevinSharp.DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace KevinSharp.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Serilog.Log.Logger.Information("Kevin Sharp application started");
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
            Session["SessionStartedUtc"] = DateTime.UtcNow;
            Session["SessionEvents"] = new SessionEvent[0];

            Session["UserEmail"] = "";
            if (User.Identity.IsAuthenticated)
            {
                Session["UserEmail"] = ((System.Security.Claims.ClaimsIdentity)User.Identity).Claims.First(c => c.Type == "emailaddress").Value;
            }

            MvcApplication.AddSessionEvent(new HttpSessionStateWrapper(Session), "session", "start", Session.SessionID);

            Serilog.Log.Logger.Information("Session {SessionId} started", Session.SessionID);
        }

        protected void Session_End(object sender, EventArgs e)
        {
            MvcApplication.AddSessionEvent(new HttpSessionStateWrapper(Session), "session", "end", Session.SessionID);

            KevinSharpDbContext dbContext = new KevinSharpDbContext();
            KevinSharp.DataModel.SessionLog sl = new DataModel.SessionLog()
                { 
                    SessionId = Session.SessionID,
                    SessionStartedUtc = (DateTime)Session["SessionStartedUtc"],
                    SessionDuration = (DateTime.UtcNow - (DateTime)Session["SessionStartedUtc"]).ToString("G"),
                    UserEmail = Session["UserEmail"].ToString(),
                    Events = JsonConvert.SerializeObject((SessionEvent[])Session["SessionEvents"])
                };
            dbContext.SessionLogs.Add(sl);
            dbContext.SaveChangesAsync();
            dbContext.Dispose();

            Serilog.Log.Logger.Information("Session {SessionId} ended", Session.SessionID);
        }

        public static void AddSessionEvent(HttpSessionStateBase session, string category, string action, string label = "", int value = 0)
        {
            List<SessionEvent> events = new List<SessionEvent>((SessionEvent[])session["SessionEvents"]);
            events.Add(new SessionEvent() { DateTimeUtc = DateTime.UtcNow.ToString("o"), Category = category, Action = action, Label = label, Value = value, UserEmail = session["UserEmail"].ToString() });
            session["SessionEvents"] = events.ToArray();
        }

        private struct SessionEvent
        {
            public string DateTimeUtc;
            public string Category;
            public string Action;
            public string Label;
            public int Value;
            public string UserEmail;

            public override string ToString()
            {
                return DateTimeUtc + " " + Category + ", " + Action + ", " + Label + ", " + Value + " (" + UserEmail + ")";
            }
        }
    }
}
