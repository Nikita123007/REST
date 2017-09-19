using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalClient.Net.Parser;

namespace ClientWebApi
{
    public class Response
    {
        public IResourceObject ResourceObject { get; set; }
        public bool IsSuccessfull { get; set; }
    }
}
