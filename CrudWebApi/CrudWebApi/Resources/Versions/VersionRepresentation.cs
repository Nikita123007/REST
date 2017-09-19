using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrudWebApi.Resources.Reviews;
using WebApi.Hal;

namespace CrudWebApi.Resources.Versions
{
    public class VersionRepresentation : Representation
    {
        public int Id { get; set; }

        public List<ReviewRepresentation> Reviews { get; set; }

        public override string Rel
        {
            get { return LinkTemplates.Versions.Version.Rel; }
            set { }
        }

        public override string Href
        {
            get { return LinkTemplates.Versions.Version.CreateLink().Href; }
            set { }
        }
        
        protected override void CreateHypermedia()
        {
            //var curiesNew = new CuriesLink("testnew", "doc/testnew/{#rel}");
            //Links.Add(curiesNew.CreateLink("create", "/do-good"));

            var curiesUser = new CuriesLink("users", "doc/users/{#rel}");

            Links.Add(curiesUser.CreateLink("create", LinkTemplates.Users.CreateUser.Href));
            Links.Add(curiesUser.CreateLink("update", LinkTemplates.Users.User.Href));
            Links.Add(curiesUser.CreateLink("retreive", LinkTemplates.Users.User.Href));
            Links.Add(curiesUser.CreateLink("delete", LinkTemplates.Users.User.Href));
            Links.Add(curiesUser.CreateLink("list", LinkTemplates.Users.GetUsers.Href));


            var curiesReview = new CuriesLink("reviews", "/doc/reviews/{#rel}");

            Links.Add(curiesReview.CreateLink("create", LinkTemplates.Reviews.CreateReview.Href));
            Links.Add(curiesReview.CreateLink("update", LinkTemplates.Reviews.Review.Href));
            Links.Add(curiesReview.CreateLink("retreive", LinkTemplates.Reviews.Review.Href));
            Links.Add(curiesReview.CreateLink("delete", LinkTemplates.Reviews.Review.Href));
            Links.Add(curiesReview.CreateLink("list", LinkTemplates.Reviews.GetReviews.Href));

            var curiesPost = new CuriesLink("posts", "/doc/posts/{#rel}");

            Links.Add(curiesPost.CreateLink("create", LinkTemplates.Posts.CreatePost.Href));
            Links.Add(curiesPost.CreateLink("update", LinkTemplates.Posts.Post.Href));
            Links.Add(curiesPost.CreateLink("retreive", LinkTemplates.Posts.Post.Href));
            Links.Add(curiesPost.CreateLink("delete", LinkTemplates.Posts.Post.Href));
            Links.Add(curiesPost.CreateLink("list", LinkTemplates.Posts.GetPosts.Href));

            var curiesCategory = new CuriesLink("categories", "/doc/categories/{#rel}");

            Links.Add(curiesCategory.CreateLink("create", LinkTemplates.Categories.CreateCategory.Href));
            Links.Add(curiesCategory.CreateLink("update", LinkTemplates.Categories.Category.Href));
            Links.Add(curiesCategory.CreateLink("retreive", LinkTemplates.Categories.Category.Href));
            Links.Add(curiesCategory.CreateLink("delete", LinkTemplates.Categories.Category.Href));
            Links.Add(curiesCategory.CreateLink("list", LinkTemplates.Categories.GetCategories.Href));
        }
    }
}