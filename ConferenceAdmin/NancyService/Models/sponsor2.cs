using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class sponsor2
    {
        public long sponsorID { get; set; }
        public string logo { get; set; }
        public Nullable<bool> deleted { get; set; }
        public Nullable<bool> active { get; set; }
        public Nullable<int> sponsorType { get; set; }
        public Nullable<long> paymentID { get; set; }
        public string emailInfo { get; set; }
        public bool byAdmin { get; set; }
        public double totalAmount { get; set; }
        public Nullable<long> userID { get; set; }
        public virtual sponsortype sponsortype1 { get; set; }
        public virtual ICollection<complementarykey> complementarykeys { get; set; }
        public virtual payment payment { get; set; }
        public virtual user user { get; set; }

    }
}
