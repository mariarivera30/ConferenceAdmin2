using Nancy.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NancyService.Models
{
    public class UserIdentity: IUserIdentity
    {
        public IEnumerable<string> Claims { get; set; }

        public string UserName { get; set; }
     
    }

    public class User
    {
        private IUserIdentity _identity;

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Claim> Claims { get; set; } 
      
        public IUserIdentity GetIdentity()
        {
            if (_identity != null)
                return _identity;

            UserIdentity identity = new UserIdentity();
            identity.UserName = this.Email;
            
            List<string> claims = new List<string>();
            foreach(var claim in this.Claims)
            {   

                claims.Add(claim.Role.Value);
            }

            identity.Claims = claims;
            _identity = identity;

            return identity;
        }
    }

    public class Role
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class Claim
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ClaimId { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}