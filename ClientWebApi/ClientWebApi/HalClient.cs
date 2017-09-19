using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HalClient.Net;
using HalClient.Net.Parser;
using Tavis.UriTemplates;

namespace ClientWebApi
{
    public class HalClient
    {
        private string Address { get; set; }
        private string NavigationUrl { get; set; }
        private IHalHttpClient Client { get; set; }
        private IReadOnlyDictionary<string, IEnumerable<ILinkObject>> ApiNavigation { get; set; }

        public IReadOnlyDictionary<string, IEnumerable<ILinkObject>> Api
        {
            get
            {
                UpdateNavigation();
                return ApiNavigation;
            }
        }

        public HalClient(string address, string navigationUrl)
        {
            Address = address.Trim('/');

            IHalJsonParser parser = new HalJsonParser();
            IHalHttpClientFactory factory = new HalHttpClientFactory(parser);
            Client = factory.CreateClient();
            NavigationUrl = navigationUrl;
           
            UpdateNavigation();
        }

        private void UpdateNavigation()
        {
            var task = ParseNavigation(NavigationUrl);
            task.Wait();
        }

        private async Task ParseNavigation(string navigationUrl)
        {
            var response = await Client.GetAsync(new Uri(Address + navigationUrl));

            if (response.IsHalResponse)
            {
                ApiNavigation = response.Resource.Links;
            }
            else
            {
                throw new Exception("Invalid navigation url.");
            }
        }

        public async Task<IHalHttpResponseMessage> Get(Uri uri)
        {
            var response = await Client.GetAsync(uri);

            return response;
        }

        public async Task<IHalHttpResponseMessage> Delete(Uri uri)
        {
            var response = await Client.DeleteAsync(uri);

            return response;
        }

        public async Task<IHalHttpResponseMessage> Post(Uri uri, object data)
        {
            var response = await Client.PostAsync(uri, data);

            return response;
        }

        public async Task<IHalHttpResponseMessage> Put(Uri uri, object data)
        {
            var response = await Client.PutAsync(uri, data);

            return response;
        }

        public bool IsTemplatedResource(string resource)
        {
            var link = ApiNavigation.Single(x => x.Key == resource);
            return link.Value.First().Templated;
        }

        public bool CommandExists(string resource)
        {
            return ApiNavigation.Any(x => x.Key == resource);
        }

        public ILinkObject GetLink(string resource)
        {
            return ApiNavigation.Single(x => x.Key == resource).Value.First();
        }

        public Uri GetUri(ILinkObject linkObject)
        {
            return new Uri(Address + linkObject.Href);
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
