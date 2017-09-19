using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudWebApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        public ICollection<Post> Posts { get; set; }
    }
}