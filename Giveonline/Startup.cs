using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Giveonline.Startup))]
namespace Giveonline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
