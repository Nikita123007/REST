using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalClient.Net;

namespace ClientWebApi.ActionHandlers
{
    public class ReadActionHandler : IActionHandler
    {
        public bool HasBody { get; set; }
        public HalClient Client { get; set; }

        public ReadActionHandler(HalClient halClient)
        {
            HasBody = false;
            Client = halClient;
        }
        
        public IHalHttpResponseMessage Execute(Request request)
        {
            try
            {
                var task = Client.Get(request.Uri);
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
