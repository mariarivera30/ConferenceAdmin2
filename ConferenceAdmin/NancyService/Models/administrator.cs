using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class administrator
    {
        public int administratorsID { get; set; }
        public int privilegesID { get; set; }
        public long membershipID { get; set; }
        public Nullable<bool> enabled { get; set; }
        public virtual membership membership { get; set; }
        public virtual privilege privilege { get; set; }
    }
}
