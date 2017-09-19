using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrudWebApi.Resources.Reviews;
using WebApi.Hal;

namespace CrudWebApi.Resources.Posts
{
    public class PostListRepresentation : PagedRepresentationList<PostRepresentation>
    {
        public PostListRepresentation(IList<PostRepresentation> posts, int totalResults, int totalPages, int page, Link uriTemplate) :
            base(posts, totalResults, totalPages, page, uriTemplate, null)
        { }
        public PostListRepresentation(IList<PostRepresentation> posts, int totalResults, int totalPages, int page, Link uriTemplate, object uriTemplateSubstitutionParams) :
            base(posts, totalResults, totalPages, page, uriTemplate, uriTemplateSubstitutionParams)
        { }

        protected override void CreateHypermedia()
        {
            base.CreateHypermedia();
            //var cur = new CuriesLink("action", "{#rel}");

            //Links.Add(cur.CreateLink("create", LinkTemplates.Posts.CreatePost.Href));
        }
    }
}