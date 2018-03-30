using System;
using System.Collections.Generic;
using System.Text;

namespace Cyclops.Data.Health
{
    public class HealthCheck
    {
        public string Microservice { get; set; }
        public string Environment { get; set; }
        public string Summary { get; set; }
        public bool IsOkay { get; set; }
        public DateTime At { get; set; }
        public List<Indicator> Indicators { get; set; }

    }
}
