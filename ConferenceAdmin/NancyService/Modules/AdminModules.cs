using Nancy;
using Nancy.Responses;
using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.ModelBinding;

namespace NancyService.Modules
{
    public class AdminModules : NancyModule
    {
        public AdminModules(conferenceadminContext context)
            : base("/admin")
        {
            AdminManager admin = new AdminManager();
            List<sponsor> sponsorList = new List<sponsor>();
            
            Post["/addsponsor"] = parameters =>
            {
                var sponsor = this.Bind<sponsor>();

                if (admin.addSponsor(sponsor))
                {
                    return HttpStatusCode.Created;
                }

                else
                {
                    return HttpStatusCode.Conflict;
                }
            };

            Get["/getSponsor"] = parameters =>
            {
                List<string> list = new List<string>();
                foreach (sponsor c in admin.getSponsorList())
                {
                    list.Add(c.firstName);
                }               
                return Response.AsJson(list);
                
            };
            
                

           
       
        }
    }
}