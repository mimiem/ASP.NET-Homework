using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyTinyBlog.Web.Startup))]
namespace MyTinyBlog.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
