using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class evaluatiorsubmission
    {
        public evaluatiorsubmission()
        {
            this.evaluationsubmitteds = new List<evaluationsubmitted>();
        }

        public long evaluationsubmissionID { get; set; }
        public long evaluatorID { get; set; }
        public long submissionID { get; set; }
        public string statusEvaluation { get; set; }
        public Nullable<bool> deleted { get; set; }
        public virtual ICollection<evaluationsubmitted> evaluationsubmitteds { get; set; }
        public virtual evaluator evaluator { get; set; }
        public virtual submission submission { get; set; }
    }
}
