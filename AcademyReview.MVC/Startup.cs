using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AcademyReview.MVC.Startup))]
namespace AcademyReview.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
