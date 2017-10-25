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
                url: "Category/{category}",
                defaults: new { controller = "Blog", action = "Category" }
            );

            routes.MapRoute(
                name: "Tag",
                url: "Tag/{tag}",
                defaults: new { controller = "Blog", action = "Tag" }
            );

            routes.MapRoute(
                name: "Post",
                url: "Archive/{year}/{month}/{title}",
                defaults: new { controller = "Blog", action = "Post" },
                constraints: new { year = @"\d{4}", month = @"\d{2}", day = @"\d{2}" }
            );

            routes.MapRoute(
                name: "Manage",
                url: "Manage",
                defaults: new { controller = "Admin", action = "Manage" }
            );

            routes.MapRoute(
               name: "Posts",
               url: "Posts",
               defaults: new { controller = "Blog", action = "Posts", p = 1 }
           );

            routes.MapRoute(
                name: "LogOff",
                url: "LogOff",
                defaults: new { controller = "Account", action = "LogOff" }
            );
        }
    }
}
