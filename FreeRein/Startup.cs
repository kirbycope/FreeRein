using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FreeRein.Startup))]
namespace FreeRein
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
