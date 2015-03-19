using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class companionminor
    {
        public int companionminorID { get; set; }
        public long minorID { get; set; }
        public long companionID { get; set; }
        public Nullable<bool> deleted { get; set; }
        public virtual companion companion { get; set; }
        public virtual minor minor { get; set; }
    }
}
