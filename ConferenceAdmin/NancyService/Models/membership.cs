using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class membership
    {
        public membership()
        {
            this.administrators = new List<administrator>();
            this.users = new List<user>();
        }

        public long membershipID { get; set; }
        public int membershipTypeID { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Nullable<System.DateTime> creationDate { get; set; }
        public Nullable<System.DateTime> deletionDate { get; set; }
        public Nullable<bool> emailConfirmation { get; set; }
        public virtual ICollection<administrator> administrators { get; set; }
        public virtual ICollection<user> users { get; set; }
        public virtual membershiptype membershiptype { get; set; }
    }
}
