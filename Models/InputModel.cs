﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _360Widgets.Models
{
    public class InputModel
    {
        public string Reference { get; set; }
        public List<Dictionary<string, string>> Thermometer { get; set; }
        public List<Dictionary<string, string>> Humidity { get; set; }
        public List<Dictionary<string, string>> Monoxide { get; set; }
    }
}
