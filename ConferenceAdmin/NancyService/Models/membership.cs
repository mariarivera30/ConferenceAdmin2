using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace NancyService.Models
{
    public class UserIdentity : IUserIdentity
    {
        public IEnumerable<string> Claims { get; set; }

        public string UserName { get; set; }

    }
    public partial class membership
    {
        public membership()
        {
            this.administrators = new List<administrator>();
            this.users = new List<user>();
        }
        //private IUserIdentity _identity;

        public long membershipID { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Nullable<System.DateTime> creationDate { get; set; }
        public Nullable<System.DateTime> deletionDate { get; set; }
        public Nullable<int> membershipTypeID { get; set; }
        public virtual ICollection<administrator> administrators { get; set; }
        public virtual ICollection<user> users { get; set; }
        public virtual membershiptype membershiptype { get; set; }
        //public ICollection<Claim> Claims { get; set; }

        //public IUserIdentity GetIdentity()
        //{
        //    if (_identity != null)
        //        return _identity;

        //    UserIdentity identity = new UserIdentity();
        //    identity.UserName = this.email;

        //    List<string> claims = new List<string>();

        //    if (this.membershipTypeID == 1)
        //    {
        //        foreach (var admin in this.administrators)
        //        {
        //            claims.Add(admin.priviledgesID.ToString());
        //        }
        //    }

        //    if (this.membershipTypeID == 2)
        //    {
        //        foreach (var user in this.users)
        //        {
        //            claims.Add(user.userTypeID.ToString());
        //        }
        //    }

        //    identity.Claims = claims;
        //    _identity = identity;

        //    return identity;
        //}
    
    }
}



