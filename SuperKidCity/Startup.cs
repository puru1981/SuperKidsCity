using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SuperKidCity.Startup))]
namespace SuperKidCity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
