using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class claim
    {
        public long claimsID { get; set; }
        public Nullable<int> privilegesID { get; set; }
        public Nullable<long> userID { get; set; }
        public Nullable<bool> deleted { get; set; }
        public virtual privilege privilege { get; set; }
        public virtual user user { get; set; }
    }
}
