using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PokedexProject.Startup))]
namespace PokedexProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
