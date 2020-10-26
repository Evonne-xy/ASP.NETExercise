using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FIT5032AssignmentV1.Startup))]
namespace FIT5032AssignmentV1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
