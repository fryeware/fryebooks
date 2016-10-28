using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fryebooks.Startup))]
namespace Fryebooks
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
