using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class membershiptype
    {
        public membershiptype()
        {
            this.memberships = new List<membership>();
        }

        public int membershipTypeID { get; set; }
        public string membershipTypeName { get; set; }
        public virtual ICollection<membership> memberships { get; set; }
    }
}
