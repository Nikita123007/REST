using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrudWebApi.Models;
using CrudWebApi.Resources.Categories;
using CrudWebApi.Resources.Reviews;
using WebApi.Hal;

namespace CrudWebApi.Resources.Posts
{
    public class PostRepresentation : Representation
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        public string Content { get; set; }

        public List<CategoryRepresentation> Categories { get; set; }

        public List<ReviewRepresentation> Reviews { get; set; }
        
        public override string Rel
        {
            get { return LinkTemplates.Posts.Post.Rel; }
            set { }
        }

        public override string Href
        {
            get { return LinkTemplates.Posts.Post.CreateLink(new { id = Id }).Href; }
            set { }
        }

        protected override void CreateHypermedia()
        {
            base.CreateHypermedia();
            //var cur = new CuriesLink("action", "{#rel}");

            ////Links.Add(cur.CreateLink("create", LinkTemplates.Posts.CreatePost.Href));
            //Links.Add(cur.CreateLink("update", Href));
            //Links.Add(cur.CreateLink("retreive", Href));
            //Links.Add(cur.CreateLink("delete", Href));
        }
    }
}