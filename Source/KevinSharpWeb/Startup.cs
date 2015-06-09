using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KevinSharpWeb.Startup))]
namespace KevinSharpWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
