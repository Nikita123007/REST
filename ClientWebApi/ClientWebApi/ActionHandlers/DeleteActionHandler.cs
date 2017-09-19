using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalClient.Net;

namespace ClientWebApi.ActionHandlers
{
    public class DeleteActionHandler : IActionHandler
    {
        public bool HasBody { get; set; }
        public HalClient Client { get; set; }

        public DeleteActionHandler(HalClient halClient)
        {
            HasBody = false;
            Client = halClient;
        }

        public IHalHttpResponseMessage Execute(Request request)
        {
            try
            {
                var task = Client.Delete(request.Uri);
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
