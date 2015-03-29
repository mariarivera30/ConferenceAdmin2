using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class usersubmission
    {
        public long userSubmissionID { get; set; }
        public long userID { get; set; }
        public Nullable<bool> deleted { get; set; }
        public long initialSubmissionID { get; set; }
        public Nullable<long> finalSubmissionID { get; set; }
        public Nullable<bool> allowFinalVersion { get; set; }
        public virtual submission submission { get; set; }
        public virtual submission submission1 { get; set; }
        public virtual user user { get; set; }
    }
}