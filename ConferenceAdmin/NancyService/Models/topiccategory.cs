using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class topiccategory
    {
        public topiccategory()
        {
            this.submissions = new List<submission>();
        }

        public int topiccategoryID { get; set; }
        public string name { get; set; }
        public Nullable<bool> deleted { get; set; }
        public virtual ICollection<submission> submissions { get; set; }
    }
}
