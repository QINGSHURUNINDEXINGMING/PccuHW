using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(all.Startup))]
namespace all
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
