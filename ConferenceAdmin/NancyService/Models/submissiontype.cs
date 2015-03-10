using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class submissiontype
    {
        public submissiontype()
        {
            this.submissions = new List<submission>();
        }

        public int submissiontypeID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public virtual ICollection<submission> submissions { get; set; }
    }
}
