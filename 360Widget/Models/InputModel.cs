using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _360Widgets.Models
{
    public class InputModelText
    { 
        public string Reference { get; set; }
        public List<List<string>> Thermometer { get; set; } = new List<List<string>>();
        public List<List<string>> Humidity { get; set; } = new List<List<string>>();
        public List<List<string>> Monoxide { get; set; } = new List<List<string>>();
    }
}
