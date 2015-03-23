using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class documentssubmitted
    {
        public long documentssubmittedID { get; set; }
        public long submissionID { get; set; }
        public string documentName { get; set; }
        public string document { get; set; }
        public Nullable<bool> deleted { get; set; }
        public virtual submission submission { get; set; }
    }
}
