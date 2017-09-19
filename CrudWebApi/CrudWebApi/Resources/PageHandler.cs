using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudWebApi.Resources
{
    public class PageHandler<T>
    {
        public int PagesTotal { get; private set; }
        public int Page { get; private set; }
        public int PageSize { get; private set; }

        public int Total => Result.Count;

        public List<T> Result { get; private set; }

        public PageHandler(ICollection<T> list, int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
            PagesTotal = (int)Math.Ceiling((double)list.Count / pageSize);
            Result = list.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}