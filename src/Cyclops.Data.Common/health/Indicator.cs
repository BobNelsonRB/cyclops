using System;
using System.Collections.Generic;
using System.Text;

namespace Cyclops.Data.Health
{
    public class Indicator
    {
        public string Key { get; set; }
        public string Display { get; set; }
        public string Category { get; set; }
        public bool IsOkay { get; set; }
        public string Message { get; set; }
    }
}
