using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class complementarykey
    {
        public long complementarykeyID { get; set; }
        public long sponsorID { get; set; }
        public Nullable<bool> isUsed { get; set; }
        public System.DateTime creationDate { get; set; }
        public Nullable<System.DateTime> deleitionDate { get; set; }
        public virtual paymentcomplementary paymentcomplementary { get; set; }
        public virtual sponsor sponsor { get; set; }
    }
}
