using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class administrator
    {
        public int administratorsID { get; set; }
        public int priviledgesID { get; set; }
        public long membershipID { get; set; }
        public virtual membership membership { get; set; }
        public virtual priviledge priviledge { get; set; }
    }
}
