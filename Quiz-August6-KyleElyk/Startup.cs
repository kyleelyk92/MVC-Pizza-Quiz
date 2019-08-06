using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Quiz_August6_KyleElyk.Startup))]
namespace Quiz_August6_KyleElyk
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
