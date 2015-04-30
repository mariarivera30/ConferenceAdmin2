using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class sponsortype
    {
        public sponsortype()
        {
           
            this.sponsors2 = new List<sponsor2>();
        }

        public int sponsortypeID { get; set; }
        public string name { get; set; }
        public double amount { get; set; }
        public string benefit1 { get; set; }
        public string benefit2 { get; set; }
        public string benefit3 { get; set; }
        public string benefit4 { get; set; }
        public string benefit5 { get; set; }
        public string benefit6 { get; set; }
        public string benefit7 { get; set; }
        public string benefit8 { get; set; }
        public string benefit9 { get; set; }
        public string benefit10 { get; set; }
        public Nullable<bool> deleted { get; set; }
        public virtual ICollection<sponsor2> sponsors2 { get; set; }
    }
}
