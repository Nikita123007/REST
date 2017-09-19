using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWebApi
{
    public class Request
    {
        public Uri Uri { get; set; }
        public object Body { get; set; }

        public Request(Uri uri)
        {
            Uri = uri;
            Body = null;
        }

        public Request(Uri uri, object body)
        {
            Uri = uri;
            Body = body;
        }
    }
}
