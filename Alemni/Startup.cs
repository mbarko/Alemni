using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Alemni.Startup))]
namespace Alemni
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
