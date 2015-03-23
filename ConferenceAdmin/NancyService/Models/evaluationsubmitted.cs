using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class evaluationsubmitted
    {
        public long evaluationsubmittedID { get; set; }
        public long evaluatiorSubmissionID { get; set; }
        public string evaluationFile { get; set; }
        public string evaluationName { get; set; }
        public Nullable<int> score { get; set; }
        public string publicFeedback { get; set; }
        public string privateFeedback { get; set; }
        public Nullable<bool> deleted { get; set; }
        public virtual evaluatiorsubmission evaluatiorsubmission { get; set; }
    }
}
