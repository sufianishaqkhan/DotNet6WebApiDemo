﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class AuthenticationModel
    {
        public string? access_token { get; set; }
        public string? token_type { get; set; }
        public string? expires_in { get; set; }
    }
}
