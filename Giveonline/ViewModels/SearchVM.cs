using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Giveonline.Models;

namespace Giveonline.ViewModels
{
    public class SearchVM
    {
        public List<About> Abouts { get; set; } = new List<About>();               //initialserer dem så vi kan counte null = ingen compilerfejl
        public List<Donation> Donations { get; set; } = new List<Donation>();
        public List<Event> Events{ get; set; } = new List<Event>();
        //public List<Story> Stories { get; set; } = new List<Story>();
        public List<Product> Products { get; set; } = new List<Product>();

    }
}