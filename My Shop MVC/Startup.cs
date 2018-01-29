using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(My_Shop_MVC.Startup))]
namespace My_Shop_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
