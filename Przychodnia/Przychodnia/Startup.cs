using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Przychodnia.Startup))]
namespace Przychodnia
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
