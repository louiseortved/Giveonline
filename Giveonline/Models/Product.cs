using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Giveonline.Models
{
    public class Product
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }
        public int Amount { get; set; }
        public bool OnSale { get; set; }
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }


       

        public virtual ICollection<SizeProduct> SizeProducts { get; set; }
    }


}