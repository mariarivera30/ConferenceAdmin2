using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class workshop
    {
        public long workshopID { get; set; }
        public long submissionID { get; set; }
        public string duration { get; set; }
        public string delivery { get; set; }
        public string plan { get; set; }
        public string necessary_equipment { get; set; }
        public Nullable<bool> deleted { get; set; }
        public virtual submission submission { get; set; }
    }
}
