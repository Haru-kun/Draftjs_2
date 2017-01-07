using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BanhangMVC.Startup))]
namespace BanhangMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
