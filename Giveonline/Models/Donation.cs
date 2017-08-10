using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Giveonline.Models
{
    public class Donation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public string ImageUrl { get; set; }


    }
}