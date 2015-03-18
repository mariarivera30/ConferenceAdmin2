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
            public int priviledge { get; set; }
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

                List<string> claims = new List<string>();

                switch (priviledge)
                {
                    case 0:
                        break;
                    case 1:
                        claims.Add("admin");
                        break;
                    case 2:
                        claims.Add("adminFinance");
                        break;
                    case 3:
                        claims.Add("adminCommittee");
                        break;
                    case 4:
                        claims.Add("evaluator");
                        break;
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
                priviledge = 0;
                userType = 0;
                // TODO: Complete member initialization
            }

        };

        public static UserAuth login(membership param)
        {
            using (conferenceadminContext contx = new conferenceadminContext())
            {
                UserAuth complete;
                UserAuth admin = (from g in contx.memberships
                                  join admi in contx.administrators.DefaultIfEmpty() on g.membershipID equals admi.membershipID
                                  where g.email == param.email && g.password == param.password
                                  select new UserAuth { memberID = g.membershipID, password = g.password, email = g.email, priviledge = admi.privilegesID }).FirstOrDefault();
                UserAuth user = (from g in contx.memberships
                                 join type in contx.users on g.membershipID equals type.membershipID
                                 where g.email == param.email && g.password == param.password
                                 select new UserAuth { memberID = g.membershipID, password = g.password, email = g.email, userType = type.userTypeID }).FirstOrDefault();

                if (admin == null && user == null)
                {
                    return null;
                }
                else if (admin != null && user == null)
                {
                    complete = admin;
                }
                else if (admin == null && user != null)
                {
                    complete = user;
                }
                else
                {
                    complete = admin;
                    complete.userType = user.userType;
                }
                return complete;

            }
        }
    }
}