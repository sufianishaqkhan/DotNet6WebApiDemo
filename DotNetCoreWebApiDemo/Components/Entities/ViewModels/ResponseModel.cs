﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class ResponseModel
    {
        //public string? Result { get; set; }
        public int StatusCode { get; set; }
        public string? Error { get; set; }
        public object? Data { get; set; }
    }
}
