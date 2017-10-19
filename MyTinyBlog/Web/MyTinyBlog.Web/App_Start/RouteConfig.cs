using System.Web.Mvc;
using System.Web.Routing;

namespace MyTinyBlog.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Blog", action = "Posts", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Category",
                url:"Category/{category}",
                defaults: new { controller = "Blog", action = "Category" }
            );
        }
    }
}
