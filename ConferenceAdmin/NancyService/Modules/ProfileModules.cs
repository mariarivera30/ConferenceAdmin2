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
            SubmissionManager submission = new SubmissionManager();
            ProfileAuthorizationManager profileAuthorization = new ProfileAuthorizationManager();


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

            Put["/makePayment"] = parameters =>
            {
                var user = this.Bind<UserInfo>();
                if (profileInfo.makePayment(user))
                    return HttpStatusCode.OK;

                else
                    return HttpStatusCode.Conflict;
            };

            //------------------------SUBMISSIONS-------------------------------------------
            //Gets the list of submissions assigned to the evaluator currently logged in to the system
            Get["/getAssignedSubmissions/{id}"] = parameters =>
            {
                long evaluatorUserID = parameters.id; //ID of the evaluator that is currently signed in
                List<Submission> assignedSubmissions = submission.getAssignedSubmissions(evaluatorUserID);
                if (assignedSubmissions == null)
                {
                    assignedSubmissions = new List<Submission>();
                }
                return Response.AsJson(assignedSubmissions);
            };

            //Get the info of a submission
            Get["/getSubmission/{id}"] = parameters =>
            {
                long submissionID = parameters.id;
                AssignedSubmission sub = submission.getSubmission(submissionID);
                if (sub == null)
                {
                    sub = new AssignedSubmission();
                }
                return Response.AsJson(sub);
            };

            //Post new evaluation for a submission
            Post["/addEvaluation"] = parameters =>
            {
                var evaluation = this.Bind<evaluationsubmitted>();

                if (submission.addEvaluation(evaluation)) return HttpStatusCode.OK;
                else return HttpStatusCode.Conflict;
            };

            //Edit evaluation for a submission
            Put["/editEvaluation"] = parameters =>
            {
                var evaluation = this.Bind<evaluationsubmitted>();

                if (submission.editEvaluation(evaluation)) return HttpStatusCode.OK;
                else return HttpStatusCode.Conflict;
            };


            //------------------------AUTHORIZATION----------------------------------
            Put["/uploadDocument"] = parameters =>
            {
                var doc = this.Bind<Authorization>();
                var minor = this.Bind<MinorUser>();
                if (profileAuthorization.uploadDocument(doc, minor))
                    return HttpStatusCode.OK;

                else
                    return HttpStatusCode.Conflict;
            };

            Get["/getTemplates"] = parameters =>
            {
                return Response.AsJson(profileAuthorization.getTemplates());
            };

            Get["/getDocuments/{userID:long}"] = parameters =>
            {
                var user = this.Bind<UserInfo>();
                return Response.AsJson(profileAuthorization.getDocuments(user));
            };

            Put["/deleteDocument"] = parameters =>
            {
                var doc = this.Bind<Authorization>();

                if (profileAuthorization.deleteDocument(doc)) 
                    return HttpStatusCode.OK;
                else 
                    return HttpStatusCode.Conflict;
            };
        }

    }
}

