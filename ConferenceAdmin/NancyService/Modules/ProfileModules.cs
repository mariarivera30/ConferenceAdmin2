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

            Put["/complementaryPayment"] = parameters =>
            {
                var user = this.Bind<UserInfo>();
                if (profileInfo.complementaryPayment(user, user.key))
                    return HttpStatusCode.OK;

                else
                    return HttpStatusCode.Conflict;
            };

            Get["/checkComplementaryKey/{complementaryKey}"] = parameters =>
            {
                var key = parameters.complementaryKey;

                return (profileInfo.checkComplementaryKey(key)) ;
            };


            //------------------------EVALUATOR - SUBMISSIONS-------------------------------------------
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
            Get["/getSubmission/{submissionID:long}/{evaluatorID:long}"] = parameters =>
            {
                long submissionID = parameters.submissionID;
                long evaluatorID = parameters.evaluatorID;
                AssignedSubmission sub = submission.getSubmission(submissionID, evaluatorID);
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
                usersubmission usersub = this.Bind<usersubmission>();

                if (submission.addEvaluation(evaluation, usersub)) return HttpStatusCode.OK;
                else return HttpStatusCode.Conflict;
            };

            //Edit evaluation for a submission
            Put["/editEvaluation"] = parameters =>
            {
                var evaluation = this.Bind<evaluationsubmitted>();
                usersubmission usersub = this.Bind<usersubmission>();

                if (submission.editEvaluation(evaluation, usersub)) return HttpStatusCode.OK;
                else return HttpStatusCode.Conflict;
            };
            //------------------------------------USER SUBMISSIONS---------------------------------
            Get["/getUserSubmissionList/{id}"] = parameters =>
            {
                long userID = parameters.id; //ID of the evaluator that is currently signed in
                List<Submission> userSubmissions = submission.getUserSubmissions(userID);
                if (userSubmissions == null)
                {
                    userSubmissions = new List<Submission>();
                }

                return Response.AsJson(userSubmissions);
            };
            //get single user submission
            Get["/getUserSubmission/{id}"] = parameters =>
            {
                long submissionID = parameters.id;
                CurrAndPrevSub sub = submission.getUserSubmission(submissionID);
                if (sub == null)
                {
                    sub = new CurrAndPrevSub();
                }
                return Response.AsJson(sub);
            };
            //get submission types
            Get["/getSubmissionTypes"] = parameters =>
            {
                List<SubmissionType> subTypes = submission.getSubmissionTypes();
                if (subTypes == null)
                {
                    subTypes = new List<SubmissionType>();
                }
                return Response.AsJson(subTypes);
            };
            //Delete a submission
            Delete["/deleteSubmission/{id}"] = parameters =>
            {
                long submissionID = parameters.id;
                if (submission.deleteSubmission(submissionID))
                    return HttpStatusCode.OK;

                else
                    return HttpStatusCode.Conflict;
            };
            //Add a submission
            Post["/postSubmission"] = parameters =>
            {
                panel pannelToAdd = null;
                workshop workshopToAdd = null;
                submission submissionToAdd = this.Bind<submission>();
                //documentssubmitted submissionDocuments = this.Bind<documentssubmitted>();
                usersubmission usersubTA = this.Bind<usersubmission>();

                int submissionTypeID = submissionToAdd.submissionTypeID;
                /*if (submissionDocuments.document == null && submissionDocuments.documentName == null)
                {
                    submissionDocuments = null;
                }*/
                if (submissionTypeID == 3)
                {
                    pannelToAdd = this.Bind<panel>();
                }
                else if (submissionTypeID == 5)
                {
                    workshopToAdd = this.Bind<workshop>();
                }
                Submission newSubmission =
                    submission.addSubmission(usersubTA, submissionToAdd, pannelToAdd, workshopToAdd);
                return Response.AsJson(newSubmission);

            };
            //edit submission
            Put["/editSubmission"] = parameters =>
            {
                panel pannelToEdit = null;
                workshop workshopToEdit = null;
                submission submissionToEdit = this.Bind<submission>();

                int submissionTypeID = submissionToEdit.submissionTypeID;
                if (submissionTypeID == 3)
                {
                    pannelToEdit = this.Bind<panel>();
                }
                else if (submissionTypeID == 5)
                {
                    workshopToEdit = this.Bind<workshop>();
                }
                Submission editedSubmission =
                    submission.editSubmission(submissionToEdit, pannelToEdit, workshopToEdit);
                return Response.AsJson(editedSubmission);
            };
            //post final version of evaluation
            Post["/postFinalSubmission"] = parameters =>
                {
                    panel pannelToAdd = null;
                    workshop workshopToAdd = null;
                    submission submissionToAdd = this.Bind<submission>();
                    documentssubmitted submissionDocuments = this.Bind<documentssubmitted>();
                    usersubmission usersubTA = this.Bind<usersubmission>();

                    int submissionTypeID = submissionToAdd.submissionTypeID;
                    if (submissionDocuments.document == null && submissionDocuments.documentName == null)
                    {
                        submissionDocuments = null;
                    }
                    if (submissionTypeID == 3)
                    {
                        pannelToAdd = this.Bind<panel>();
                    }
                    else if (submissionTypeID == 5)
                    {
                        workshopToAdd = this.Bind<workshop>();
                    }
                    Submission newSubmission =
                        submission.addFinalSubmission(usersubTA, submissionToAdd, submissionDocuments, pannelToAdd, workshopToAdd);
                    return Response.AsJson(newSubmission);
                };

            Get["/getSubmissionDeadline"] = parameters =>
                {
                    return Response.AsJson(submission.getSubmissionDeadline());
                };

            //------------------------AUTHORIZATION----------------------------------
            Put["/uploadDocument"] = parameters =>
            {
                var doc = this.Bind<Authorization>();
                var minor = this.Bind<MinorUser>();
                return Response.AsJson(profileAuthorization.uploadDocument(doc, minor));
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

            Post["/selectCompanion"] = parameters =>
            {
                var user = this.Bind<UserInfo>();
                var companion = this.Bind<companion>();

                var status = profileAuthorization.selectCompanion(user, companion);
                if (status != null)
                    return status;
                else
                    return HttpStatusCode.Conflict;
            };

            Get["/getCompanionKey/{userID:long}"] = parameters =>
            {
                var user = this.Bind<UserInfo>();
                return Response.AsJson(profileAuthorization.getCompanionKey(user));
            };

        }

    }
}

