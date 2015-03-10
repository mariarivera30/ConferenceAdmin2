using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class paymenttype
    {
        public paymenttype()
        {
            this.payments = new List<payment>();
        }

        public int paymentTypeID { get; set; }
        public string name { get; set; }
        public virtual ICollection<payment> payments { get; set; }
    }
}
