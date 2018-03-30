using System;
using System.Collections.Generic;

namespace Cyclops.Model
{
    public class Note
    {
        public string Id { get; set; }
        public string Display { get; set; }
        public string Body { get; set; }
        public string Disposition { get; set; }
        public List<string> Tags { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

    }
}
