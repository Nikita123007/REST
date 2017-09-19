using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Hal;

namespace CrudWebApi
{
    public static class LinkTemplates
    {
        public static class Versions
        {
            /// <summary>
            /// /version/
            /// </summary>
            public static Link Version => new Link("version", "/version/{id}");
        }

        public static class Users
        {
            /// <summary>
            /// /users/{id}
            /// </summary>
            public static Link User => new Link("user", "/people/{id}");

            /// <summary>
            /// /users
            /// </summary>
            public static Link CreateUser => new Link("users", "/people/");

            /// <summary>
            /// /users?page={page}
            /// </summary>
            public static Link GetUsers => new Link("users", "/people{?page}");
        }

        public static class Reviews
        {
            /// <summary>
            /// /reviews/{id}
            /// </summary>
            public static Link Review => new Link("reviews", "/reviews/{id}");

            /// <summary>
            /// /users?page={page}
            /// </summary>
            public static Link CreateReview => new Link("reviews", "/reviews/");

            /// <summary>
            /// /reviews?page={page}
            /// </summary>
            public static Link GetReviews => new Link("reviews", "/reviews{?page}");
        }

        public static class Categories
        {
            /// <summary>
            /// /categories/{id}
            /// </summary>
            public static Link Category => new Link("categories", "/categories/{id}");

            /// <summary>
            /// /categories
            /// </summary>
            public static Link CreateCategory => new Link("categories", "/categories/");

            /// <summary>
            /// /categories?page={page}
            /// </summary>
            public static Link GetCategories => new Link("categories", "/categories{?page}");
        }

        public static class Posts
        {
            /// <summary>
            /// /posts/{id}
            /// </summary>
            public static Link Post => new Link("posts", "/posts/{id}");

            /// <summary>
            /// /posts
            /// </summary>
            public static Link CreatePost => new Link("posts", "/posts/");

            /// <summary>
            /// /posts?page={page}
            /// </summary>
            public static Link GetPosts => new Link("posts", "/posts{?page}");
        }
    }
}