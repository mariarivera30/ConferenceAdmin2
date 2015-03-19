using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class templatesubmission
    {
        public int templatesubmissionID { get; set; }
        public long templateID { get; set; }
        public long submissionID { get; set; }
        public Nullable<bool> deleted { get; set; }
        public virtual submission submission { get; set; }
        public virtual template template { get; set; }
    }
}
