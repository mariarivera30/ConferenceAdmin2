using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class documentssubmitted
    {
        public long documentssubmittedID { get; set; }
        public long submissionID { get; set; }
        public string document { get; set; }
        public System.DateTime creationDate { get; set; }
        public Nullable<System.DateTime> deleitionDate { get; set; }
        public string documentssubmittedcol { get; set; }
        public virtual submission submission { get; set; }
    }
}
