using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class panel
    {
        public long panelsID { get; set; }
        public long submissionID { get; set; }
        public string panelistNames { get; set; }
        public string plan { get; set; }
        public string guideQuestion { get; set; }
        public string necessaryEquipment { get; set; }
        public string formatDescription { get; set; }
        public Nullable<bool> deleted { get; set; }
        public virtual submission submission { get; set; }
    }
}
