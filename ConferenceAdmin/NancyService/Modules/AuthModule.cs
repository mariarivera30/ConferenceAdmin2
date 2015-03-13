using Nancy;

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
        public AuthModule( ITokenizer tokenizer)
            : base("/auth")
        {
          
            // Method for registering a user.
            Post["/register"] = parameters =>
            {
               

                return HttpStatusCode.Created;
            };

            Post["/"] = x =>
            {
                

                return new
                {
                    
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