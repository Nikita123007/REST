using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientWebApi.Resources;

namespace ClientWebApi.ObjectReaders
{
    public class CategoryObjectReader : IObjectReader
    {
        public PostObjectReader PostObjectReader { get; set; }
        
        public object ReadFromConsole(bool needEmbedded)
        {
            var category = new Category();

            while (true)
            {
                try
                {
                    Console.WriteLine("Enter id: ");
                    category.Id = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Invalid id");
                }
            }

            Console.WriteLine("Title: ");
            category.Title = Console.ReadLine();

            if (needEmbedded)
            {
                Console.WriteLine("Amount of posts: ");

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

                var list = new List<Post>(amount);

                for (int i = 0; i < amount; i++)
                {
                    list.Add((Post)PostObjectReader.ReadFromConsole(false));
                }
                category.Posts = list;
            }

            return category;
        }
    }
}
