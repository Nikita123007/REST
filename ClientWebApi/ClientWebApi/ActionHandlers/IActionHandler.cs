using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalClient.Net;

namespace ClientWebApi.ActionHandlers
{
    public interface IActionHandler
    {
        bool HasBody { get; set; }
        IHalHttpResponseMessage Execute(Request request);
    }
}
