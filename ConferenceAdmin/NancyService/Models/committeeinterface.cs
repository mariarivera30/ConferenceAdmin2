using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class committeeinterface
    {
        public int committeID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string affiliation { get; set; }
        public string description { get; set; }
    }
}
