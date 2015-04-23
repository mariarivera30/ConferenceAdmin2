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
            SignUpManager signUp = new SignUpManager();
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
                    email = user.email,
                };


            };
            Get["/accountConfirmation/{key}"] = parameters =>
            {
                string key = parameters.key;
                String result = signUp.confirmAccount(key);

                if (result != null)
                    return Response.AsJson(result);
            
                else
                {
                    return HttpStatusCode.Conflict;
                }
               

            };
            Get["/checkEmail/{email}"] = parameters =>
            {
                string email = parameters.email;
                String result = signUp.checkEmail(email);

                if (result != null) 
                     return Response.AsJson(result);

                else { 
                    return HttpStatusCode.Conflict; 
                }

            };

            
            Get["/requestPass/{email}"] = parameters =>
            {
                string email = parameters.email;

                String result = signUp.requestPass(email);

                if (result != null)
                    return Response.AsJson(result);

                else
                {
                    return HttpStatusCode.Conflict;
                }
               

            };

            Post["/changePassword"] = parameters =>
            {

                NancyService.Modules.SignUpManager.UserCreation member = this.Bind<NancyService.Modules.SignUpManager.UserCreation>();

                if (member == null)
                {
                    return HttpStatusCode.Conflict;
                }
                else
                {
                    return Response.AsJson(signUp.changePassword(member));
                
                }

            };
            Post["/createUser"] = parameters =>
            {
                var user = this.Bind<user>();
                var member = this.Bind<membership>();
                var address = this.Bind<address>();
                
                

                if (user == null)
                {
                    return HttpStatusCode.Conflict;
                }
                else
                {
                    if (signUp.createUser(user, member, address))
                        return HttpStatusCode.Created;
                    else
                    {
                        return HttpStatusCode.InternalServerError;
                    }
                }

            };
        }
    }
}