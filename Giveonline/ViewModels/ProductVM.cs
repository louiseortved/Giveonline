using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Giveonline.Models;
using Microsoft.Owin.Security;

namespace Giveonline.ViewModels
{
    public class ProductVM
    {
        public virtual List<Category> Categories { get; set; }
        public virtual List<Size> Sizes { get; set; }
        public virtual List<Brand> Brands { get; set; }
        public virtual List<ProductImage> Images { get; set; }

        public virtual List<SizeProduct> SizeProduct { get; set; }

        public virtual Product Product { get; set; }

    }
}