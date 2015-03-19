using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class complementarykey
    {
        public long complementarykeyID { get; set; }
        public long sponsorID { get; set; }
        public Nullable<bool> isUsed { get; set; }
        public Nullable<bool> deleted { get; set; }
        public virtual paymentcomplementary paymentcomplementary { get; set; }
        public virtual sponsor sponsor { get; set; }
    }
}
