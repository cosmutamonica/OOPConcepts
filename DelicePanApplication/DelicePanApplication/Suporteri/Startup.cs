using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Suporteri.Startup))]
namespace Suporteri
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
