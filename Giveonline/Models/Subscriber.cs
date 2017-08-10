﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Giveonline.Models
{
    public class Subscriber
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool IsSubscribed { get; set; } = true;
    }
}