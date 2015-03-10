using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class workshop
    {
        public int workshopID { get; set; }
        public long submissionID { get; set; }
        public string duration { get; set; }
        public string delivery { get; set; }
        public string plan { get; set; }
        public string necessary_equipment { get; set; }
        public virtual submission submission { get; set; }
    }
}
