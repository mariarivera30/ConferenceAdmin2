using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class template
    {
        public template()
        {
            this.templatesubmissions = new List<templatesubmission>();
        }

        public long templateID { get; set; }
        public string name { get; set; }
        public string document { get; set; }
        public string topic { get; set; }
        public string description { get; set; }
        public Nullable<bool> deleted { get; set; }
        public virtual ICollection<templatesubmission> templatesubmissions { get; set; }
    }
}
