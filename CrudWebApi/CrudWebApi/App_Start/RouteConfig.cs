using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace CrudWebApi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapHttpRoute("VersionRoute", LinkTemplates.Versions.Version.Href.Trim('/'), new { controller = "Version" });
            routes.MapHttpRoute("UsersRoute", LinkTemplates.Users.User.Href.Trim('/'), new { controller = "User" });
            routes.MapHttpRoute("CategoriesRoute", LinkTemplates.Categories.Category.Href.Trim('/'), new { controller = "Category" });
            routes.MapHttpRoute("PostsRoute", LinkTemplates.Posts.Post.Href.Trim('/'), new { controller = "Post" });
            routes.MapHttpRoute("ReviewsRoute", LinkTemplates.Reviews.Review.Href.Trim('/'), new { controller = "Review" });
            routes.MapHttpRoute("DefaultApi", "{controller}/{id}", new { id = RouteParameter.Optional });
        }
    }
}
