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
    public class LoginModule : NancyModule
    {

        public LoginModule(conferenceadminContext context, ITokenizer tokenizer)
            : base("/auth")
        {

            Post["/login"] = parameters =>
            {
                var paramuser = this.Bind<membership>();
                NancyService.Modules.LoginAuthenticateManager.UserAuth user = LoginAuthenticateManager.login(paramuser);

                if (user == null)
                {
                    return HttpStatusCode.Unauthorized;
                }

                var userIdentity = user.GetIdentity();

                var token = tokenizer.Tokenize(userIdentity, Context);

                return new
                {
                    memberID = user.memberID,
                    userID = user.userID,
                    userClaims = userIdentity.Claims,
                    Token = token,
                };


            };

        }
    }
}