using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Draftjs.Startup))]
namespace Draftjs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
