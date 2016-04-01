using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FARMC.Web.Startup))]
namespace FARMC.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
