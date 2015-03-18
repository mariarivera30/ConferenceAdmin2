using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class privilege
    {
        public privilege()
        {
            this.administrators = new List<administrator>();
        }

        public int privilegesID { get; set; }
        public string privilegestType { get; set; }
        public virtual ICollection<administrator> administrators { get; set; }
    }
}
