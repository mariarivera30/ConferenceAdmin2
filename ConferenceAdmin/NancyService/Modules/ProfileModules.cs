using Nancy;
using Nancy.Responses;
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
    public class ProfileModules : NancyModule
    {
        public ProfileModules(ITokenizer tokenizer)
            : base("/profile")
        {
            ProfileInfoManager profileInfo = new ProfileInfoManager();

            Get["/getProfileInfo/{userID:long}"] = parameters =>
            {
                var user = this.Bind<UserInfo>();
                return Response.AsJson(profileInfo.getProfileInfo(user));
            };

            Put["/updateProfileInfo"] = parameters =>
            {
                var user = this.Bind<UserInfo>();
                if (profileInfo.updateProfileInfo(user))
                    return HttpStatusCode.OK;

                else
                    return HttpStatusCode.Conflict;
            };

            Put["/apply"] = parameters =>
            {
                var user = this.Bind<UserInfo>();
                if (profileInfo.apply(user))
                    return HttpStatusCode.OK;

                else
                    return HttpStatusCode.Conflict;
            };

        }

    }
}

