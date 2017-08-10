﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Giveonline.Models;

namespace Giveonline.ViewModels
{
    public class NewsletterVM
    {
        public virtual List<Subscriber> Subscribers { get; set; }
        public virtual Newsletter Newsletter { get; set; }
    }
}