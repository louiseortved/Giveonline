
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Giveonline.Models;

namespace Giveonline.ViewModels
{
    public class ContentVM
    {
        public virtual List<Slider> Sliders { get; set; }

        public virtual List<Event> Events{ get; set; }
        public virtual List<About> Abouts { get; set; }

        public virtual About About { get; set; }
    }
}