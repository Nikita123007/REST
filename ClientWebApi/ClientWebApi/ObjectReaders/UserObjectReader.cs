using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientWebApi.Resources;

namespace ClientWebApi.ObjectReaders
{
    public class UserObjectReader : IObjectReader
    {
        public ReviewObjectReader ReviewObjectReader { get; set; }
        
        public object ReadFromConsole(bool needEmbedded)
        {
            var user = new User();

            while (true)
            {
                try
                {
                    Console.WriteLine("Enter id: ");
                    user.Id = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Invalid id");
                }
            }

            Console.WriteLine("Enter name: ");
            user.Name = Console.ReadLine();

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
                user.Reviews = list;
            }

            return user;
        }
    }
}
