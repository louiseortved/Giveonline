using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Giveonline.Models
{
    public class Story
    {
        public int  Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}