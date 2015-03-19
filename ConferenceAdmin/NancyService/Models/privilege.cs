using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class privilege
    {
        public privilege()
        {
            this.claims = new List<claim>();
        }

        public int privilegesID { get; set; }
        public string privilegestType { get; set; }
        public virtual ICollection<claim> claims { get; set; }
    }
}
