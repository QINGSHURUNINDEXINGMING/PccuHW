using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(testNFC.Startup))]
namespace testNFC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
