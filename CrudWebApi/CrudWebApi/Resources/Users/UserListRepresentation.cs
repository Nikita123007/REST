using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Hal;

namespace CrudWebApi.Resources.Users
{
    public class UserListRepresentation : PagedRepresentationList<UserRepresentation>
    {
        public UserListRepresentation(IList<UserRepresentation> users, int totalResults, int totalPages, int page, Link uriTemplate) :
            base(users, totalResults, totalPages, page, uriTemplate, null)
        { }
        public UserListRepresentation(IList<UserRepresentation> users, int totalResults, int totalPages, int page, Link uriTemplate, object uriTemplateSubstitutionParams) :
            base(users, totalResults, totalPages, page, uriTemplate, uriTemplateSubstitutionParams)
        { }

        protected override void CreateHypermedia()
        {
            base.CreateHypermedia();
            //var cur = new CuriesLink("action", "{#rel}");

            //Links.Add(cur.CreateLink("create", LinkTemplates.Users.CreateUser.Href));
        }
    }
}