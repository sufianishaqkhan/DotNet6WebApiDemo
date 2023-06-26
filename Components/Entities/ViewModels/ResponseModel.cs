using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class ResponseModel
    {
        public string? result { get; set; }
        public int status_code { get; set; }
        public string? error { get; set; }
        public object? data { get; set; }
    }
}
