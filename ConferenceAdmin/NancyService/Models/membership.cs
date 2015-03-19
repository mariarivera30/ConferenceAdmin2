using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class membership
    {
        public membership()
        {
            this.users = new List<user>();
        }

        public long membershipID { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Nullable<bool> emailConfirmation { get; set; }
        public string confirmationKey { get; set; }
        public Nullable<bool> deleted { get; set; }
        public virtual ICollection<user> users { get; set; }
    }
}
