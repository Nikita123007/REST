using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrudWebApi.Resources.Reviews;
using WebApi.Hal;

namespace CrudWebApi.Resources.Users
{
    public class UserRepresentation : Representation
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ReviewRepresentation> Reviews { get; set; }
        
        public override string Rel
        {
            get { return LinkTemplates.Users.User.Rel; }
            set { }
        }

        public override string Href
        {
            get { return LinkTemplates.Users.User.CreateLink(new { id = Id }).Href; }
            set { }
        }


        protected override void CreateHypermedia()
        {
            base.CreateHypermedia();
            //var cur = new CuriesLink("action", "{#rel}");

            ////Links.Add(cur.CreateLink("create", LinkTemplates.Users.CreateUser.Href));
            //Links.Add(cur.CreateLink("update", Href));
            //Links.Add(cur.CreateLink("retreive", Href));
            //Links.Add(cur.CreateLink("delete", Href));
        }
    }
}