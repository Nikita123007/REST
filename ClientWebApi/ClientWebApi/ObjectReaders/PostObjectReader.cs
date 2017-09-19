using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientWebApi.Resources;

namespace ClientWebApi.ObjectReaders
{
    public class PostObjectReader : IObjectReader
    {
        public ReviewObjectReader ReviewObjectReader { get; set; }
        public CategoryObjectReader CategoryObjectReader { get; set; }
        
        public object ReadFromConsole(bool needEmbedded)
        {
            var post = new Post();

            while (true)
            {
                try
                {
                    Console.WriteLine("Enter id: ");
                    post.Id = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Invalid id");
                }
            }

            Console.WriteLine("Title: ");
            post.Title = Console.ReadLine();

            Console.WriteLine("Enter content: ");
            post.Content = Console.ReadLine();

            if (needEmbedded)
            {
                Console.WriteLine("Amount of reviews: ");
                int amount;
                while (true)
                {
                    try
                    {
                        amount = Convert.ToInt32(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Invalid amount");
                    }
                }

                var list = new List<Review>(amount);

                for (int i = 0; i < amount; i++)
                {
                    list.Add((Review)ReviewObjectReader.ReadFromConsole(false));
                }
                post.Reviews = list;

                Console.WriteLine("Amount of categories: ");
                amount = Convert.ToInt32(Console.ReadLine());
                var catList = new List<Category>(amount);

                for (int i = 0; i < amount; i++)
                {
                    catList.Add((Category)CategoryObjectReader.ReadFromConsole(false));
                }
                post.Categories = catList;
            }

            return post;
        }
    }
}
