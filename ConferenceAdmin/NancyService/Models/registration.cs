using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class registration
    {
        public long registrationID { get; set; }
        public long userID { get; set; }
        public long paymentID { get; set; }
        public Nullable<System.DateTime> date1 { get; set; }
        public Nullable<System.DateTime> date2 { get; set; }
        public string date3 { get; set; }
        public virtual payment payment { get; set; }
        public virtual user user { get; set; }
    }
}
