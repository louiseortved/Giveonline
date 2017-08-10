using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Giveonline.Models
{
    public class Size
    {

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<SizeProduct> SizeProducts { get; set; }
    }
}