using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Hal;

namespace CrudWebApi.Resources.Categories
{
    public class CategoryListRepresentation : PagedRepresentationList<CategoryRepresentation>
    {
        public CategoryListRepresentation(IList<CategoryRepresentation> categories, int totalResults, int totalPages, int page, Link uriTemplate) :
            base(categories, totalResults, totalPages, page, uriTemplate, null)
        { }
        public CategoryListRepresentation(IList<CategoryRepresentation> categories, int totalResults, int totalPages, int page, Link uriTemplate, object uriTemplateSubstitutionParams) :
            base(categories, totalResults, totalPages, page, uriTemplate, uriTemplateSubstitutionParams)
        { }

        protected override void CreateHypermedia()
        {
            base.CreateHypermedia();
            //var cur = new CuriesLink("action", "{#rel}");

            //Links.Add(cur.CreateLink("create", LinkTemplates.Categories.CreateCategory.Href));
        }
    }
}