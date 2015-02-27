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
    public class AuthModule:NancyModule
    {
        public AuthModule(ISTETSContext context, ITokenizer tokenizer)
            : base("/auth")
        {
          
            // Method for registering a user.
            Post["/register"] = parameters =>
            {
                var user = this.Bind<User>();
                
                context.Users.Add(user);
                context.Claims.Add(new Claim() { UserId = user.UserId, RoleId = 1 });

                context.SaveChanges();

                return HttpStatusCode.Created;
            };

            Post["/"] = x =>
            {
                var paramuser = this.Bind<User>();

                var user = context.Users
                    .Include("Claims.Role") 
                    .Where(i => i.Email == paramuser.Email && i.Password == paramuser.Password).FirstOrDefault();

                if (user == null)
                {
                    return HttpStatusCode.Unauthorized;
                }

                var userIdentity = user.GetIdentity();

                var token = tokenizer.Tokenize(userIdentity, Context);

                return new
                {
                    Token = token,
                };
            };

            // This route validates authentication only.
            Get["/validation"] = _ =>
            {
                this.RequiresAuthentication();
                this.RequiresClaims(new[] { "admin" });
                return "Yay! You are authenticated!";
            };

            // Route that validated for the admin claim in the role.
            Get["/admin"] = _ =>
            {
                this.RequiresClaims(new[] { "admin" });
                return "Yay! You are authorized!";
            };
        }
    }
}