using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrudWebApi.Resources.Posts;
using CrudWebApi.Resources.Users;
using WebApi.Hal;

namespace CrudWebApi.Resources.Reviews
{
    public class ReviewRepresentation : Representation
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public PostRepresentation Post { get; set; }

        public UserRepresentation User { get; set; }

        public override string Rel
        {
            get { return LinkTemplates.Reviews.Review.Rel; }
            set { }
        }

        public override string Href
        {
            get { return LinkTemplates.Reviews.Review.CreateLink(new { id = Id }).Href; }
            set { }
        }

        protected override void CreateHypermedia()
        {
            base.CreateHypermedia();
            //var cur = new CuriesLink("action", "{#rel}");

            ////Links.Add(cur.CreateLink("create", LinkTemplates.Reviews.CreateReview.Href));
            //Links.Add(cur.CreateLink("update", Href));
            //Links.Add(cur.CreateLink("retreive", Href));
            //Links.Add(cur.CreateLink("delete", Href));
        }
    }
}