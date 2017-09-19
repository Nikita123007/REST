using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientWebApi.ActionHandlers;
using ClientWebApi.ObjectReaders;
using ClientWebApi.Registry;
using HalClient.Net;
using HalClient.Net.Parser;

namespace ClientWebApi
{
    public class Application
    {
        private ActionRegistry ActionRegistry { get; set; }
        private ObjectReaderRegistry ObjectReaderRegistry { get; set; }
        private HalClient Client { get; set; }

        public Application()
        {
            try
            {
                Client = new HalClient("http://localhost:57268", "/version/1");
            }
            catch
            {
                Console.WriteLine("No connection");
                Console.ReadKey();
                throw new Exception();
            }

            InitializeActionRegistry();
            InitializeObjectReaderRegistry();
        }

        public void InitializeActionRegistry()
        {
            ActionRegistry = new ActionRegistry();
            ActionRegistry.Reg("create", new CreateActionHandler(Client));
            ActionRegistry.Reg("retreive", new ReadActionHandler(Client));
            ActionRegistry.Reg("list", new ReadActionHandler(Client));
            ActionRegistry.Reg("update", new UpdateActionHandler(Client));
            ActionRegistry.Reg("delete", new DeleteActionHandler(Client));
        }

        public void InitializeObjectReaderRegistry()
        {
            ObjectReaderRegistry = new ObjectReaderRegistry();
            var user = new UserObjectReader();
            var post = new PostObjectReader();
            var category = new CategoryObjectReader();
            var review = new ReviewObjectReader();

            user.ReviewObjectReader = review;
            post.ReviewObjectReader = review;
            post.CategoryObjectReader = category;
            category.PostObjectReader = post;
            review.PostObjectReader = post;
            review.UserObjectReader = user;

            ObjectReaderRegistry.Reg("users", user);
            ObjectReaderRegistry.Reg("reviews", review);
            ObjectReaderRegistry.Reg("posts", review);
            ObjectReaderRegistry.Reg("categories", category);
        }

        public void Execute()
        {
            var command = "";
            while (command != "q")
            {
                Console.WriteLine();
                Console.WriteLine("Enter command (q => exit, h => help)");
                Console.Write("> ");
                command = Console.ReadLine(); // retreive:users
                if (command == "h")
                {
                    PrintActions();
                    continue;
                }
                else if (command == "q")
                {
                    break;
                }
                if (!Client.CommandExists(command)) continue;

                var actionHandler = ActionRegistry.Get(command);

                if (actionHandler == null)
                {
                    Console.WriteLine("Unsupported");
                    continue;
                }

                var template = ReadTemplate(command);
                var link = Client.GetLink(command);
                var resolvedLink = Helpers.UriParser.ParseLinkObject(link, template);
                var linkUri = Client.GetUri(resolvedLink);

                Request request;
                if (actionHandler.HasBody)
                {
                    var objectReader = ObjectReaderRegistry.Get(command);
                    if (objectReader == null)
                    {
                        Console.WriteLine("Unsupported");
                        continue;
                    }
                    request = new Request(linkUri, objectReader.ReadFromConsole(true));
                }
                else
                {
                    request = new Request(linkUri);
                }

                try
                {
                    var response = actionHandler.Execute(request);
                    PrintResponse(response);
                }
                catch
                {
                    // Ignored
                }
            }
        }

        public void PrintResponse(IHalHttpResponseMessage response)
        {
            if (response.IsHalResponse)
            {
                PrintResource(response.Resource);
            }
            else
            {
                if (response.Message.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.Message.ReasonPhrase);
                }
            }
        }



        public Dictionary<string, string> ReadTemplate(string command)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            if (Client.IsTemplatedResource(command))
            {
                Console.WriteLine("Enter parameters separated as shown: 'key:value', empty string when ready");
                Console.WriteLine("Template: " + Client.GetLink(command).Template);
                dictionary = new Dictionary<string, string>();
                string keyValue;
                do
                {
                    try
                    {
                        keyValue = Console.ReadLine();
                        if (keyValue != null)
                        {
                            var keyValueParsed = keyValue.Trim().Split(new char[] { ':' }, 2);
                            dictionary.Add(keyValueParsed[0], keyValueParsed[1]);
                        }
                    }
                    catch
                    {
                        break;
                    }
                } while (keyValue.Trim() != string.Empty);
            }
            return dictionary;
        }

        public void PrintActions()
        {
            Console.WriteLine("[Actions]");
            foreach (var link in Client.Api)
            {
                if (link.Key == "self" || link.Key == "curies") continue;
                Console.WriteLine(link.Key);
            }
            Console.WriteLine("[/Actions]");
        }

        public void PrintResource(IResourceObject resourceObject)
        {
            Console.WriteLine("===");
            foreach (var stateValuePair in resourceObject.State)
            {
                Console.WriteLine("-> " + stateValuePair.Value.Name + " => " + stateValuePair.Value.Value);
            }

            if (resourceObject.Embedded.Count > 0)
            {
                Console.WriteLine("Embedded: ");
                foreach (var emb in resourceObject.Embedded)
                {
                    Console.WriteLine("-> " + emb.Key);

                    foreach (var value in emb.Value)
                    {
                        foreach (var stateValuePair in value.State)
                        {
                            Console.WriteLine(stateValuePair.Value.Name + " => " + stateValuePair.Value.Value);
                        }
                    }
                }
            }
            Console.WriteLine("===");
        }
    }
}
