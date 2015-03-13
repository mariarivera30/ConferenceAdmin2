using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class priviledge
    {
        public priviledge()
        {
            this.administrators = new List<administrator>();
        }

        public int priviledgesID { get; set; }
        public string priviledgestType { get; set; }
        public virtual ICollection<administrator> administrators { get; set; }
    }
}
