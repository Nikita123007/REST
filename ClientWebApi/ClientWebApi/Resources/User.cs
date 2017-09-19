using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWebApi.Resources
{
    public class User : IResource
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Review> Reviews { get; set; }
    }
}
