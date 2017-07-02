using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(szalkszop.Startup))]
namespace szalkszop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
