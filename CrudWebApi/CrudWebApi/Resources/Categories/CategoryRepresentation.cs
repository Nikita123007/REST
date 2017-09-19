using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrudWebApi.Resources.Posts;
using WebApi.Hal;

namespace CrudWebApi.Resources.Categories
{
    public class CategoryRepresentation : Representation
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public List<PostRepresentation> Posts { get; set; }
        
        public override string Rel
        {
            get { return LinkTemplates.Categories.Category.Rel; }
            set { }
        }

        public override string Href
        {
            get { return LinkTemplates.Categories.Category.CreateLink(new { id = Id }).Href; }
            set { }
        }

        protected override void CreateHypermedia()
        {
            base.CreateHypermedia();
            //var cur = new CuriesLink("action", "{#rel}");

            ////Links.Add(cur.CreateLink("create", LinkTemplates.Categories.CreateCategory.Href));
            //Links.Add(cur.CreateLink("update", Href));
            //Links.Add(cur.CreateLink("retreive", Href));
            //Links.Add(cur.CreateLink("delete", Href));
        }
    }
}