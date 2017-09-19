using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalClient.Net.Parser;
using Tavis.UriTemplates;

namespace ClientWebApi.Helpers
{
    public class UriParser
    {
        public static ILinkObject ParseLinkObject(ILinkObject link, Dictionary<string, string> parameters)
        {
            if (parameters.Count > 0)
            {
                var resolvedLink = link.ResolveTemplated((UriTemplate x) =>
                {
                    foreach (var keyValue in parameters)
                    {
                        x.AddParameter(keyValue.Key, keyValue.Value);
                    }
                    return x.Resolve();
                });
                return resolvedLink;
            }
            else
            {
                return link;
            }
        }
    }
}
