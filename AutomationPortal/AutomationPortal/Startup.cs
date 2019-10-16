using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutomationPortal.Startup))]
namespace AutomationPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
