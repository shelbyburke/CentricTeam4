using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CentricTeam4.Startup))]
namespace CentricTeam4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
