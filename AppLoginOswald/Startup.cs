using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppLoginOswald.Startup))]
namespace AppLoginOswald
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
