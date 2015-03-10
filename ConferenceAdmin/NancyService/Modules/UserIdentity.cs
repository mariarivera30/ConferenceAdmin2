using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.Security;

namespace NancyService.Modules
{
    public class UserIdentity : IUserIdentity
    {
        public IEnumerable<string> Claims { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }

    }
}
