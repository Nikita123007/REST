using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalClient.Net;

namespace ClientWebApi.ActionHandlers
{
    public class UpdateActionHandler : IActionHandler
    {
        public bool HasBody { get; set; }
        public HalClient Client { get; set; }

        public UpdateActionHandler(HalClient halClient)
        {
            HasBody = true;
            Client = halClient;
        }

        public IHalHttpResponseMessage Execute(Request request)
        {
            try
            {
                var task = Client.Put(request.Uri, request.Body);
                task.Wait();
                var res = task.Result;
                return res;
            }
            catch (Exception e)
            {
                if (e.InnerException != null) Console.WriteLine(e.InnerException.Message);
                throw;
            }
        }
    }
}
