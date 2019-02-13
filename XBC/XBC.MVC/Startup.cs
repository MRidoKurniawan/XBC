using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XBC.MVC.Startup))]
namespace XBC.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
