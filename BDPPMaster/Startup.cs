using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BDPPMaster.Startup))]
namespace BDPPMaster
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
