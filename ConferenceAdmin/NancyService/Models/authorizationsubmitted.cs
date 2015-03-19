using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class authorizationsubmitted
    {
        public int authorizationSubmittedID { get; set; }
        public long minorID { get; set; }
        public string documentFile { get; set; }
        public string documentName { get; set; }
        public Nullable<bool> deleted { get; set; }
        public virtual minor minor { get; set; }
    }
}
