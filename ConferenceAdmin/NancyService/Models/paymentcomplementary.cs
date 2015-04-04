using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class paymentcomplementary
    {
        public long paymentcomplementaryID { get; set; }
        public Nullable<long> paymentID { get; set; }
        public long complementaryKeyID { get; set; }
        public Nullable<bool> deleted { get; set; }
        public virtual complementarykey complementarykey { get; set; }
        public virtual payment payment { get; set; }
    }
}
