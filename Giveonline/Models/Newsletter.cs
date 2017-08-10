using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Giveonline.Models
{
    public class Newsletter
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string HtmlContent { get; set; }


    }
}