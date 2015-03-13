using Nancy;
using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.ModelBinding;
using Nancy.Authentication.Token;
using Nancy.Security;

namespace NancyService.Modules
{
    public class AuthModule : NancyModule
    {
        public AuthModule(conferenceadminContext context, ITokenizer tokenizer)
            : base("/auth")
        {

            // Method for registering a user.
            Post["/register"] = parameters =>
            {

               //var user = this.Bind<user>();
               //context.users.Add(user);
               var member = this.Bind<membership>();
             
               member.membershipTypeID = 1;
               member.creationDate = new DateTime();
               context.memberships.Add(member);
               context.SaveChanges();
               return HttpStatusCode.Created;
            };

            Post["/addsponsor"] = parameters =>
            {
                var sponsor = this.Bind<sponsor>();
                context.sponsors.Add(sponsor);
                context.SaveChanges();
                return HttpStatusCode.Created;
            };

           

           // Post["/"] = x =>
           // {
               // var paramuser = this.Bind<membership>();

               // var member = context.memberships
                 //   .Include("Claims.users")
                 //   .Where(i => i.email == paramuser.email && i.password == paramuser.password).FirstOrDefault();

             //   if (member == null)
             //   {
             //       return HttpStatusCode.Unauthorized;
             //   }

               // var userIdentity = member.GetIdentity();

               // var token = tokenizer.Tokenize(userIdentity, Context);

              //  return new
             //   {
                    //Token = token,
              //  };
          //  };

            // This route validates authentication only.
            Get["/validation"] = _ =>
            {
                this.RequiresAuthentication();
                this.RequiresClaims(new[] { "1" });
                return "Yay! You are authenticated!";
            };

            // Route that validated for the admin claim in the role.
            //Get["/admin"] = _ =>
            //{
            //    this.RequiresClaims(new[] { "1" });
            //    return "Yay! You are authorized!";
            //};
        }
    }
}