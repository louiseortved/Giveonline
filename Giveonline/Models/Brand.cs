﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Giveonline.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }


        public virtual ICollection<Product> Products { get; set; }
    }
}