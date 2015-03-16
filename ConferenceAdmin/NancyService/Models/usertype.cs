using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class usertype
    {
        public usertype()
        {
            this.users = new List<user>();
        }

        public int userTypeID { get; set; }
        public string userTypeName { get; set; }
        public string description { get; set; }
        public Nullable<double> registrationCost { get; set; }
        public Nullable<double> registrationLateFee { get; set; }
        public virtual ICollection<user> users { get; set; }
    }
}
