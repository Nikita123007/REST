using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWebApi.Resources
{
    public class Post : IResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public List<Category> Categories { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
