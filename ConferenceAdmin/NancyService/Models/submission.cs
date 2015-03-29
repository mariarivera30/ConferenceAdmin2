using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class submission
    {
        public submission()
        {
            this.documentssubmitteds = new List<documentssubmitted>();
            this.evaluatiorsubmissions = new List<evaluatiorsubmission>();
            this.panels = new List<panel>();
            this.usersubmissions = new List<usersubmission>();
            this.usersubmissions1 = new List<usersubmission>();
            this.templatesubmissions = new List<templatesubmission>();
            this.workshops = new List<workshop>();
        }

        public long submissionID { get; set; }
        public int topicID { get; set; }
        public int submissionTypeID { get; set; }
        public string submissionAbstract { get; set; }
        public string title { get; set; }
        public string status { get; set; }
        public System.DateTime creationDate { get; set; }
        public Nullable<bool> byAdmin { get; set; }
        public Nullable<bool> deleted { get; set; }
        public virtual ICollection<documentssubmitted> documentssubmitteds { get; set; }
        public virtual ICollection<evaluatiorsubmission> evaluatiorsubmissions { get; set; }
        public virtual ICollection<panel> panels { get; set; }
        public virtual ICollection<usersubmission> usersubmissions { get; set; }
        public virtual ICollection<usersubmission> usersubmissions1 { get; set; }
        public virtual ICollection<templatesubmission> templatesubmissions { get; set; }
        public virtual submissiontype submissiontype { get; set; }
        public virtual ICollection<workshop> workshops { get; set; }
        public virtual topiccategory topiccategory { get; set; }
    }
}
