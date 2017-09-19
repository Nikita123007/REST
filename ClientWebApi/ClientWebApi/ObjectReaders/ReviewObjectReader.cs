using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientWebApi.Resources;

namespace ClientWebApi.ObjectReaders
{
    public class ReviewObjectReader : IObjectReader
    {
        public UserObjectReader UserObjectReader { get; set; }
        public PostObjectReader PostObjectReader { get; set; }
        
        public object ReadFromConsole(bool needEmbedded)
        {
            var review = new Review();

            while (true)
            {
                try
                {
                    Console.WriteLine("Enter id: ");
                    review.Id = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Invalid id");
                }
            }

            Console.WriteLine("Enter content: ");
            review.Content = Console.ReadLine();

            if (needEmbedded)
            {
                Console.WriteLine("Reading user? (y/n)");
                var response = Console.ReadLine();
                if (response == "y")
                {
                    review.User = (User)UserObjectReader.ReadFromConsole(false);
                }
                else
                {
                    review.User = null;
                }

                Console.WriteLine("Reading post? (y/n)");
                response = Console.ReadLine();
                if (response == "y")
                {
                    review.Post = (Post)PostObjectReader.ReadFromConsole(false);
                }
                else
                {
                    review.Post = null;
                }
            }

            return review;
        }
    }
}
