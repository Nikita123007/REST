using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudWebApi.Models
{
    public class User
    {
        public int Id { get; set; }
  
        [Required]
        public string Name { get; set; }
  
        public ICollection<Review> Reviews { get; set; }
    }
}