using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class complementarykey
    {
        public complementarykey()
        {
            this.paymentcomplementaries = new List<paymentcomplementary>();
        }

        public long complementarykeyID { get; set; }
        public long sponsorID2 { get; set; }
        public string key { get; set; }
        public Nullable<bool> isUsed { get; set; }
        public Nullable<bool> deleted { get; set; }
        public virtual ICollection<paymentcomplementary> paymentcomplementaries { get; set; }
        public virtual sponsor2 sponsor2 { get; set; }
    }
}
