using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KevinSharp.Web.Startup))]
namespace KevinSharp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
