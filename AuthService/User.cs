﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService
{
    public class User
    {
        public int Userid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
