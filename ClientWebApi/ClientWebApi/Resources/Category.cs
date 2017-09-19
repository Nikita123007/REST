using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWebApi.Resources
{
    public class Category : IResource
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<Post> Posts { get; set; }
    }
}
