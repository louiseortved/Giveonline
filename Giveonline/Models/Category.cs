using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Giveonline.Models
{
    public class Category
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}