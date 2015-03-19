using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class evaluator
    {
        public evaluator()
        {
            this.evaluatiorsubmissions = new List<evaluatiorsubmission>();
        }

        public long evaluatorsID { get; set; }
        public long userID { get; set; }
        public Nullable<bool> deleted { get; set; }
        public virtual ICollection<evaluatiorsubmission> evaluatiorsubmissions { get; set; }
        public virtual user user { get; set; }
    }
}
