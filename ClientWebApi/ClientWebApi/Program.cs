using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using ClientWebApi.Resources;
using HalClient.Net.Parser;
using Tavis.UriTemplates;

namespace ClientWebApi
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var app = new Application();
                app.Execute();
            }
            catch
            {
                // ignored
            }
        }
    }
}
