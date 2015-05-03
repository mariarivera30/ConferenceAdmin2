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

                return (profileInfo.checkComplementaryKey(key));
            };


            //------------------------EVALUATOR - SUBMISSIONS-------------------------------------------
            //Gets the list of submissions assigned to the evaluator currently logged in to the system
            Get["/getAssignedSubmissions/{evaluatorUserID:long}/{index:int}"] = parameters =>
            {
                long evaluatorUserID = parameters.evaluatorUserID; //ID of the evaluator that is currently signed in
                int index = parameters.index;
                SubmissionPagingQuery assignedSubmissions = submission.getAssignedSubmissions(evaluatorUserID, index);
                if (assignedSubmissions == null)
                {
                    assignedSubmissions = new SubmissionPagingQuery();
                }
                return Response.AsJson(assignedSubmissions);
            };

            //Search within a list with a specific criteria
            Get["/searchAssignedSubmission/{evaluatorUserID}/{index}/{criteria}"] = parameters =>
            {
                long evaluatorUserID = parameters.evaluatorUserID; //ID of the evaluator that is currently signed in
                int index = parameters.index;
                string criteria = parameters.criteria;
                SubmissionPagingQuery assignedSubmissions = submission.searchAssignedSubmission(evaluatorUserID, index, criteria);
                if (assignedSubmissions == null)
                {
                    assignedSubmissions = new SubmissionPagingQuery();
                }
                return Response.AsJson(assignedSubmissions);
            };

            //Get the info of a submission
            Get["/getSubmission/{submissionID:long}/{evaluatorID:long}"] = parameters =>
            {
                long submissionID = parameters.submissionID;
                long evaluatorID = parameters.evaluatorID;
                AssignedSubmission sub = submission.getSubmissionForEvaluation(submissionID, evaluatorID);
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
                Submission prevSub = submission.deleteSubmission(submissionID);
                if (prevSub != null)
                    return Response.AsJson(prevSub);
                else return null;
            };
            //Add a submission
            Post["/postSubmission"] = parameters =>
            {
                panel pannelToAdd = null;
                workshop workshopToAdd = null;
                submission submissionToAdd = this.Bind<submission>();
                usersubmission usersubTA = this.Bind<usersubmission>();

                int submissionTypeID = submissionToAdd.submissionTypeID;
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
            //add new file to submission
            Put["/addSubmissionFile"] = parameters =>
                {
                    documentssubmitted doc = this.Bind<documentssubmitted>();
                    if (doc.document != null)
                    {
                        if (submission.addSubmissionFile(doc))
                            return HttpStatusCode.OK;
                        else
                            return HttpStatusCode.Conflict;
                    }
                    else return HttpStatusCode.Conflict;
                };
            //manage existing files for a submission
            Put["/manageExistingFiles"] = parameters =>
                {
                    //documentssubmitted sub = this.Bind<documentssubmitted>();
                    ExistingFile sub = this.Bind<ExistingFile>();
                    if (sub.IDsList.Count == 0)
                    {
                        return HttpStatusCode.OK;
                    }
                    else
                    {

                        if (submission.manageExistingFiles(sub.submissionID, sub.IDsList))
                            return HttpStatusCode.OK;
                        else
                            return HttpStatusCode.Conflict;
                    }
                };

            //re-create final submission files
            Put["/createFinalSubmissionFiles"] = parameters =>
            {
                //documentssubmitted sub = this.Bind<documentssubmitted>();
                ExistingFile sub = this.Bind<ExistingFile>();
                if (sub.IDsList.Count == 0)
                {
                    return HttpStatusCode.OK;
                }
                else
                {

                    if (submission.createFinalSubmissionFiles(sub.submissionID, sub.prevID, sub.IDsList))
                        return HttpStatusCode.OK;
                    else
                        return HttpStatusCode.Conflict;
                }
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
            //get the deadline for the additon of submissions
            Get["/getSubmissionDeadlines"] = parameters =>
                {
                    return Response.AsJson(submission.getSubmissionDeadlines());
                };
            //get the template file
            Get["/getTemplateFile/{id}"] = parameters =>
                {
                    int templateID = parameters.id;
                    return Response.AsJson(profileAuthorization.getTemplateFile(templateID));
                };
            //get the authorization file 
            Get["/getAuthorizationFile/{id}"] = parameters =>
            {
                int authorizationID = parameters.id;
                return Response.AsJson(profileAuthorization.getAuthorizationFile(authorizationID));
            };
            //get evaluation template
            Get["/getEvaluationTemplate/{templateID:long}"] = parameters =>
                {
                    int templateID = parameters.templateID;
                    return Response.AsJson(submission.getEvaluationTemplate(templateID));
                };
            //get evaluation file 
            Get["/getEvaluationFile/{submissionID:long}/{evaluatorID:long}"] = parameters =>
            {
                long submissionID = parameters.submissionID;
                long evaluatorID = parameters.evaluatorID;
                return Response.AsJson(submission.getEvaluationFile(submissionID, evaluatorID));
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
        public class ExistingFile
        {
            public long submissionID { get; set; }
            public long prevID { get; set; }
            public List<long> IDsList { get; set; }
        }
    }
}

