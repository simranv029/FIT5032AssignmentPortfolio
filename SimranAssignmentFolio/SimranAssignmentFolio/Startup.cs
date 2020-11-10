using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimranAssignmentFolio.Startup))]
namespace SimranAssignmentFolio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
