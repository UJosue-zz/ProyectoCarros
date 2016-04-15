using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProyectoCarros.Startup))]
namespace ProyectoCarros
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
