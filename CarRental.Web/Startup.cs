using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarRental.Web.Startup))]
namespace CarRental.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
