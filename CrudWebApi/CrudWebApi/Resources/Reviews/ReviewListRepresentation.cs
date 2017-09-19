using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrudWebApi.Resources.Users;
using WebApi.Hal;

namespace CrudWebApi.Resources.Reviews
{
    public class ReviewListRepresentation : PagedRepresentationList<ReviewRepresentation>
    {
        public ReviewListRepresentation(IList<ReviewRepresentation> reviews, int totalResults, int totalPages, int page, Link uriTemplate) :
            base(reviews, totalResults, totalPages, page, uriTemplate, null)
        { }
        public ReviewListRepresentation(IList<ReviewRepresentation> reviews, int totalResults, int totalPages, int page, Link uriTemplate, object uriTemplateSubstitutionParams) :
            base(reviews, totalResults, totalPages, page, uriTemplate, uriTemplateSubstitutionParams)
        { }

        protected override void CreateHypermedia()
        {
            base.CreateHypermedia();
            //var cur = new CuriesLink("action", "{#rel}");

            //Links.Add(cur.CreateLink("create", LinkTemplates.Reviews.CreateReview.Href));
        }
    }
}