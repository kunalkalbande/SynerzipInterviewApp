﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynerzipInterviewApp.Models
{
    public class AuthenticationModel
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string token { get; set; }
    }
}
