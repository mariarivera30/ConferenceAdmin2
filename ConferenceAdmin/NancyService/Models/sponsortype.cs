using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class sponsortype
    {
        public sponsortype()
        {
            this.sponsors = new List<sponsor>();
        }

        public int sponsortypeID { get; set; }
        public string name { get; set; }
        public string amount { get; set; }
        public string benefit1 { get; set; }
        public string benefit2 { get; set; }
        public string benefit3 { get; set; }
        public string benefit4 { get; set; }
        public string benefit5 { get; set; }
        public virtual ICollection<sponsor> sponsors { get; set; }
    }
}
