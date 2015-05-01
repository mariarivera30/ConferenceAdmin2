using Nancy.Security;
using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace NancyService.Modules
{
    public class LoginAuthenticateManager
    {

        public class UserAuth
        {
            public string email { get; set; }
            public string password { get; set; }
            public string passwordSalt { get; set; }
            public long userID { get; set; }
            public int userType { get; set; }
            public long memberID { get; set; }

            public UserAuth(string email, string password, long memberID)
            {
                this.email = email;
                this.password = password;
                this.memberID = memberID;

            }
            private IUserIdentity _identity;
            
            public IUserIdentity GetIdentity()
            {
                if (_identity != null)
                    return _identity;

                UserIdentity identity = new UserIdentity();
                identity.UserName = this.email;

                List<string> claims;
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    claims = (
                              from c in context.claims 
                              join p in context.privileges on c.privilegesID equals p.privilegesID
                              where this.userID == c.userID && c.deleted == false
                              select p.privilegestType).ToList();
                }             
       
                switch (userType)
                {
                    case 0:
                        break;
                    case 1:
                        claims.Add("minor");
                        break;
                    case 6:
                        claims.Add("companion");
                        break;
                    case 7:
                        claims.Add("sponsor");
                        break;
                    default:
                        {
                            claims.Add("participant");
                            break;
                        }
                }

               

                identity.Claims = claims;
                _identity = identity;

                return identity;
            }

            public UserAuth()
            {
               userType = 0;
                // TODO: Complete member initialization
            }

        };

        public static UserAuth login(membership param)
        {
            
            using (conferenceadminContext contx = new conferenceadminContext())
            {
               
                /*UserAuth user = (from g in contx.memberships
                                 join u in contx.users on g.membershipID equals u.membershipID
                                 where g.email == param.email  && g.password.Equals(param.password) && g.deleted == false && u.deleted == false
                                 select new UserAuth { userID = u.userID, memberID = g.membershipID, password = g.password, email = g.email, userType = u.userTypeID }).FirstOrDefault();
                */
                //copy paste del query de maria, solo quite: && g.password.Equals(param.password)  del where clause y anadi: passwordSalt = g.passwordSalt, al select
                UserAuth user = (from g in contx.memberships
                                 join u in contx.users on g.membershipID equals u.membershipID
                                 where g.email == param.email && g.deleted == false && u.deleted == false
                                 select new UserAuth { userID = u.userID, memberID = g.membershipID, password = g.password, email = g.email, userType = u.userTypeID }).FirstOrDefault();
               

                if (user == null)
                {
                    return null;
                }
                              
                else
                {
                   var crypto = new SimpleCrypto.PBKDF2();
                   if (Security.ValidateSHA1HashData(param.password, user.password))
                  // if( string.Equals(crypto.Compute(param.password, user.passwordSalt), user.password, StringComparison.Ordinal))
                        return user;
                   else{
                       return null;
                   }
                    
                }
     
            }
        }
    }
}