using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;


namespace NancyService.Modules
{
    class SubmissionManager
    {
        //ccwicEmail
        string ccwicEmail = "ccwictest@gmail.com";
        string ccwicEmailPass = "ccwic123456789";

        //Jaimeiris - gets the submission with ID submission ID to be evaluated by evaluator with evaluatorID
        public AssignedSubmission getSubmission(long submissionID, long evaluatorID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //gets all the evaluations assigned to the given evaluator
                    AssignedSubmission subs = new AssignedSubmission();
                    evaluatiorsubmission sub;
                    bool isFinalVersion = context.usersubmission.Where(c => c.finalSubmissionID == submissionID).FirstOrDefault() == null ? false : true;

                    sub = context.evaluatiorsubmissions.Where(c => c.submissionID == submissionID && c.evaluatorID == evaluatorID && c.deleted == false).FirstOrDefault();

                    if (sub.submission.submissionTypeID == 1 || sub.submission.submissionTypeID == 2 || sub.submission.submissionTypeID == 4)
                    {

                        subs = new AssignedSubmission
                        {
                            submissionID = sub.submissionID,
                            userType = sub.submission.usersubmissions1.FirstOrDefault() == null ? null : sub.submission.usersubmissions1.FirstOrDefault().user.usertype.userTypeName,
                            evaluatorID = sub.evaluatorID,
                            submissionTitle = sub.submission.title,
                            topic = sub.submission.topiccategory.name,
                            submitterFirstName = sub.submission.usersubmissions1.FirstOrDefault() == null ? null : sub.submission.usersubmissions1.FirstOrDefault().user.firstName,
                            submitterLastName = sub.submission.usersubmissions1.FirstOrDefault() == null ? null : sub.submission.usersubmissions1.FirstOrDefault().user.lastName,
                            submissionAbstract = sub.submission.submissionAbstract,
                            submissionFileList = sub.submission.documentssubmitteds.Where(u => u.deleted == false).
                                Select(c => new SubmissionDocument
                                {
                                    documentssubmittedID = c.documentssubmittedID,
                                    submissionID = c.submissionID,
                                    documentName = c.documentName,
                                    //document = c.document,
                                    deleted = c.deleted
                                }).ToList(),
                            submissionType = sub.submission.submissiontype.name,
                            evaluationTemplateID = sub.submission.templatesubmissions.Where(u => u.deleted == false).FirstOrDefault() == null ? -1 : sub.submission.templatesubmissions.Where(u => u.deleted == false).FirstOrDefault().templateID,
                            panelistNames = null,
                            plan = null,
                            guideQuestions = null,
                            format = null,
                            equipment = null,
                            duration = null,
                            delivery = null,
                            evaluationsubmittedID = (sub.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault() == null ? -1 : sub.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault().evaluationsubmittedID),
                            evaluatiorSubmissionID = sub.evaluationsubmissionID,
                            evaluationName = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.evaluationName).FirstOrDefault(),
                            evaluationFile = sub.evaluationsubmitteds.Where(d => d.deleted == false).Select(r => r.evaluationFile).FirstOrDefault(),
                            evaluationScore = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.score).FirstOrDefault(),
                            publicFeedback = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.publicFeedback).FirstOrDefault(),
                            privateFeedback = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.privateFeedback).FirstOrDefault(),
                            subIsEvaluated = (sub.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault() == null ? false : true),
                            allowFinalVersion = ((sub.submission.usersubmissions1.FirstOrDefault() == null ?
                            null : sub.submission.usersubmissions1.FirstOrDefault().allowFinalVersion) == null ?
                            false : sub.submission.usersubmissions1.FirstOrDefault().allowFinalVersion) == false ? false : true
                        };
                    }
                    else if (sub.submission.submissionTypeID == 3)
                    {

                        subs = new AssignedSubmission
                        {
                            submissionID = sub.submissionID,
                            userType = sub.submission.usersubmissions1.FirstOrDefault() == null ? null : sub.submission.usersubmissions1.FirstOrDefault().user.usertype.userTypeName,
                            evaluatorID = sub.evaluatorID,
                            submissionTitle = sub.submission.title,
                            topic = sub.submission.topiccategory.name,
                            submitterFirstName = sub.submission.usersubmissions1.FirstOrDefault() == null ? null : sub.submission.usersubmissions1.FirstOrDefault().user.firstName,
                            submitterLastName = sub.submission.usersubmissions1.FirstOrDefault() == null ? null : sub.submission.usersubmissions1.FirstOrDefault().user.lastName,
                            submissionAbstract = sub.submission.submissionAbstract,
                            submissionFileList = sub.submission.documentssubmitteds.Where(u => u.deleted == false).
                                Select(c => new SubmissionDocument
                                {
                                    documentssubmittedID = c.documentssubmittedID,
                                    submissionID = c.submissionID,
                                    documentName = c.documentName,
                                    //document = c.document,
                                    deleted = c.deleted
                                }).ToList(),
                            submissionType = sub.submission.submissiontype.name,
                            evaluationTemplateID = sub.submission.templatesubmissions.Where(u => u.deleted == false).FirstOrDefault() == null ? -1 : sub.submission.templatesubmissions.Where(u => u.deleted == false).FirstOrDefault().templateID,
                            panelistNames = sub.submission.panels.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.submission.panels.Where(y => y.deleted == false).FirstOrDefault().panelistNames,
                            plan = sub.submission.panels.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.submission.panels.Where(y => y.deleted == false).FirstOrDefault().plan,
                            guideQuestions = sub.submission.panels.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.submission.panels.Where(y => y.deleted == false).FirstOrDefault().guideQuestion,
                            format = sub.submission.panels.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.submission.panels.Where(y => y.deleted == false).FirstOrDefault().formatDescription,
                            equipment = sub.submission.panels.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.submission.panels.Where(y => y.deleted == false).FirstOrDefault().necessaryEquipment,
                            duration = null,
                            delivery = null,
                            evaluationsubmittedID = (sub.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault() == null ? -1 : sub.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault().evaluationsubmittedID),
                            evaluatiorSubmissionID = sub.evaluationsubmissionID,
                            evaluationName = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.evaluationName).FirstOrDefault(),
                            evaluationFile = sub.evaluationsubmitteds.Where(d => d.deleted == false).Select(r => r.evaluationFile).FirstOrDefault(),
                            evaluationScore = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.score).FirstOrDefault(),
                            publicFeedback = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.publicFeedback).FirstOrDefault(),
                            privateFeedback = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.privateFeedback).FirstOrDefault(),
                            subIsEvaluated = (sub.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault() == null ? false : true),
                            allowFinalVersion = ((sub.submission.usersubmissions1.FirstOrDefault() == null ?
                            null : sub.submission.usersubmissions1.FirstOrDefault().allowFinalVersion) == null ?
                            false : sub.submission.usersubmissions1.FirstOrDefault().allowFinalVersion) == false ? false : true
                        };
                    }
                    else if (sub.submission.submissionTypeID == 5)
                    {

                        subs = new AssignedSubmission
                        {
                            submissionID = sub.submissionID,
                            userType = sub.submission.usersubmissions1.FirstOrDefault() == null ? null : sub.submission.usersubmissions1.FirstOrDefault().user.usertype.userTypeName,
                            evaluatorID = sub.evaluatorID,
                            submissionTitle = sub.submission.title,
                            topic = sub.submission.topiccategory.name,
                            submitterFirstName = sub.submission.usersubmissions1.FirstOrDefault() == null ? null : sub.submission.usersubmissions1.FirstOrDefault().user.firstName,
                            submitterLastName = sub.submission.usersubmissions1.FirstOrDefault() == null ? null : sub.submission.usersubmissions1.FirstOrDefault().user.lastName,
                            submissionAbstract = sub.submission.submissionAbstract,
                            submissionFileList = sub.submission.documentssubmitteds.Where(u => u.deleted == false).
                                Select(c => new SubmissionDocument
                                {
                                    documentssubmittedID = c.documentssubmittedID,
                                    submissionID = c.submissionID,
                                    documentName = c.documentName,
                                    //document = c.document,
                                    deleted = c.deleted
                                }).ToList(),
                            submissionType = sub.submission.submissiontype.name,
                            evaluationTemplateID = sub.submission.templatesubmissions.Where(u => u.deleted == false).FirstOrDefault() == null ? -1 : sub.submission.templatesubmissions.Where(u => u.deleted == false).FirstOrDefault().templateID,
                            panelistNames = null,
                            plan = sub.submission.workshops.Where(y => y.deleted == false).FirstOrDefault().plan,
                            guideQuestions = null,
                            format = null,
                            equipment = sub.submission.workshops.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.submission.workshops.Where(y => y.deleted == false).FirstOrDefault().necessary_equipment,
                            duration = sub.submission.workshops.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.submission.workshops.Where(y => y.deleted == false).FirstOrDefault().duration,
                            delivery = sub.submission.workshops.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.submission.workshops.Where(y => y.deleted == false).FirstOrDefault().delivery,
                            evaluationsubmittedID = (sub.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault() == null ? -1 : sub.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault().evaluationsubmittedID),
                            evaluatiorSubmissionID = sub.evaluationsubmissionID,
                            evaluationName = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.evaluationName).FirstOrDefault(),
                            evaluationFile = sub.evaluationsubmitteds.Where(d => d.deleted == false).Select(r => r.evaluationFile).FirstOrDefault(),
                            evaluationScore = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.score).FirstOrDefault(),
                            publicFeedback = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.publicFeedback).FirstOrDefault(),
                            privateFeedback = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.privateFeedback).FirstOrDefault(),
                            subIsEvaluated = (sub.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault() == null ? false : true),
                            allowFinalVersion = ((sub.submission.usersubmissions1.FirstOrDefault() == null ?
                            null : sub.submission.usersubmissions1.FirstOrDefault().allowFinalVersion) == null ?
                            false : sub.submission.usersubmissions1.FirstOrDefault().allowFinalVersion) == false ? false : true
                        };
                    }
                    subs.isFinalVersion = isFinalVersion;
                    return subs;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.getSubmission error " + ex);
                return null;
            }
        }

        //Jaimeiris - gets the submission with ID submission ID to be evaluated by evaluator with evaluatorID
        public Evaluation getEvaluationDetails(long submissionID, long evaluatorID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //gets all the evaluations assigned to the given evaluator
                    Evaluation subs = new Evaluation();
                    evaluatiorsubmission sub;

                    sub = context.evaluatiorsubmissions.Where(c => c.submissionID == submissionID && c.evaluatorID == evaluatorID && c.deleted == false).FirstOrDefault();
                    if (sub != null)
                    {
                        subs = new Evaluation
                        {
                            submissionID = sub.submissionID,
                            evaluatorID = sub.evaluatorID,
                            templateID = sub.submission.templatesubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? -1 : sub.submission.templatesubmissions.Where(c => c.deleted == false).FirstOrDefault().templateID,
                            templateName = sub.submission.templatesubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : sub.submission.templatesubmissions.Where(c => c.deleted == false).FirstOrDefault().template.name,
                            evaluationFileName = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.evaluationName).FirstOrDefault(),
                            evaluationFile = sub.evaluationsubmitteds.Where(d => d.deleted == false).Select(r => r.evaluationFile).FirstOrDefault(),
                            score = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.score).FirstOrDefault(),
                            publicFeedback = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.publicFeedback).FirstOrDefault(),
                            privateFeedback = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.privateFeedback).FirstOrDefault(),
                            evaluatorFirstName = sub.evaluator.user.firstName,
                            evaluatorLastName = sub.evaluator.user.lastName
                        };
                    }
                    return subs;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.getEvaluationDetails error " + ex);
                return null;
            }
        }

        public SubmissionPagingQuery getAssignedSubmissions(long userID, int index)
        {
            SubmissionPagingQuery page = new SubmissionPagingQuery();
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    int pageSize = 10;
                    //gets all final evaluations assigned to the given evaluator
                    List<Submission> assignedFinalSubmissions = context.evaluatiorsubmissions.
                        Where(c => c.evaluator.userID == userID && c.deleted == false && c.submission.usersubmissions.Where(d => d.deleted == false).FirstOrDefault() != null).
                        Select(i => new Submission
                        {
                            submissionID = i.submissionID,
                            evaluatorID = i.evaluatorID,
                            userType = i.submission.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : i.submission.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().user.usertype.userTypeName,
                            submissionTitle = i.submission.title,
                            topic = i.submission.topiccategory.name,
                            isEvaluated = (i.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault() == null ? false : true),
                            isFinalSubmission = true
                        }).ToList();

                    //gets all non-final the evaluations assigned to the given evaluator
                    List<Submission> assignedSubmissions = context.evaluatiorsubmissions.
                        Where(c => c.evaluator.userID == userID && c.deleted == false && c.submission.usersubmissions1.Where(d => d.deleted == false).FirstOrDefault() != null).
                        Select(i => new Submission
                        {
                            submissionID = i.submissionID,
                            evaluatorID = i.evaluatorID,
                            userType = i.submission.usersubmissions1.Where(c => c.deleted == false).FirstOrDefault() == null ? null : i.submission.usersubmissions1.Where(c => c.deleted == false).FirstOrDefault().user.usertype.userTypeName,
                            submissionTitle = i.submission.title,
                            topic = i.submission.topiccategory.name,
                            isEvaluated = (i.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault() == null ? false : true),
                            isFinalSubmission = false
                        }).ToList();
                    foreach (var finalSub in assignedFinalSubmissions)
                    {
                        assignedSubmissions.Add(finalSub);
                    }
                    assignedSubmissions = assignedSubmissions.OrderBy(n => n.submissionTitle).ToList();
                    page.rowCount = assignedSubmissions.Count();
                    if (page.rowCount > 0)
                    {
                        page.maxIndex = (int)Math.Ceiling(page.rowCount / (double)pageSize);
                        List<Submission> submissionPage = assignedSubmissions.Skip(pageSize * index).Take(pageSize).ToList(); //Skip past rows and take new elements
                        page.results = submissionPage;
                    }

                    return page;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.getAssignedSubmissions error " + ex);
                return null;
            }
        }

        //Search within a list with a specific criteria
        public SubmissionPagingQuery searchAssignedSubmission(long userID, int index, string criteria)
        {
            SubmissionPagingQuery page = new SubmissionPagingQuery();
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    int pageSize = 10;
                    bool searchingFinal = criteria.Contains("final");
                    bool searchingPending = criteria.Contains("pending");
                    //gets all final evaluations assigned to the given evaluator
                    List<Submission> assignedFinalSubmissions = context.evaluatiorsubmissions.
                        Where(c => (c.submission.title.ToLower().Contains(criteria) 
                            || c.submission.usersubmissions.Where(j => j.deleted == false).FirstOrDefault().user.usertype.userTypeName.ToLower().Contains(criteria) 
                            || c.submission.topiccategory.name.ToLower().Contains(criteria)
                            || (c.evaluationsubmitteds.Where(j => j.deleted == false).FirstOrDefault() == null) == searchingPending
                            || searchingFinal) 
                            && c.evaluator.userID == userID && c.deleted == false && c.submission.usersubmissions.Where(d => d.deleted == false).FirstOrDefault() != null).
                        Select(i => new Submission
                        {
                            submissionID = i.submissionID,
                            evaluatorID = i.evaluatorID,
                            userType = i.submission.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : i.submission.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().user.usertype.userTypeName,
                            submissionTitle = i.submission.title,
                            topic = i.submission.topiccategory.name,
                            isEvaluated = (i.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault() == null ? false : true),
                            isFinalSubmission = true
                        }).ToList();

                    //gets all non-final the evaluations assigned to the given evaluator
                    List<Submission> assignedSubmissions = context.evaluatiorsubmissions.
                        Where(c => (c.submission.title.ToLower().Contains(criteria) 
                            || c.submission.usersubmissions.Where(j => j.deleted == false).FirstOrDefault().user.usertype.userTypeName.ToLower().Contains(criteria) 
                            || c.submission.topiccategory.name.ToLower().Contains(criteria)
                            || (c.evaluationsubmitteds.Where(j => j.deleted == false).FirstOrDefault() == null) == searchingPending) 
                            && c.evaluator.userID == userID && c.deleted == false && c.submission.usersubmissions1.Where(d => d.deleted == false).FirstOrDefault() != null).
                        Select(i => new Submission
                        {
                            submissionID = i.submissionID,
                            evaluatorID = i.evaluatorID,
                            userType = i.submission.usersubmissions1.Where(c => c.deleted == false).FirstOrDefault() == null ? null : i.submission.usersubmissions1.Where(c => c.deleted == false).FirstOrDefault().user.usertype.userTypeName,
                            submissionTitle = i.submission.title,
                            topic = i.submission.topiccategory.name,
                            isEvaluated = (i.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault() == null ? false : true),
                            isFinalSubmission = false
                        }).ToList();
                    foreach (var finalSub in assignedFinalSubmissions)
                    {
                        assignedSubmissions.Add(finalSub);
                    }
                    assignedSubmissions = assignedSubmissions.OrderBy(n => n.submissionTitle).ToList();
                    page.rowCount = assignedSubmissions.Count();
                    if (page.rowCount > 0)
                    {
                        page.maxIndex = (int)Math.Ceiling(page.rowCount / (double)pageSize);
                        List<Submission> submissionPage = assignedSubmissions.Skip(pageSize * index).Take(pageSize).ToList(); //Skip past rows and take new elements
                        page.results = submissionPage;
                    }

                    return page;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.getAssignedSubmissions error " + ex);
                return null;
            }
        }

        public bool addEvaluation(evaluationsubmitted evaluation, usersubmission usersubIn)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //Checking if its final or initial submission, here initialSubmissionID is actually the id of the submission evaluated, not necessarily the initial
                    bool isFinalSubmission = context.usersubmission.Where(c => c.finalSubmissionID == usersubIn.initialSubmissionID).FirstOrDefault() == null ?
                        false : (context.usersubmission.Where(d => d.initialSubmissionID == usersubIn.initialSubmissionID).FirstOrDefault() == null ? true : false);
                    usersubmission userSub;

                    evaluation.deleted = false;
                    context.evaluationsubmitteds.Add(evaluation);
                    context.SaveChanges();
                    context.evaluatiorsubmissions.Where(c => c.evaluationsubmissionID == evaluation.evaluatiorSubmissionID).FirstOrDefault().statusEvaluation = "Evaluated";
                    context.SaveChanges();
                    if (isFinalSubmission)
                    {
                        userSub = context.usersubmission.Where(c => c.finalSubmissionID == usersubIn.initialSubmissionID).FirstOrDefault();
                    }
                    else
                    {
                        userSub = context.usersubmission.Where(c => c.initialSubmissionID == usersubIn.initialSubmissionID).FirstOrDefault();
                    }
                    userSub.allowFinalVersion = usersubIn.allowFinalVersion;
                    context.SaveChanges();
                    if (usersubIn.allowFinalVersion == true)
                    {
                        String email = null;
                        email = (context.usersubmission.Where(c => c.initialSubmissionID == usersubIn.initialSubmissionID).FirstOrDefault() == null ? null :
                            context.usersubmission.Where(c => c.initialSubmissionID == usersubIn.initialSubmissionID).FirstOrDefault().user.membership.email);
                        if (email == null) email = context.usersubmission.Where(c => c.finalSubmissionID == usersubIn.initialSubmissionID).FirstOrDefault() == null ?
                             null : context.usersubmission.Where(c => c.finalSubmissionID == usersubIn.initialSubmissionID).FirstOrDefault().user.membership.email;
                        
                        try { sendFinalVersionAllowedEmail(email, context.submissions.Where(c => c.submissionID == usersubIn.initialSubmissionID).FirstOrDefault().title); }

                        catch (Exception ex)
                        {
                            Console.Write("SubmissionManager.addEvaluation error " + ex);
                            return false;
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.addEvaluation error " + ex);
                return false;
            }
        }

        //Send email when submission status has been changed
        private void sendFinalVersionAllowedEmail(string email, String submissionTitle)
        {
            MailAddress ccwic = new MailAddress(ccwicEmail);
            MailAddress user = new MailAddress(email);
            MailMessage mail = new System.Net.Mail.MailMessage(ccwic, user);
            
                mail.Subject = "Caribbean Celebration of Women in Computing Submission Requirement";
                mail.Body = "Greetings, \n\n " +
                    "Our evaluators have asked that you submit a final version of your submission with the name: " + submissionTitle + ". To view the comments made about your submission and the desired changes, please login to view your profile through the following link: \n\n" +
                    "http://136.145.116.238/#/Login/Log" + ".";            
            
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            smtp.Credentials = new NetworkCredential(
                ccwicEmail, ccwicEmailPass);
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }

        public bool editEvaluation(evaluationsubmitted evaluation, usersubmission userSubIn)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    bool isFinalSubmission = context.usersubmission.Where(c => c.finalSubmissionID == userSubIn.initialSubmissionID).FirstOrDefault() == null ?
                        false : (context.usersubmission.Where(d => d.initialSubmissionID == userSubIn.initialSubmissionID).FirstOrDefault() == null ? true : false);
                    usersubmission userSub;

                    evaluationsubmitted dbEvaluation = context.evaluationsubmitteds.Where(c => c.evaluationsubmittedID == evaluation.evaluationsubmittedID).FirstOrDefault();
                    dbEvaluation.deleted = false;
                    if (evaluation.evaluationName != null || evaluation.evaluationFile != null)
                    {
                        dbEvaluation.evaluationName = evaluation.evaluationName;
                        dbEvaluation.evaluationFile = evaluation.evaluationFile;
                    }
                    dbEvaluation.score = evaluation.score;
                    dbEvaluation.publicFeedback = evaluation.publicFeedback;
                    dbEvaluation.privateFeedback = evaluation.privateFeedback;
                    var evaluatorSub = dbEvaluation.evaluatiorsubmission;
                    if (evaluatorSub != null)
                    {
                        dbEvaluation.evaluatiorsubmission.statusEvaluation = "Evaluated";
                    }
                    if (isFinalSubmission)
                    {
                        userSub = context.usersubmission.Where(c => c.finalSubmissionID == userSubIn.initialSubmissionID).FirstOrDefault();
                    }
                    else
                    {
                        userSub = context.usersubmission.Where(c => c.initialSubmissionID == userSubIn.initialSubmissionID).FirstOrDefault(); 
                        userSub.allowFinalVersion = userSubIn.allowFinalVersion;
                    }
                    context.SaveChanges();

                    if (userSubIn.allowFinalVersion == true)
                    {
                        String email = null;
                        email = (context.usersubmission.Where(c => c.initialSubmissionID == userSubIn.initialSubmissionID).FirstOrDefault() == null ? null :
                            context.usersubmission.Where(c => c.initialSubmissionID == userSubIn.initialSubmissionID).FirstOrDefault().user.membership.email);
                        if (email == null) email = context.usersubmission.Where(c => c.finalSubmissionID == userSubIn.initialSubmissionID).FirstOrDefault() == null ?
                             null : context.usersubmission.Where(c => c.finalSubmissionID == userSubIn.initialSubmissionID).FirstOrDefault().user.membership.email;

                        try { sendFinalVersionAllowedEmail(email, context.submissions.Where(c => c.submissionID == userSubIn.initialSubmissionID).FirstOrDefault().title); }

                        catch (Exception ex)
                        {
                            Console.Write("SubmissionManager.editEvaluation error " + ex);
                            return false;
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.editEvaluation error " + ex);
                return false;
            }
        }


        public List<Submission> getUserSubmissions(long userID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //get all final submissions
                    List<Submission> userFinalSubmissions = context.usersubmission.Where(c => c.userID == userID && c.deleted == false && c.finalSubmissionID != null && c.submission.byAdmin == false).
                        Select(i => new Submission
                        {
                            submissionID = i.submission == null ? -1 : i.submission.submissionID,
                            firstName = i.user.firstName,
                            lastName = i.user.lastName,
                            email = i.user.membership.email,
                            submissionTypeName = i.submission == null ? null : i.submission.submissiontype.name,
                            submissionTypeID = i.submission == null ? -1 : i.submission.submissionTypeID,
                            submissionTitle = i.submission == null ? null : i.submission.title,
                            topiccategoryID = i.submission == null ? -1 : i.submission.topicID,
                            status = i.submission == null ? null : i.submission.status,
                            templateName = i.submission.templatesubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            null : i.submission.templatesubmissions.Where(c => c.deleted == false).FirstOrDefault().template.name,
                            templateID = i.submission.templatesubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            -1 : i.submission.templatesubmissions.Where(c => c.deleted == false).FirstOrDefault().templateID,
                            isEvaluated = (i.submission.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : i.submission.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault().statusEvaluation) == "Evaluated" ? true : false,
                            isAssigned = i.submission.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? false : true,
                            isFinalSubmission = true

                        }).ToList();
                    //get all submissions that do no have a final submission
                    List<Submission> userSubmissions = context.usersubmission.Where(c => c.userID == userID && c.deleted == false && c.finalSubmissionID == null && c.submission1.byAdmin == false).
                        Select(i => new Submission
                        {
                            submissionID = i.submission1 == null ? -1 : i.submission1.submissionID,
                            submissionTypeName = i.submission1 == null ? null : i.submission1.submissiontype.name,
                            submissionTypeID = i.submission1 == null ? -1 : i.submission1.submissionTypeID,
                            submissionTitle = i.submission1 == null ? null : i.submission1.title,
                            topiccategoryID = i.submission1 == null ? -1 : i.submission1.topicID,
                            status = i.submission1 == null ? null : i.submission1.status,
                            isEvaluated = (i.submission1.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : i.submission1.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault().statusEvaluation) == "Evaluated" ? true : false,
                            isAssigned = i.submission1.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? false : true,
                            isFinalSubmission = false,
                            finalSubmissionAllowed = (i.allowFinalVersion == null ? false : i.allowFinalVersion) == false ? false : true
                        }).ToList();
                    foreach (Submission final in userFinalSubmissions)
                    {
                        userSubmissions.Add(final);
                    }
                    return userSubmissions.OrderBy(x => x.submissionTitle).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.getUserSubmissions error " + ex);
                return null;
            }
        }
        //Jaimeiris - Gets the current and previous (when applicable) submission with submissionID
        public CurrAndPrevSub getUserSubmission(long submissionID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //gets all the evaluations assigned to the given evaluator
                    CurrAndPrevSub subs = new CurrAndPrevSub();

                    submission sub = context.submissions.Where(c => c.submissionID == submissionID && c.deleted == false).FirstOrDefault();
                    bool isFinalVersion = context.usersubmission.Where(c => c.finalSubmissionID == submissionID).FirstOrDefault() == null ? false : true;
                    if (sub.submissionTypeID == 1 || sub.submissionTypeID == 2 || sub.submissionTypeID == 4)
                    {

                        subs = new CurrAndPrevSub
                        {
                            submissionID = sub.submissionID,
                            submitterFirstName = sub.usersubmissions1.FirstOrDefault() != null ?
                            sub.usersubmissions1.FirstOrDefault().user.firstName : (sub.usersubmissions.FirstOrDefault() == null ? null : sub.usersubmissions.FirstOrDefault().user.firstName),
                            submitterLastName = sub.usersubmissions1.FirstOrDefault() != null ?
                            sub.usersubmissions1.FirstOrDefault().user.lastName : (sub.usersubmissions.FirstOrDefault() == null ? null : sub.usersubmissions.FirstOrDefault().user.lastName),
                            submitterEmail = sub.usersubmissions1.FirstOrDefault() != null ?
                            sub.usersubmissions1.FirstOrDefault().user.membership.email : (sub.usersubmissions.FirstOrDefault() == null ? null : sub.usersubmissions.FirstOrDefault().user.membership.email),
                            submissionTitle = sub.title,
                            topic = sub.topiccategory.name,
                            topiccategoryID = sub.topiccategory.topiccategoryID,
                            submissionAbstract = sub.submissionAbstract,
                            submissionFileList = sub.documentssubmitteds.Where(u => u.deleted == false).
                                Select(c => new SubmissionDocument
                                {
                                    documentssubmittedID = c.documentssubmittedID,
                                    submissionID = c.submissionID,
                                    documentName = c.documentName,
                                    //document = c.document,
                                    deleted = c.deleted
                                }).ToList(),
                            submissionType = sub.submissiontype.name,
                            submissionTypeID = sub.submissionTypeID,
                            templateName = sub.templatesubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            null : sub.templatesubmissions.Where(c => c.deleted == false).FirstOrDefault().template.name,
                            templateID = sub.templatesubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            -1 : sub.templatesubmissions.Where(c => c.deleted == false).FirstOrDefault().templateID,
                            panelistNames = null,
                            plan = null,
                            guideQuestions = null,
                            format = null,
                            equipment = null,
                            duration = null,
                            delivery = null,
                            subIsEvaluated = (sub.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : sub.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault().statusEvaluation) == "Evaluated" ? true : false,
                            publicFeedback = (sub.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            null : sub.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault().evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault()) == null ?
                            null : sub.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault().evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault().publicFeedback,
                            //get previous submission if possible
                            hasPrevVersion = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? false : true,
                            prevSubmissionID = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? -1 : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.submissionID,
                            prevSubmissionTitle = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.title,
                            prevTopic = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.topiccategory.name,
                            prevSubmissionAbstract = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.submissionAbstract,
                            prevSubmissionFileList = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.documentssubmitteds.Where(u => u.deleted == false).
                                Select(c => new SubmissionDocument
                                {
                                    documentssubmittedID = c.documentssubmittedID,
                                    submissionID = c.submissionID,
                                    documentName = c.documentName,
                                    //document = c.document,
                                    deleted = c.deleted
                                }).ToList(),
                            prevSubmissionType = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.submissiontype.name,
                            prevPanelistNames = null,
                            prevPlan = null,
                            prevGuideQuestions = null,
                            prevFormat = null,
                            prevEquipment = null,
                            prevDuration = null,
                            prevDelivery = null,
                            prevSubIsEvaluated = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            false : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            false : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault().statusEvaluation == "Evaluated" ? true : false,
                            prevPublicFeedback = ((sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1 == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault()) == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault().evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault()) == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault().evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault().publicFeedback


                        };
                    }
                    else if (sub.submissionTypeID == 3)
                    {

                        subs = new CurrAndPrevSub
                        {
                            submissionID = sub.submissionID,
                            submitterFirstName = sub.usersubmissions1.FirstOrDefault() != null ?
                            sub.usersubmissions1.FirstOrDefault().user.firstName : (sub.usersubmissions.FirstOrDefault() == null ? null : sub.usersubmissions.FirstOrDefault().user.firstName),
                            submitterLastName = sub.usersubmissions1.FirstOrDefault() != null ?
                            sub.usersubmissions1.FirstOrDefault().user.lastName : (sub.usersubmissions.FirstOrDefault() == null ? null : sub.usersubmissions.FirstOrDefault().user.lastName),
                            submitterEmail = sub.usersubmissions1.FirstOrDefault() != null ?
                            sub.usersubmissions1.FirstOrDefault().user.membership.email : (sub.usersubmissions.FirstOrDefault() == null ? null : sub.usersubmissions.FirstOrDefault().user.membership.email),
                            submissionTitle = sub.title,
                            topic = sub.topiccategory.name,
                            topiccategoryID = sub.topiccategory.topiccategoryID,
                            submissionAbstract = sub.submissionAbstract,
                            submissionFileList = sub.documentssubmitteds.Where(u => u.deleted == false).
                                Select(c => new SubmissionDocument
                                {
                                    documentssubmittedID = c.documentssubmittedID,
                                    submissionID = c.submissionID,
                                    documentName = c.documentName,
                                    //document = c.document,
                                    deleted = c.deleted
                                }).ToList(),
                            submissionType = sub.submissiontype.name,
                            submissionTypeID = sub.submissionTypeID,
                            templateName = sub.templatesubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            null : sub.templatesubmissions.Where(c => c.deleted == false).FirstOrDefault().template.name,
                            templateID = sub.templatesubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            -1 : sub.templatesubmissions.Where(c => c.deleted == false).FirstOrDefault().templateID,
                            panelistNames = (sub.panels.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.panels.Where(y => y.deleted == false).FirstOrDefault().panelistNames),
                            plan = (sub.panels.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.panels.Where(y => y.deleted == false).FirstOrDefault().plan),
                            guideQuestions = (sub.panels.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.panels.Where(y => y.deleted == false).FirstOrDefault().guideQuestion),
                            format = (sub.panels.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.panels.Where(y => y.deleted == false).FirstOrDefault().formatDescription),
                            equipment = (sub.panels.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.panels.Where(y => y.deleted == false).FirstOrDefault().necessaryEquipment),
                            duration = null,
                            delivery = null,
                            subIsEvaluated = (sub.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : sub.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault().statusEvaluation) == "Evaluated" ? true : false,
                            publicFeedback = (sub.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                             null : sub.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault().evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault()) == null ?
                             null : sub.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault().evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault().publicFeedback,

                            //previous
                            hasPrevVersion = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? false : true,
                            prevSubmissionID = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? -1 : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.submissionID,
                            prevSubmissionTitle = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.title,
                            prevTopic = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.topiccategory.name,
                            prevSubmissionAbstract = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.submissionAbstract,
                            prevSubmissionFileList = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.documentssubmitteds.Where(u => u.deleted == false).
                                Select(c => new SubmissionDocument
                                {
                                    documentssubmittedID = c.documentssubmittedID,
                                    submissionID = c.submissionID,
                                    documentName = c.documentName,
                                    //document = c.document,
                                    deleted = c.deleted
                                }).ToList(),
                            prevSubmissionType = sub.usersubmissions.FirstOrDefault() == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.submissiontype.name,
                            prevPanelistNames = (sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.panels.Where(y => y.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.panels.Where(y => y.deleted == false).FirstOrDefault().panelistNames),
                            prevPlan = (sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.panels.Where(y => y.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.panels.Where(y => y.deleted == false).FirstOrDefault().plan),
                            prevGuideQuestions = (sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.panels.Where(y => y.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.panels.Where(y => y.deleted == false).FirstOrDefault().guideQuestion),
                            prevFormat = (sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.panels.Where(y => y.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.panels.Where(y => y.deleted == false).FirstOrDefault().formatDescription),
                            prevEquipment = (sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.panels.Where(y => y.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.panels.Where(y => y.deleted == false).FirstOrDefault().necessaryEquipment),
                            prevDuration = null,
                            prevDelivery = null,
                            prevSubIsEvaluated = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            false : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            false : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault().statusEvaluation == "Evaluated" ? true : false,
                            prevPublicFeedback = ((sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                             null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1 == null ?
                             null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault()) == null ?
                             null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault().evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault()) == null ?
                             null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault().evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault().publicFeedback

                        };
                    }
                    else if (sub.submissionTypeID == 5)
                    {

                        subs = new CurrAndPrevSub
                        {
                            submissionID = sub.submissionID,
                            submitterFirstName = sub.usersubmissions1.FirstOrDefault() != null ?
                            sub.usersubmissions1.FirstOrDefault().user.firstName : (sub.usersubmissions.FirstOrDefault() == null ? null : sub.usersubmissions.FirstOrDefault().user.firstName),
                            submitterLastName = sub.usersubmissions1.FirstOrDefault() != null ?
                            sub.usersubmissions1.FirstOrDefault().user.lastName : (sub.usersubmissions.FirstOrDefault() == null ? null : sub.usersubmissions.FirstOrDefault().user.lastName),
                            submitterEmail = sub.usersubmissions1.FirstOrDefault() != null ?
                            sub.usersubmissions1.FirstOrDefault().user.membership.email : (sub.usersubmissions.FirstOrDefault() == null ? null : sub.usersubmissions.FirstOrDefault().user.membership.email),
                            submissionTitle = sub.title,
                            topic = sub.topiccategory.name,
                            topiccategoryID = sub.topiccategory.topiccategoryID,
                            submissionAbstract = sub.submissionAbstract,
                            submissionFileList = sub.documentssubmitteds.Where(u => u.deleted == false).
                                Select(c => new SubmissionDocument
                                {
                                    documentssubmittedID = c.documentssubmittedID,
                                    submissionID = c.submissionID,
                                    documentName = c.documentName,
                                    //document = c.document,
                                    deleted = c.deleted
                                }).ToList(),
                            submissionType = sub.submissiontype.name,
                            submissionTypeID = sub.submissionTypeID,
                            templateName = sub.templatesubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            null : sub.templatesubmissions.Where(c => c.deleted == false).FirstOrDefault().template.name,
                            templateID = sub.templatesubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            -1 : sub.templatesubmissions.Where(c => c.deleted == false).FirstOrDefault().templateID,
                            panelistNames = null,
                            plan = (sub.workshops.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.workshops.Where(y => y.deleted == false).FirstOrDefault().plan),
                            guideQuestions = null,
                            format = null,
                            equipment = (sub.workshops.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.workshops.Where(y => y.deleted == false).FirstOrDefault().necessary_equipment),
                            duration = (sub.workshops.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.workshops.Where(y => y.deleted == false).FirstOrDefault().duration),
                            delivery = (sub.workshops.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.workshops.Where(y => y.deleted == false).FirstOrDefault().delivery),
                            subIsEvaluated = (sub.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : sub.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault().statusEvaluation) == "Evaluated" ? true : false,
                            publicFeedback = (sub.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                             null : sub.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault().evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault()) == null ?
                             null : sub.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault().evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault().publicFeedback,

                            //previous
                            hasPrevVersion = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? false : true,
                            prevSubmissionID = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? -1 : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.submissionID,
                            prevSubmissionTitle = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.title,
                            prevTopic = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.topiccategory.name,
                            prevSubmissionAbstract = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.submissionAbstract,
                            prevSubmissionFileList = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.documentssubmitteds.Where(u => u.deleted == false).
                                Select(c => new SubmissionDocument
                                {
                                    documentssubmittedID = c.documentssubmittedID,
                                    submissionID = c.submissionID,
                                    documentName = c.documentName,
                                    //document = c.document,
                                    deleted = c.deleted
                                }).ToList(),
                            prevSubmissionType = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.submissiontype.name,
                            prevPanelistNames = null,
                            prevPlan = (sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.workshops.Where(y => y.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.workshops.Where(y => y.deleted == false).FirstOrDefault().plan),
                            prevGuideQuestions = null,
                            prevFormat = null,
                            prevEquipment = (sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.workshops.Where(y => y.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.workshops.Where(y => y.deleted == false).FirstOrDefault().necessary_equipment),
                            prevDuration = (sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.workshops.Where(y => y.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.workshops.Where(y => y.deleted == false).FirstOrDefault().duration),
                            prevDelivery = (sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.workshops.Where(y => y.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.workshops.Where(y => y.deleted == false).FirstOrDefault().delivery),
                            prevSubIsEvaluated = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            false : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            false : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault().statusEvaluation == "Evaluated" ? true : false,
                            prevPublicFeedback = ((sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1 == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault()) == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault().evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault()) == null ?
                            null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().submission1.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault().evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault().publicFeedback

                        };
                        /*if (isFinalVersion)
                        {
                            subs.submitterFirstName = context.usersubmission.Where(c => c.finalSubmissionID == submissionID).FirstOrDefault() == null ?
                                null : context.usersubmission.Where(c => c.finalSubmissionID == submissionID).FirstOrDefault().user.firstName; 
                        }
                        else
                        {
                            subs.submitterFirstName = context.usersubmission.Where(c => c.initialSubmissionID == submissionID).FirstOrDefault() == null ?
                                null : context.usersubmission.Where(c => c.initialSubmissionID == submissionID).FirstOrDefault().user.firstName; 
                        }*/
                    }
                    return subs;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.getSubmission error " + ex);
                return null;
            }
        }



        public List<SubmissionType> getSubmissionTypes()
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //gets all the submissions uploaded by the user currently logged in
                    List<SubmissionType> userSubmissions = context.submissiontypes.
                        Select(i => new SubmissionType
                        {
                            submissionTypeID = i.submissiontypeID,
                            submissionTypeName = i.name
                        }).ToList();
                    return userSubmissions;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.getSubmissionTypes error " + ex);
                return null;
            }
        }

        public Submission deleteSubmission(long submissionID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    Submission prevSub = null;
                    submission sub = context.submissions.Where(c => c.submissionID == submissionID).FirstOrDefault();
                    bool isFinalVersion = context.usersubmission.Where(c => c.deleted == false && c.finalSubmissionID == submissionID).FirstOrDefault() == null ? false : true;
                    if (isFinalVersion)
                    {
                        prevSub = context.usersubmission.Where(c => c.finalSubmissionID == submissionID).
                        Select(i => new Submission
                        {
                            submissionID = i.submission1 == null ? -1 : i.submission1.submissionID,
                            submissionTypeName = i.submission1 == null ? null : i.submission1.submissiontype.name,
                            submissionTypeID = i.submission1 == null ? -1 : i.submission1.submissionTypeID,
                            submissionTitle = i.submission1 == null ? null : i.submission1.title,
                            topiccategoryID = i.submission1 == null ? -1 : i.submission1.topicID,
                            status = i.submission1 == null ? null : i.submission1.status,
                            isEvaluated = (i.submission1.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : i.submission1.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault().statusEvaluation) == "Evaluated" ? true : false,
                            isAssigned = i.submission1.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? false : true,
                            isFinalSubmission = false,
                            finalSubmissionAllowed = (i.allowFinalVersion == null ? false : i.allowFinalVersion) == false ? false : true
                        }).FirstOrDefault();

                        //if submission to be deleted is final version disconnect the final version from the previous one                       
                        var theFinalSub = context.usersubmission.Where(c => c.deleted == false && c.finalSubmissionID == submissionID).FirstOrDefault();
                        theFinalSub.finalSubmissionID = null;
                    }

                    //delete pdf files
                    if (sub.documentssubmitteds != null)
                    {
                        foreach (var s in sub.documentssubmitteds)
                        {
                            s.deleted = true;
                        }
                    }
                    //delete submission
                    sub.deleted = true;
                    //delete user submissions
                    if (sub.usersubmissions.FirstOrDefault() != null && isFinalVersion == false)
                    {
                        sub.usersubmissions.FirstOrDefault().deleted = true;
                    }
                    if (sub.usersubmissions1.FirstOrDefault() != null && isFinalVersion == false)
                    {
                        sub.usersubmissions1.FirstOrDefault().deleted = true;
                    }
                    //if submission is pannel delete extra fields
                    if (sub.submissionTypeID == 3 && sub.panels != null)
                    {
                        foreach (var s in sub.panels)
                        {
                            s.deleted = true;
                        }
                    }
                    //if submission is workshop delete extra fields
                    if (sub.submissionTypeID == 5 && sub.workshops != null)
                    {
                        foreach (var s in sub.workshops)
                        {
                            s.deleted = true;
                        }
                    }
                    //if submission has an evaluator assigned (can only happen when an admin or committe evaluator deletes it)
                    List<evaluatiorsubmission> evaluatorSubmission = context.evaluatiorsubmissions.Where(c => c.submissionID == submissionID && c.deleted == false).ToList();
                    foreach (evaluatiorsubmission evalSub in evaluatorSubmission)
                    {
                        evalSub.deleted = true;
                    }

                    context.SaveChanges();

                    return prevSub;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.deleteSubmission error " + ex);
                return null;
            }
        }

        //este metodo no toma en consideracion cuando un admin sube un submission!
        public Submission addSubmission(usersubmission usersubTA, submission submissionToAdd, panel pannelToAdd, workshop workshopToAdd)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    submission sub = new submission();
                    //for all types of submissions
                    //table submission
                    sub.topicID = submissionToAdd.topicID;
                    sub.submissionTypeID = submissionToAdd.submissionTypeID;
                    sub.submissionAbstract = submissionToAdd.submissionAbstract;
                    sub.title = submissionToAdd.title;
                    sub.status = "Pending";
                    sub.creationDate = DateTime.Now;
                    sub.deleted = false;
                    sub.byAdmin = false;
                    context.submissions.Add(sub);
                    context.SaveChanges();
                    //table usersubmission
                    long submissionID = sub.submissionID;
                    usersubmission usersub = new usersubmission();
                    usersub.userID = usersubTA.userID;
                    usersub.initialSubmissionID = submissionID;
                    usersub.allowFinalVersion = false;
                    usersub.deleted = false;
                    usersub.finalSubmissionID = null;
                    context.usersubmission.Add(usersub);
                    context.SaveChanges();
                    //table documents submitted
                    if (submissionToAdd.submissionTypeID != 4)
                    {
                        documentssubmitted subDocs = new documentssubmitted();

                        foreach (var doc in submissionToAdd.documentssubmitteds)
                        {
                            subDocs.submissionID = submissionID;
                            subDocs.documentName = doc.documentName;
                            subDocs.document = doc.document;
                            subDocs.deleted = false;
                            context.documentssubmitteds.Add(subDocs);
                            context.SaveChanges();
                        }

                    }
                    //table pannels
                    if (submissionToAdd.submissionTypeID == 3 && pannelToAdd != null)
                    {
                        panel subPanel = new panel();
                        subPanel.submissionID = submissionID;
                        subPanel.panelistNames = pannelToAdd.panelistNames;
                        subPanel.plan = pannelToAdd.plan;
                        subPanel.guideQuestion = pannelToAdd.guideQuestion;
                        subPanel.formatDescription = pannelToAdd.formatDescription;
                        subPanel.necessaryEquipment = pannelToAdd.necessaryEquipment;
                        subPanel.deleted = false;
                        context.panels.Add(subPanel);
                        context.SaveChanges();
                    }
                    //table workshop
                    if (submissionToAdd.submissionTypeID == 5 && workshopToAdd != null)
                    {
                        workshop subWorkshop = new workshop();
                        subWorkshop.submissionID = submissionID;
                        subWorkshop.duration = workshopToAdd.duration;
                        subWorkshop.delivery = workshopToAdd.delivery;
                        subWorkshop.plan = workshopToAdd.plan;
                        subWorkshop.necessary_equipment = workshopToAdd.necessary_equipment;
                        subWorkshop.deleted = false;
                        context.workshops.Add(subWorkshop);
                        context.SaveChanges();
                    }

                    Submission addedSub = new Submission
                    {
                        submissionID = submissionID,
                        submissionTypeName = getSubmissionTypeName(sub.submissionTypeID),
                        submissionTypeID = sub.submissionTypeID,
                        submissionTitle = sub.title,
                        topiccategoryID = sub.topicID,
                        status = sub.status,
                        isEvaluated = false,
                        isFinalSubmission = false
                    };
                    return addedSub;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.addSubmission error " + ex);
                return null;
            }
        }

        public bool addSubmissionFile(documentssubmitted file)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {                   
                        documentssubmitted subDoc = new documentssubmitted();

                        subDoc.submissionID = file.submissionID;
                        subDoc.documentName = file.documentName;
                        subDoc.document = file.document;
                        subDoc.deleted = false;
                        context.documentssubmitteds.Add(subDoc);
                        context.SaveChanges();                                
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.addSubmission error " + ex);
                return false;
            }
        }

        public bool manageExistingFiles(long subID, List<long> existingDocsID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //all documents in DB for submission with ID SubmissionID
                    List<documentssubmitted> prevDocuments = context.documentssubmitteds.Where(d => d.submissionID == subID).ToList<documentssubmitted>();
                    //list of all documents that are in the DB and will not be removed from the submission
                    List<documentssubmitted> existingDocs = prevDocuments.Where(c => existingDocsID.Contains(c.documentssubmittedID)).ToList();
                    //list of all the documents that used to belong to the submission but where deleted by the user
                    List<documentssubmitted> docsInDBtbd = prevDocuments.Except(existingDocs).ToList();
                    //remove from the DB all items delete by the user
                    foreach (var docTBD in docsInDBtbd)
                    {
                        context.documentssubmitteds.Remove(docTBD);
                    }
                    context.SaveChanges();                   
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.addSubmission error " + ex);
                return false;
            }
        }

        public Submission editSubmission(submission submissionToEdit, panel pannelToEdit, workshop workshopToEdit)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {

                    submission sub = context.submissions.Where(c => c.submissionID == submissionToEdit.submissionID).FirstOrDefault();
                    //for all types of submissions
                    //table submission
                    sub.topicID = submissionToEdit.topicID;
                    sub.submissionAbstract = submissionToEdit.submissionAbstract;
                    sub.title = submissionToEdit.title;
                    context.SaveChanges();
                    //table pannels
                    if (sub.submissionTypeID == 3 && pannelToEdit != null)
                    {
                        panel subPanel = context.panels.Where(c => c.submissionID == sub.submissionID).FirstOrDefault();
                        subPanel.panelistNames = pannelToEdit.panelistNames;
                        subPanel.plan = pannelToEdit.plan;
                        subPanel.guideQuestion = pannelToEdit.guideQuestion;
                        subPanel.formatDescription = pannelToEdit.formatDescription;
                        subPanel.necessaryEquipment = pannelToEdit.necessaryEquipment;
                        context.SaveChanges();
                    }
                    //table workshop
                    if (sub.submissionTypeID == 5 && workshopToEdit != null)
                    {
                        workshop subWorkshop = context.workshops.Where(c => c.submissionID == sub.submissionID).FirstOrDefault();
                        subWorkshop.duration = workshopToEdit.duration;
                        subWorkshop.delivery = workshopToEdit.delivery;
                        subWorkshop.plan = workshopToEdit.plan;
                        subWorkshop.necessary_equipment = workshopToEdit.necessary_equipment;
                        context.SaveChanges();
                    }
                    Submission editedSub = new Submission
                    {
                        submissionID = sub.submissionID,
                        submissionTypeName = getSubmissionTypeName(sub.submissionTypeID),
                        submissionTypeID = sub.submissionTypeID,
                        submissionTitle = sub.title,
                        topiccategoryID = sub.topicID
                    };


                    if (submissionToEdit.submissionTypeID != 4)
                    {
                        //all documents in DB for submission with ID SubmissionID
                        List<documentssubmitted> prevDocuments = context.documentssubmitteds.Where(d => d.submissionID == sub.submissionID).ToList<documentssubmitted>();
                        //list of all new documents that are being added to the submission
                        List<documentssubmitted> addedDocs = submissionToEdit.documentssubmitteds.Where(c => c.document != null ).ToList();
                        //list of IDs of all documents that are in the DB and will not be removed from the submission
                        List <long> remainingDocsID = submissionToEdit.documentssubmitteds.Where(c => c.document == null).Select(d => d.documentssubmittedID).ToList();
                        //list of all documents that are in the DB and will not be removed from the submission
                        List<documentssubmitted> remainingDocs = prevDocuments.Where(c => remainingDocsID.Contains(c.documentssubmittedID)).ToList();
                        //list of all the documents that used to belong to the submission but where deleted by the user
                        List<documentssubmitted> docsInDBtbd = prevDocuments.Except(remainingDocs).ToList();
                        //remove from the DB all items delete by the user
                        foreach (var docTBD in docsInDBtbd)
                        {
                            context.documentssubmitteds.Remove(docTBD);
                        }
                        context.SaveChanges();

                        //add to the DB all new documents added to the submission
                        documentssubmitted subDocs = new documentssubmitted();
                        foreach (var docs in addedDocs)
                        {
                            subDocs.submissionID = sub.submissionID;
                            subDocs.documentName = docs.documentName;
                            subDocs.document = docs.document;
                            subDocs.deleted = false;
                            context.documentssubmitteds.Add(subDocs);
                            context.SaveChanges();
                        }
                    }
                    return editedSub;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.addEvaluation error " + ex);
                return null;
            }
        }

        public String getSubmissionTypeName(long submissionTypeID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    String submissionTypeName = context.submissiontypes.Where(c => c.submissiontypeID == submissionTypeID).Select(i => i.name).FirstOrDefault();
                    return submissionTypeName;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.editSubmission error " + ex);
                return null;
            }
        }

        public SubmissionPagingQuery getAllSubmissions(int index)
        {
            SubmissionPagingQuery page = new SubmissionPagingQuery();
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    int pageSize = 10;
                    int? scoreSum = 0;
                    int evalCount = 0;
                    double avgScore = 0.00;
                    int numOfEvaluations = 0;
                    //get all final submissions.
                    List<Submission> userSubmissions = new List<Submission>();
                    List<usersubmission> subList = context.usersubmission.Where(c => c.deleted == false && c.finalSubmissionID != null).ToList();
                    foreach (var sub in subList)
                    {
                        long userID = sub.userID;
                        long submissionID = sub.submission == null ? -1 : sub.submission.submissionID;
                        String submissionTypeName = sub.submission == null ? null : sub.submission.submissiontype == null ? null : sub.submission.submissiontype.name;
                        int submissionTypeID = sub.submission == null ? -1 : sub.submission.submissionTypeID;
                        String submissionTitle = sub.submission == null ? null : sub.submission.title;
                        int topiccategoryID = sub.submission == null ? -1 : sub.submission.topicID;
                        String topic = sub.submission == null ? null : sub.submission.topiccategory == null ? null : sub.submission.topiccategory.name;
                        String status = sub.submission == null ? null : sub.submission.status;
                        bool byAdmin = sub.submission == null ? false : sub.submission.byAdmin == true ? true : false;
                        IEnumerable<IGrouping<long, evaluatiorsubmission>> groupBy = sub.submission == null ? null : sub.submission.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                           null : sub.submission.evaluatiorsubmissions.Where(c => c.deleted == false).GroupBy(s => s.submissionID).ToList();
                        if (groupBy != null)
                        {
                            foreach (var subGroup in groupBy)//goes through all groups of sub/evalsub
                            {
                                foreach (var evalsForSub in subGroup)//goes through all evaluatiorsubmission for each submission
                                {
                                    int? thisScore = evalsForSub.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault() == null ?
                                        -1 : evalsForSub.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault().score;
                                    if (thisScore != -1)//if submission has been evaluated
                                    {
                                        scoreSum = scoreSum + thisScore;
                                        evalCount++;
                                    }
                                }
                                avgScore = evalCount == 0 ? 0.00 : (double)scoreSum / evalCount;
                                numOfEvaluations = evalCount;
                                scoreSum = 0;
                                evalCount = 0;
                            }
                            userSubmissions.Add(new Submission(userID, submissionID, submissionTypeName,
                            submissionTypeID, submissionTitle, topiccategoryID, topic, status, avgScore, numOfEvaluations, byAdmin));
                        }
                        else
                        {
                            userSubmissions.Add(new Submission(userID, submissionID, submissionTypeName,
                            submissionTypeID, submissionTitle, topiccategoryID, topic, status, 0, numOfEvaluations, byAdmin));
                        }
                    }
                    scoreSum = 0;
                    evalCount = 0;
                    avgScore = 0.00;
                    numOfEvaluations = 0;
                    //get all submissions that do not have a final submission
                    List<usersubmission> subList2 = context.usersubmission.Where(c => c.deleted == false && c.finalSubmissionID == null).ToList();
                    foreach (var sub in subList2)
                    {
                        long userID = sub.userID;
                        long submissionID = sub.submission1 == null ? -1 : sub.submission1.submissionID;
                        String submissionTypeName = sub.submission1 == null ? null : sub.submission1.submissiontype == null ? null : sub.submission1.submissiontype.name;
                        int submissionTypeID = sub.submission1 == null ? -1 : sub.submission1.submissionTypeID;
                        String submissionTitle = sub.submission1.title;
                        int topiccategoryID = sub.submission1 == null ? -1 : sub.submission1.topicID;
                        String topic = sub.submission1 == null ? null : sub.submission1.topiccategory == null ? null : sub.submission1.topiccategory.name;
                        String status = sub.submission1 == null ? null : sub.submission1.status;
                        bool byAdmin = sub.submission1 == null ? false : sub.submission1.byAdmin == true ? true : false;
                        IEnumerable<IGrouping<long, evaluatiorsubmission>> groupBy = sub.submission1 == null ? null : sub.submission1.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                                null : sub.submission1.evaluatiorsubmissions.Where(c => c.deleted == false).GroupBy(s => s.submissionID).ToList();
                        if (groupBy != null)
                        {
                            foreach (var subGroup in groupBy)//goes through all groups of sub/evalsub
                            {
                                foreach (var evalsForSub in subGroup)//goes through all evaluatiorsubmission for each submission
                                {
                                    int? thisScore = evalsForSub.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault() == null ?
                                        -1 : evalsForSub.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault().score;
                                    if (thisScore != -1)//if submission has been evaluated
                                    {
                                        scoreSum = scoreSum + thisScore;
                                        evalCount++;
                                    }
                                }
                                avgScore = evalCount == 0 ? 0.00 : (double)scoreSum / evalCount;
                                numOfEvaluations = evalCount;
                                scoreSum = 0;
                                evalCount = 0;
                            }
                            userSubmissions.Add(new Submission(userID, submissionID, submissionTypeName,
                            submissionTypeID, submissionTitle, topiccategoryID, topic, status, avgScore, numOfEvaluations, byAdmin));
                        }
                        else
                        {
                            userSubmissions.Add(new Submission(userID, submissionID, submissionTypeName,
                            submissionTypeID, submissionTitle, topiccategoryID, topic, status, 0, numOfEvaluations, byAdmin));
                        }
                    }
                    userSubmissions = userSubmissions.OrderBy(c => -c.avgScore).ToList();
                    page.rowCount = userSubmissions.Count();
                    if (page.rowCount > 0)
                    {
                        page.maxIndex = (int)Math.Ceiling(page.rowCount / (double)pageSize);
                        var allUserSubmissions = userSubmissions.Skip(pageSize * index).Take(pageSize).ToList(); //Skip past rows and take new elements
                        page.results = allUserSubmissions;
                    }

                    return page;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.getAllSubmissions error " + ex);
                return null;
            }
        }


        public Submission addFinalSubmission(usersubmission usersubTA, submission submissionToAdd, documentssubmitted submissionDocuments, panel pannelToAdd, workshop workshopToAdd)
        {
            try
            {
                String email = "";
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    submission sub = new submission();
                    //for all types of submissions
                    //table submission
                    sub.topicID = submissionToAdd.topicID;
                    sub.submissionTypeID = submissionToAdd.submissionTypeID;
                    sub.submissionAbstract = submissionToAdd.submissionAbstract;
                    sub.title = submissionToAdd.title;
                    sub.status = "Pending";
                    sub.creationDate = DateTime.Now;
                    sub.deleted = false;
                    sub.byAdmin = false;
                    context.submissions.Add(sub);
                    context.SaveChanges();
                    //table usersubmission
                    long finalSubmissionID = sub.submissionID;
                    usersubmission usersub = context.usersubmission.Where(c => c.initialSubmissionID == usersubTA.initialSubmissionID && c.deleted == false).FirstOrDefault();
                    usersub.finalSubmissionID = finalSubmissionID;
                    context.SaveChanges();
                    
                    //list of all the files that the prev submission had
                    List<documentssubmitted> prevDocuments = context.documentssubmitteds.Where(c => c.submissionID == usersubTA.initialSubmissionID).ToList();
                    //list of all the documents of the remaining docs-the docs that are in the prev that will stay in the final
                    List<long> remainingDocsID = submissionToAdd.documentssubmitteds.Where(c => c.document == null).Select(d => d.documentssubmittedID).ToList();
                    //list of all documents that are in the DB and will not be removed from the submission
                    List<documentssubmitted> remainingDocs = prevDocuments.Where(c => remainingDocsID.Contains(c.documentssubmittedID)).ToList();
                    documentssubmitted doc = new documentssubmitted();
                    foreach (var oldDocs in remainingDocs)
                    {
                        doc.submissionID = sub.submissionID;
                        doc.documentName = oldDocs.documentName;
                        doc.document = oldDocs.document;
                        doc.deleted = false;
                        context.documentssubmitteds.Add(doc);
                        context.SaveChanges();
                    }    

                    //replace every document bound to the submission
                    documentssubmitted subDocs = new documentssubmitted();
                    //the new documents to be submitted
                    List<documentssubmitted> newDocs = submissionToAdd.documentssubmitteds.Where(c => c.document != null).ToList();
                    foreach (var docs in newDocs)
                    {
                        subDocs.submissionID = sub.submissionID;
                        subDocs.documentName = docs.documentName;
                        subDocs.document = docs.document;
                        subDocs.deleted = false;
                        context.documentssubmitteds.Add(subDocs);
                        context.SaveChanges();
                    }
                    //table pannels
                    if (submissionToAdd.submissionTypeID == 3 && pannelToAdd != null)
                    {
                        panel subPanel = new panel();
                        subPanel.submissionID = finalSubmissionID;
                        subPanel.panelistNames = pannelToAdd.panelistNames;
                        subPanel.plan = pannelToAdd.plan;
                        subPanel.guideQuestion = pannelToAdd.guideQuestion;
                        subPanel.formatDescription = pannelToAdd.formatDescription;
                        subPanel.necessaryEquipment = pannelToAdd.necessaryEquipment;
                        subPanel.deleted = false;
                        context.panels.Add(subPanel);
                        context.SaveChanges();
                    }
                    //table workshop
                    if (submissionToAdd.submissionTypeID == 5 && workshopToAdd != null)
                    {
                        workshop subWorkshop = new workshop();
                        subWorkshop.submissionID = finalSubmissionID;
                        subWorkshop.duration = workshopToAdd.duration;
                        subWorkshop.delivery = workshopToAdd.delivery;
                        subWorkshop.plan = workshopToAdd.plan;
                        subWorkshop.necessary_equipment = workshopToAdd.necessary_equipment;
                        subWorkshop.deleted = false;
                        context.workshops.Add(subWorkshop);
                        context.SaveChanges();
                    }

                    //Delete connection between previous submissions and evaluators that have not evaluated them yet, 
                    //since these will not be taken into consideration for the avg score of the final submission
                    List<evaluatiorsubmission> TBD = context.evaluatiorsubmissions.Where(c => c.submissionID == usersubTA.initialSubmissionID && c.statusEvaluation != "Evaluated" && c.deleted == false).ToList();
                    foreach (var assignment in TBD)
                    {
                        //quitando el assignment de submission y evaluator del intial submission a los q aun no han evaluado, se les envia un email al evaluador de que no esta asignado.
                        assignment.deleted = true;

                        try {
                            email = assignment.evaluator.user.membership.email;//evaluator email
                            String subject = "Caribbean Celebration of Women in Computing Assignment Deletion";
                            String messageBody = "Greetings, \n\n " +
                                "The request to evaluate the submission with title " + assignment.submission.title + " has been removed. It is no longer required for you to evaluate said submission.  To view all of your assigned submission please login to the system through the following link: \n\n" +
                                "http://136.145.116.238/#/Login/Log" + ".";                            
                            sendAssignmentEmail(email, subject, messageBody); //inform evaluator of deleted assignment via email
                        }

                        catch (Exception ex)
                        {
                            Console.Write("SubmissionManager.addEvaluation error " + ex);
                            return null;
                        }

                    }
                    context.SaveChanges();
                    

                    Submission addedSub = new Submission
                    {
                        submissionID = finalSubmissionID,
                        submissionTypeName = getSubmissionTypeName(sub.submissionTypeID),
                        submissionTypeID = sub.submissionTypeID,
                        submissionTitle = sub.title,
                        topiccategoryID = sub.topicID,
                        status = sub.status,
                        isEvaluated = false,
                        isFinalSubmission = true
                    };
                    return addedSub;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.addSubmission error " + ex);
                return null;
            }
        }

        //Send email to evaluator when an assignment to a submission was removed
        private void sendAssignmentEmail(string email, String subject, String messageBody)
        {
            MailAddress ccwic = new MailAddress(ccwicEmail);
            MailAddress user = new MailAddress(email);
            MailMessage mail = new System.Net.Mail.MailMessage(ccwic, user);

            mail.Subject = subject;
            mail.Body = messageBody;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            smtp.Credentials = new NetworkCredential(
                ccwicEmail, ccwicEmailPass);
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }

        public Submission postAdminFinalSubmission(usersubmission usersubTA, submission submissionToAdd, documentssubmitted submissionDocuments, panel pannelToAdd, workshop workshopToAdd)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    submission sub = new submission();
                    //for all types of submissions
                    //table submission
                    sub.topicID = submissionToAdd.topicID;
                    sub.submissionTypeID = submissionToAdd.submissionTypeID;
                    sub.submissionAbstract = submissionToAdd.submissionAbstract;
                    sub.title = submissionToAdd.title;
                    sub.status = "Pending";
                    sub.creationDate = DateTime.Now;
                    sub.deleted = false;
                    sub.byAdmin = true;
                    context.submissions.Add(sub);
                    context.SaveChanges();
                    //table usersubmission
                    long finalSubmissionID = sub.submissionID;
                    usersubmission usersub = context.usersubmission.Where(c => c.initialSubmissionID == usersubTA.initialSubmissionID && c.deleted == false).FirstOrDefault();
                    usersub.finalSubmissionID = finalSubmissionID;
                    context.SaveChanges();

                    //list of all the files that the prev submission had
                    List<documentssubmitted> prevDocuments = context.documentssubmitteds.Where(c => c.submissionID == usersubTA.initialSubmissionID).ToList();
                    //list of all the documents of the remaining docs-the docs that are in the prev that will stay in the final
                    List<long> remainingDocsID = submissionToAdd.documentssubmitteds.Where(c => c.document == null).Select(d => d.documentssubmittedID).ToList();
                    //list of all documents that are in the DB and will not be removed from the submission
                    List<documentssubmitted> remainingDocs = prevDocuments.Where(c => remainingDocsID.Contains(c.documentssubmittedID)).ToList();
                    documentssubmitted doc = new documentssubmitted();
                    foreach (var oldDocs in remainingDocs)
                    {
                        doc.submissionID = sub.submissionID;
                        doc.documentName = oldDocs.documentName;
                        doc.document = oldDocs.document;
                        doc.deleted = false;
                        context.documentssubmitteds.Add(doc);
                        context.SaveChanges();
                    }

                    //replace every document bound to the submission
                    documentssubmitted subDocs = new documentssubmitted();
                    //the new documents to be submitted
                    List<documentssubmitted> newDocs = submissionToAdd.documentssubmitteds.Where(c => c.document != null).ToList();
                    foreach (var docs in newDocs)
                    {
                        subDocs.submissionID = sub.submissionID;
                        subDocs.documentName = docs.documentName;
                        subDocs.document = docs.document;
                        subDocs.deleted = false;
                        context.documentssubmitteds.Add(subDocs);
                        context.SaveChanges();
                    }

                    //table pannels
                    if (submissionToAdd.submissionTypeID == 3 && pannelToAdd != null)
                    {
                        panel subPanel = new panel();
                        subPanel.submissionID = finalSubmissionID;
                        subPanel.panelistNames = pannelToAdd.panelistNames;
                        subPanel.plan = pannelToAdd.plan;
                        subPanel.guideQuestion = pannelToAdd.guideQuestion;
                        subPanel.formatDescription = pannelToAdd.formatDescription;
                        subPanel.necessaryEquipment = pannelToAdd.necessaryEquipment;
                        subPanel.deleted = false;
                        context.panels.Add(subPanel);
                        context.SaveChanges();
                    }
                    //table workshop
                    if (submissionToAdd.submissionTypeID == 5 && workshopToAdd != null)
                    {
                        workshop subWorkshop = new workshop();
                        subWorkshop.submissionID = finalSubmissionID;
                        subWorkshop.duration = workshopToAdd.duration;
                        subWorkshop.delivery = workshopToAdd.delivery;
                        subWorkshop.plan = workshopToAdd.plan;
                        subWorkshop.necessary_equipment = workshopToAdd.necessary_equipment;
                        subWorkshop.deleted = false;
                        context.workshops.Add(subWorkshop);
                        context.SaveChanges();
                    }

                    //Delete connection between previous submissions and evaluators that have not evaluated them yet, 
                    //since these will not be taken into consideration for the avg score of the final submission
                    List<evaluatiorsubmission> TBD = context.evaluatiorsubmissions.Where(c => c.submissionID == usersubTA.initialSubmissionID && c.statusEvaluation != "Evaluated" && c.deleted == false).ToList();
                    foreach (var assignment in TBD)
                    {
                        assignment.deleted = true;
                    }
                    context.SaveChanges();

                    Submission addedSub = new Submission
                    {
                        submissionID = finalSubmissionID,
                        submissionTypeName = getSubmissionTypeName(sub.submissionTypeID),
                        submissionTypeID = sub.submissionTypeID,
                        submissionTitle = sub.title,
                        topiccategoryID = sub.topicID,
                        status = sub.status,
                        isEvaluated = false,
                        isFinalSubmission = true
                    };
                    return addedSub;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.addSubmission error " + ex);
                return null;
            }
        }

        public List<Evaluation> getSubmissionEvaluations(long submissionID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    List<Evaluation> subEvals = new List<Evaluation>();
                    List<Evaluation> prevSubEvals = new List<Evaluation>();
                    //Checking if submission has a previous version:
                    long initialSubmissionID = context.usersubmission.Where(c => c.finalSubmissionID == submissionID && c.deleted == false) == null ?
                        -1 : context.usersubmission.Where(c => c.finalSubmissionID == submissionID && c.deleted == false).Select(d => d.initialSubmissionID).FirstOrDefault();
                    if (initialSubmissionID > -1)//if submissionID belong to a submission that does has a previous version
                    {
                        //get initial submission evaluation
                        prevSubEvals = getEvaluations(initialSubmissionID) == null ?
                            new List<Evaluation>() : getEvaluations(initialSubmissionID);
                        //get final submission evaluation
                        subEvals = getEvaluations(submissionID) == null ?
                            new List<Evaluation>() : getEvaluations(submissionID);
                    }
                    else
                    {
                        //get only submission evaluation
                        subEvals = getEvaluations(submissionID) == null ?
                            new List<Evaluation>() : getEvaluations(submissionID);
                    }
                    if (prevSubEvals.Count > 0)
                    {
                        foreach (var eval in prevSubEvals)
                        {
                            eval.isPrevSub = true;
                            subEvals.Add(eval);
                        }
                    }
                    return subEvals;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.getSubmissionEvaluations error " + ex);
                return null;
            }
        }
        //for admin
        public List<Evaluation> getEvaluations(long submissionID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {

                    //getting the evaluation
                    List<evaluatiorsubmission> evalSubmissionsList = context.evaluatiorsubmissions.Where(c => c.submissionID == submissionID && c.deleted == false).ToList();
                    Evaluation eval;
                    List<Evaluation> submissionEvaluations = new List<Evaluation>();
                    foreach (var evalSub in evalSubmissionsList)
                    {
                        if (evalSub.evaluationsubmitteds.FirstOrDefault() != null)
                        {
                            eval = evalSub.evaluationsubmitteds.Select(c => new Evaluation
                            {
                                submissionID = c.evaluatiorsubmission.submissionID,
                                evaluatorID = c.evaluatiorsubmission.evaluatorID,
                                evaluatorSubmissionID = c.evaluatiorsubmission.evaluationsubmissionID,
                                evaluatorFirstName = c.evaluatiorsubmission.evaluator.user.firstName,
                                evaluatorLastName = c.evaluatiorsubmission.evaluator.user.lastName,
                                score = c.score,
                                publicFeedback = c.publicFeedback,
                                privateFeedback = c.privateFeedback,
                                evaluationFile = c.evaluationFile,
                                evaluationFileName = c.evaluationName,
                                evaluationStatus = c.evaluatiorsubmission.statusEvaluation,
                                isPrevSub = false

                            }).FirstOrDefault();

                            submissionEvaluations.Add(eval);
                        }
                        else
                        {
                            eval = new Evaluation
                            {
                                submissionID = evalSub.submissionID,
                                evaluatorID = evalSub.evaluatorID,
                                evaluatorSubmissionID = evalSub.evaluationsubmissionID,
                                evaluatorFirstName = evalSub.evaluator.user.firstName,
                                evaluatorLastName = evalSub.evaluator.user.lastName,
                                score = 0,
                                publicFeedback = null,
                                privateFeedback = null,
                                evaluationFile = null,
                                evaluationFileName = null,
                                evaluationStatus = evalSub.statusEvaluation
                            };
                            submissionEvaluations.Add(eval);
                        }
                    }

                    return submissionEvaluations;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.getSubmissionEvaluations error " + ex);
                return null;
            }
        }

        public bool getSubmissionDeadline()
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    String submissionDeadlineString = context.interfaceinformations.Where(c => c.attribute == "submissionDeadline").Select(d => d.content).FirstOrDefault();
                    var Day = Convert.ToInt32(submissionDeadlineString.Split('/')[1]);
                    var Month = Convert.ToInt32(submissionDeadlineString.Split('/')[0]);
                    var Year = Convert.ToInt32(submissionDeadlineString.Split('/')[2]);

                    DateTime submissionDeadline = new DateTime(Year, Month, Day);
                    return (DateTime.Compare(submissionDeadline, DateTime.Now.Date) >= 0);
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.getSubmissionDeadline error " + ex);
                return false;
            }
        }

        public List<EvaluatorQuery> getAcceptedEvaluators()
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var evaluators = context.users.Where(evaluator => evaluator.evaluatorStatus == "Accepted" && evaluator.deleted == false).
                        Select(evaluator => new EvaluatorQuery
                    {
                        userID = (long)evaluator.userID,
                        evaluatorID = evaluator.evaluators.Where(c => c.deleted == false).FirstOrDefault() == null ? -1 : evaluator.evaluators.Where(c => c.deleted == false).FirstOrDefault().evaluatorsID,
                        firstName = evaluator.firstName,
                        lastName = evaluator.lastName,
                        email = evaluator.membership.email,
                        acceptanceStatus = evaluator.evaluatorStatus

                    }).ToList();
                    return evaluators;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.getAcceptedEvaluators error " + ex);
                return null;
            }
        }

        public Evaluation assignEvaluator(long submissionID, long evaluatorID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //assigning evaluator to a submission
                    evaluatiorsubmission relation = new evaluatiorsubmission();
                    relation.evaluatorID = evaluatorID;
                    relation.submissionID = submissionID;
                    relation.statusEvaluation = "Pending";
                    relation.deleted = false;
                    context.evaluatiorsubmissions.Add(relation);
                    context.SaveChanges();

                    Evaluation addedRelation = new Evaluation();
                    addedRelation.submissionID = submissionID;
                    addedRelation.evaluatorID = evaluatorID;
                    addedRelation.evaluatorFirstName = context.evaluators.Where(c => c.deleted == false).FirstOrDefault(c => c.evaluatorsID == evaluatorID).user.firstName;
                    addedRelation.evaluatorLastName = context.evaluators.Where(c => c.deleted == false).FirstOrDefault(c => c.evaluatorsID == evaluatorID).user.lastName;
                    addedRelation.score = 0;
                    addedRelation.evaluatorSubmissionID = context.evaluatiorsubmissions.Where(es => es.submissionID == relation.submissionID && es.evaluatorID == relation.evaluatorID && es.deleted == false).FirstOrDefault().evaluationsubmissionID;

                    //inform evaluator of assigned submission
                    try
                    {
                        string email = relation.evaluator.user.membership.email;
                        String subject = "Caribbean Celebration of Women in Computing Submission Assignment ";
                        String messageBody = "Greetings, \n\n " +
                                    "You have been requested to evaluate the submission with the title " + context.submissions.Where(c => c.submissionID == relation.submissionID).FirstOrDefault().title + ". To view and evaluate this submission please login to the system through the following link: \n\n" +
                                    "http://136.145.116.238/#/Login/Log" + ".";
                        sendAssignmentEmail(email, subject, messageBody); //inform evaluator of assignment via email
                    }
                    catch (Exception ex)
                    {
                        Console.Write("SubmissionManager.sendAssignmentEmail error " + ex);
                        return null;
                    }
                    return addedRelation;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.assignEvaluator error " + ex);
                return null;
            }
        }
        //Assigns a template to a submission
        public bool assignTemplate(long submissionID, long templateID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //asignarle un evaluation template al submission
                    bool subInTable = context.templatesubmissions.Where(c => c.submissionID == submissionID && c.deleted == false).FirstOrDefault() == null ? false : true;
                    if (subInTable)
                    {
                        templatesubmission ts = context.templatesubmissions.Where(c => c.submissionID == submissionID && c.deleted == false).FirstOrDefault();
                        ts.templateID = templateID;
                        context.SaveChanges();
                    }
                    else
                    {
                        templatesubmission templateRelation = new templatesubmission();
                        templateRelation.templateID = templateID;
                        templateRelation.submissionID = submissionID;
                        templateRelation.deleted = false;
                        context.templatesubmissions.Add(templateRelation);
                        context.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.assignEvaluator error " + ex);
                return false;
            }
        }

        //removes relation of evaluator and submissions
        public long removeEvaluatorSubmission(long evaluatorSubmissionID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    evaluatiorsubmission evalSubToRemove = context.evaluatiorsubmissions.Where(c => c.evaluationsubmissionID == evaluatorSubmissionID && c.deleted == false).FirstOrDefault();
                    evalSubToRemove.deleted = true;
                    context.SaveChanges();
                    try
                    {
                        string email = evalSubToRemove.evaluator.user.membership.email;//evaluator email
                        String subject = "Caribbean Celebration of Women in Computing Assignment Deletion";
                        String messageBody = "Greetings, \n\n " +
                            "The request to evaluate the submission with title " + evalSubToRemove.submission.title + " has been removed. It is no longer required for you to evaluate said submission.  To view all of your assigned submission please login to the system through the following link: \n\n" +
                            "http://136.145.116.238/#/Login/Log" + ".";
                        sendAssignmentEmail(email, subject, messageBody); //inform evaluator of deleted assignment via email
                    }

                    catch (Exception ex)
                    {
                        Console.Write("SubmissionManager.addEvaluation error " + ex);
                        return -1;
                    }
                    return evalSubToRemove.evaluationsubmissionID;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.removeEvaluatorSubmission error " + ex);
                return -1;
            }
        }

        public Submission changeSubmissionStatus(long submissionID, string newStatus)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    bool changedAcceptanceStatus = false;
                    submission sub = context.submissions.Where(c => c.submissionID == submissionID && c.deleted == false).FirstOrDefault();
                    sub.status = newStatus;
                    context.SaveChanges();
                    if (newStatus == "Accepted" && sub.byAdmin != true)
                    {
                        user u = sub.usersubmissions1.Where(c => c.deleted == false).FirstOrDefault() == null ? null : sub.usersubmissions1.Where(c => c.deleted == false).FirstOrDefault().user;

                        if (u == null)
                            u = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().user;

                        u.acceptanceStatus = "Accepted";
                        u.hasApplied = true;
                        context.SaveChanges();
                        changedAcceptanceStatus = true;
                    }
                    Submission subAltered = new Submission();
                    subAltered.changedAcceptanceStatus = changedAcceptanceStatus;
                    subAltered.submissionID = sub.submissionID;
                    subAltered.status = newStatus;
                    //send email
                    String email = null;
                    email = sub.usersubmissions1.Where(c => c.deleted == false).FirstOrDefault() == null ? null : sub.usersubmissions1.Where(c => c.deleted == false).FirstOrDefault().user.membership.email;
                    if (email == null) email = sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ? null : sub.usersubmissions.Where(c => c.deleted == false).FirstOrDefault().user.membership.email;
                    try { sendSubmissionUpdateEmail(email, sub.title, sub.status, changedAcceptanceStatus); }

                    catch (Exception ex)
                    {
                        Console.Write("SubmissionManager.sendSubmissionUpdateEmail error " + ex);
                        return null;
                    }

                    return subAltered;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.changeSubmissionStatus error " + ex);
                return null;
            }
        }
        //Send email when submission status has been changed
        private void sendSubmissionUpdateEmail(string email, String submissionName, String submissionStatus, bool acceptanceUpdated)
        {
            MailAddress ccwic = new MailAddress(ccwicEmail);
            MailAddress user = new MailAddress(email);
            MailMessage mail = new System.Net.Mail.MailMessage(ccwic, user);
            if (acceptanceUpdated)
            {
                mail.Subject = "Caribbean Celebration of Women in Computing Submission Update";
                mail.Body = "Greetings, \n\n " +
                    "The status for your submission with the name " + submissionName + " has been changed to " +
                    submissionStatus + ". This means that you have been accepted to assist to the Caribbean Celebration of Women in Computing. To participate, the next step is to register, you can do so by logging into your profile through the following link: \n\n"
                    + "http://136.145.116.238/#/Login/Log" + ".";
            }
            else
            {
                mail.Subject = "Caribbean Celebration of Women in Computing Submission Update";
                mail.Body = "Greetings, \n\n " +
                    "The status for your submission with the name " + submissionName + " has been changed to " + submissionStatus + ".";
            }

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            smtp.Credentials = new NetworkCredential(
                ccwicEmail, ccwicEmailPass);
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }

        //This adds the submission when it is added by an administrator
        public Submission addSubmissionByAdmin(usersubmission usersubTA, submission submissionToAdd, panel pannelToAdd, workshop workshopToAdd)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    submission sub = new submission();
                    //for all types of submissions
                    //table submission
                    sub.topicID = submissionToAdd.topicID;
                    sub.submissionTypeID = submissionToAdd.submissionTypeID;
                    sub.submissionAbstract = submissionToAdd.submissionAbstract;
                    sub.title = submissionToAdd.title;
                    sub.status = "Pending";
                    sub.creationDate = DateTime.Now;
                    sub.deleted = false;
                    sub.byAdmin = true;
                    context.submissions.Add(sub);
                    context.SaveChanges();
                    //table usersubmission
                    long submissionID = sub.submissionID;
                    usersubmission usersub = new usersubmission();
                    usersub.userID = usersubTA.userID;
                    usersub.initialSubmissionID = submissionID;
                    usersub.allowFinalVersion = false;
                    usersub.deleted = false;
                    usersub.finalSubmissionID = null;
                    context.usersubmission.Add(usersub);
                    context.SaveChanges();
                    //table documents submitted
                    if (submissionToAdd.submissionTypeID != 4)
                    {
                        documentssubmitted subDocs = new documentssubmitted();

                        foreach (var doc in submissionToAdd.documentssubmitteds)
                        {
                            subDocs.submissionID = submissionID;
                            subDocs.documentName = doc.documentName;
                            subDocs.document = doc.document;
                            subDocs.deleted = false;
                            context.documentssubmitteds.Add(subDocs);
                            context.SaveChanges();
                        }
                    }
                    //table pannels
                    if (submissionToAdd.submissionTypeID == 3 && pannelToAdd != null)
                    {
                        panel subPanel = new panel();
                        subPanel.submissionID = submissionID;
                        subPanel.panelistNames = pannelToAdd.panelistNames;
                        subPanel.plan = pannelToAdd.plan;
                        subPanel.guideQuestion = pannelToAdd.guideQuestion;
                        subPanel.formatDescription = pannelToAdd.formatDescription;
                        subPanel.necessaryEquipment = pannelToAdd.necessaryEquipment;
                        subPanel.deleted = false;
                        context.panels.Add(subPanel);
                        context.SaveChanges();
                    }
                    //table workshop
                    if (submissionToAdd.submissionTypeID == 5 && workshopToAdd != null)
                    {
                        workshop subWorkshop = new workshop();
                        subWorkshop.submissionID = submissionID;
                        subWorkshop.duration = workshopToAdd.duration;
                        subWorkshop.delivery = workshopToAdd.delivery;
                        subWorkshop.plan = workshopToAdd.plan;
                        subWorkshop.necessary_equipment = workshopToAdd.necessary_equipment;
                        subWorkshop.deleted = false;
                        context.workshops.Add(subWorkshop);
                        context.SaveChanges();
                    }

                    Submission addedSub = new Submission
                    {
                        submissionID = submissionID,
                        submissionTypeName = getSubmissionTypeName(sub.submissionTypeID),
                        submissionTypeID = sub.submissionTypeID,
                        submissionTitle = sub.title,
                        topiccategoryID = sub.topicID,
                        status = sub.status,
                        isEvaluated = false,
                        isFinalSubmission = false
                    };
                    return addedSub;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.addSubmission error " + ex);
                return null;
            }
        }

        public List<GuestList> getListOfUsers()
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var users = context.users.Where(c => c.deleted == false).Select(d =>
                        new GuestList
                        {
                            userID = d.userID,
                            firstName = d.firstName,
                            lastName = d.lastName
                        }).ToList();
                    return users;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.getListOfUsers error " + ex);
                return null;
            }
        }
        //gets all the deleted submissions
        public SubmissionPagingQuery getDeletedSubmissions(int index)
        {
            SubmissionPagingQuery page = new SubmissionPagingQuery();

            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    int pageSize = 10;
                    var subs = context.submissions.Where(c => c.deleted == true).Select(d =>
                        new Submission
                        {
                            userID = d.usersubmissions.Where(c => c.deleted == true).FirstOrDefault() == null ? -1 : d.usersubmissions.Where(c => c.deleted == true).FirstOrDefault().userID,
                            submissionID = d.submissionID,
                            submissionTypeName = d.submissiontype.name,
                            submissionTypeID = d.submissionTypeID,
                            submissionTitle = d.title,
                            topiccategoryID = d.topicID,
                            topic = d.topiccategory.name,
                            status = d.status
                        }).OrderBy(c => c.submissionTitle).ToList();

                    page.rowCount = subs.Count();
                    if (page.rowCount > 0)
                    {
                        page.maxIndex = (int)Math.Ceiling(page.rowCount / (double)pageSize);
                        var registrationPayments = subs.Skip(pageSize * index).Take(pageSize).ToList(); //Skip past rows and take new elements
                        page.results = registrationPayments;
                    }

                    return page;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.getDeletedSubmissions error " + ex);
                return null;
            }
        }

        //gets the fields of a deleted submission
        public CurrAndPrevSub getADeletedSubmission(long submissionID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    CurrAndPrevSub sub = context.submissions.Where(c => c.deleted == true && c.submissionID == submissionID).
                        Select(d => new CurrAndPrevSub
                        {
                            submissionID = d.submissionID,
                            submitterFirstName = d.usersubmissions1.FirstOrDefault() != null ?
                            d.usersubmissions1.FirstOrDefault().user.firstName : (d.usersubmissions.FirstOrDefault() == null ? null : d.usersubmissions.FirstOrDefault().user.firstName),
                            submitterLastName = d.usersubmissions1.FirstOrDefault() != null ?
                            d.usersubmissions1.FirstOrDefault().user.lastName : (d.usersubmissions.FirstOrDefault() == null ? null : d.usersubmissions.FirstOrDefault().user.lastName),
                            submitterEmail = d.usersubmissions1.FirstOrDefault() != null ?
                            d.usersubmissions1.FirstOrDefault().user.membership.email : (d.usersubmissions.FirstOrDefault() == null ? null : d.usersubmissions.FirstOrDefault().user.membership.email),
                            submissionTitle = d.title,
                            topic = d.topiccategory.name,
                            topiccategoryID = d.topiccategory.topiccategoryID,
                            submissionAbstract = d.submissionAbstract,
                            submissionFileList = d.documentssubmitteds.Where(u => u.deleted == true).
                                Select(c => new SubmissionDocument
                                {
                                    documentssubmittedID = c.documentssubmittedID,
                                    submissionID = c.submissionID,
                                    documentName = c.documentName,
                                    //document = c.document,
                                    deleted = c.deleted
                                }).ToList(),
                            submissionType = d.submissiontype.name,
                            submissionTypeID = d.submissionTypeID,
                            panelistNames = d.panels.Where(c => c.deleted == true).FirstOrDefault() == null ? null : d.panels.Where(c => c.deleted == true).FirstOrDefault().panelistNames,
                            planPanel = d.panels.Where(c => c.deleted == true).FirstOrDefault() == null ? null : d.panels.Where(c => c.deleted == true).FirstOrDefault().plan,
                            planWorkshop = d.workshops.Where(c => c.deleted == true).FirstOrDefault() == null ? null : d.workshops.Where(c => c.deleted == true).FirstOrDefault().plan,
                            guideQuestions = d.panels.Where(c => d.deleted == true).FirstOrDefault() == null ? null : d.panels.Where(c => c.deleted == true).FirstOrDefault().guideQuestion,
                            format = d.panels.Where(c => c.deleted == true).FirstOrDefault() == null ? null : d.panels.Where(c => c.deleted == true).FirstOrDefault().formatDescription,
                            equipmentPanel = d.panels.Where(c => c.deleted == true).FirstOrDefault() == null ? null : d.panels.Where(c => c.deleted == true).FirstOrDefault().necessaryEquipment,
                            equipmentWorkshop = d.workshops.Where(c => c.deleted == true).FirstOrDefault() == null ? null : d.workshops.Where(c => c.deleted == true).FirstOrDefault().necessary_equipment,
                            duration = d.workshops.Where(c => c.deleted == true).FirstOrDefault() == null ? null : d.workshops.Where(c => c.deleted == true).FirstOrDefault().duration,
                            delivery = d.workshops.Where(c => c.deleted == true).FirstOrDefault() == null ? null : d.workshops.Where(c => c.deleted == true).FirstOrDefault().delivery

                        }).FirstOrDefault();
                    return sub;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.getADeletedSubmission error " + ex);
                return null;
            }
        }

        public bool isMaster(long userID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    bool isMaster = context.claims.Where(c => c.userID == userID && c.privilegesID == 5).FirstOrDefault() == null ? false : true;
                    return isMaster;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.isMaster error " + ex);
                return false;
            }
        }

        // Search within the list with a certain criteria
        public SubmissionPagingQuery searchSubmission(int index, string criteria)
        {
            SubmissionPagingQuery page = new SubmissionPagingQuery();
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    int pageSize = 10;
                    int? scoreSum = 0;
                    int evalCount = 0;
                    double avgScore = 0.00;
                    int numOfEvaluations = 0;
                    //get all final submissions.
                    List<Submission> userSubmissions = new List<Submission>();
                    List<usersubmission> subList = context.usersubmission.Where(c => (c.submission.title.ToLower().Contains(criteria) || c.submission.topiccategory.name.ToLower().Contains(criteria) || c.submission.submissiontype.name.ToLower().Contains(criteria) || c.submission.status.ToLower().Contains(criteria)) && c.deleted == false && c.finalSubmissionID != null).ToList();
                    foreach (var sub in subList)
                    {
                        long userID = sub.userID;
                        long submissionID = sub.submission == null ? -1 : sub.submission.submissionID;
                        String submissionTypeName = sub.submission == null ? null : sub.submission.submissiontype == null ? null : sub.submission.submissiontype.name;
                        int submissionTypeID = sub.submission == null ? -1 : sub.submission.submissionTypeID;
                        String submissionTitle = sub.submission == null ? null : sub.submission.title;
                        int topiccategoryID = sub.submission == null ? -1 : sub.submission.topicID;
                        String topic = sub.submission == null ? null : sub.submission.topiccategory == null ? null : sub.submission.topiccategory.name;
                        String status = sub.submission == null ? null : sub.submission.status;
                        bool byAdmin = sub.submission == null ? false : sub.submission.byAdmin == true ? true : false;
                        IEnumerable<IGrouping<long, evaluatiorsubmission>> groupBy = sub.submission == null ? null : sub.submission.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                           null : sub.submission.evaluatiorsubmissions.Where(c => c.deleted == false).GroupBy(s => s.submissionID).ToList();
                        if (groupBy != null)
                        {
                            foreach (var subGroup in groupBy)//goes through all groups of sub/evalsub
                            {
                                foreach (var evalsForSub in subGroup)//goes through all evaluatiorsubmission for each submission
                                {
                                    int? thisScore = evalsForSub.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault() == null ?
                                        -1 : evalsForSub.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault().score;
                                    if (thisScore != -1)//if submission has been evaluated
                                    {
                                        scoreSum = scoreSum + thisScore;
                                        evalCount++;
                                    }
                                }
                                avgScore = evalCount == 0 ? 0.00 : (double)scoreSum / evalCount;
                                numOfEvaluations = evalCount;
                                scoreSum = 0;
                                evalCount = 0;
                            }
                            userSubmissions.Add(new Submission(userID, submissionID, submissionTypeName,
                            submissionTypeID, submissionTitle, topiccategoryID, topic, status, avgScore, numOfEvaluations, byAdmin));
                        }
                        else
                        {
                            userSubmissions.Add(new Submission(userID, submissionID, submissionTypeName,
                            submissionTypeID, submissionTitle, topiccategoryID, topic, status, 0, numOfEvaluations, byAdmin));
                        }
                    }
                    scoreSum = 0;
                    evalCount = 0;
                    avgScore = 0.00;
                    numOfEvaluations = 0;
                    //get all submissions that do not have a final submission
                    List<usersubmission> subList2 = context.usersubmission.Where(c => (c.submission.title.ToLower().Contains(criteria) || c.submission.topiccategory.name.ToLower().Contains(criteria) || c.submission.submissiontype.name.ToLower().Contains(criteria)) && c.deleted == false && c.finalSubmissionID == null).ToList();
                    foreach (var sub in subList2)
                    {
                        long userID = sub.userID;
                        long submissionID = sub.submission1 == null ? -1 : sub.submission1.submissionID;
                        String submissionTypeName = sub.submission1 == null ? null : sub.submission1.submissiontype == null ? null : sub.submission1.submissiontype.name;
                        int submissionTypeID = sub.submission1 == null ? -1 : sub.submission1.submissionTypeID;
                        String submissionTitle = sub.submission1.title;
                        int topiccategoryID = sub.submission1 == null ? -1 : sub.submission1.topicID;
                        String topic = sub.submission1 == null ? null : sub.submission1.topiccategory == null ? null : sub.submission1.topiccategory.name;
                        String status = sub.submission1 == null ? null : sub.submission1.status;
                        bool byAdmin = sub.submission1 == null ? false : sub.submission1.byAdmin == true ? true : false;
                        IEnumerable<IGrouping<long, evaluatiorsubmission>> groupBy = sub.submission1 == null ? null : sub.submission1.evaluatiorsubmissions.Where(c => c.deleted == false).FirstOrDefault() == null ?
                                null : sub.submission1.evaluatiorsubmissions.Where(c => c.deleted == false).GroupBy(s => s.submissionID).ToList();
                        if (groupBy != null)
                        {
                            foreach (var subGroup in groupBy)//goes through all groups of sub/evalsub
                            {
                                foreach (var evalsForSub in subGroup)//goes through all evaluatiorsubmission for each submission
                                {
                                    int? thisScore = evalsForSub.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault() == null ?
                                        -1 : evalsForSub.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault().score;
                                    if (thisScore != -1)//if submission has been evaluated
                                    {
                                        scoreSum = scoreSum + thisScore;
                                        evalCount++;
                                    }
                                }
                                avgScore = evalCount == 0 ? 0.00 : (double)scoreSum / evalCount;
                                numOfEvaluations = evalCount;
                                scoreSum = 0;
                                evalCount = 0;
                            }
                            userSubmissions.Add(new Submission(userID, submissionID, submissionTypeName,
                            submissionTypeID, submissionTitle, topiccategoryID, topic, status, avgScore, numOfEvaluations, byAdmin));
                        }
                        else
                        {
                            userSubmissions.Add(new Submission(userID, submissionID, submissionTypeName,
                            submissionTypeID, submissionTitle, topiccategoryID, topic, status, 0, numOfEvaluations, byAdmin));
                        }
                    }
                    userSubmissions = userSubmissions.OrderBy(c => -c.avgScore).ToList();
                    page.rowCount = userSubmissions.Count();
                    if (page.rowCount > 0)
                    {
                        page.maxIndex = (int)Math.Ceiling(page.rowCount / (double)pageSize);
                        var allUserSubmissions = userSubmissions.Skip(pageSize * index).Take(pageSize).ToList(); //Skip past rows and take new elements
                        page.results = allUserSubmissions;
                    }

                    return page;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.getAllSubmissions error " + ex);
                return null;
            }
        }

        // Search within the list with a certain criteria
        public SubmissionPagingQuery searchDeletedSubmission(int index, string criteria)
        {
            SubmissionPagingQuery page = new SubmissionPagingQuery();

            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    int pageSize = 10;
                    var subs = context.submissions.Where(c => (c.title.ToLower().Contains(criteria) || c.topiccategory.name.ToLower().Contains(criteria) || c.submissiontype.name.ToLower().Contains(criteria)) && c.deleted == true).Select(d =>
                        new Submission
                        {
                            userID = d.usersubmissions.Where(c => c.deleted == true).FirstOrDefault() == null ? -1 : d.usersubmissions.Where(c => c.deleted == true).FirstOrDefault().userID,
                            submissionID = d.submissionID,
                            submissionTypeName = d.submissiontype.name,
                            submissionTypeID = d.submissionTypeID,
                            submissionTitle = d.title,
                            topiccategoryID = d.topicID,
                            topic = d.topiccategory.name,
                            status = d.status
                        }).OrderBy(c => c.submissionTitle).ToList();

                    page.rowCount = subs.Count();
                    if (page.rowCount > 0)
                    {
                        page.maxIndex = (int)Math.Ceiling(page.rowCount / (double)pageSize);
                        var registrationPayments = subs.Skip(pageSize * index).Take(pageSize).ToList(); //Skip past rows and take new elements
                        page.results = registrationPayments;
                    }

                    return page;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.getDeletedSubmissions error " + ex);
                return null;
            }
        }
        //gets file with ID in parameter
        public SubmissionDocument getSubmissionFile(long documentID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    SubmissionDocument file = context.documentssubmitteds.Where(c => c.documentssubmittedID == documentID && c.deleted == false).
                        Select(d => new SubmissionDocument
                        {
                            documentssubmittedID = d.documentssubmittedID,
                            submissionID = d.submissionID,
                            documentName = d.documentName,
                            document = d.document
                        }).FirstOrDefault();
                    return file;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.getSubmissionFile error " + ex);
                return null;
            }
        }

        //get evaluation template
        public Evaluation getEvaluationTemplate(long templateID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    Evaluation file = context.templates.Where(c => c.templateID == templateID && c.deleted == false).
                        Select(d => new Evaluation
                        {                            
                            templateID = d.templateID,
                            evaluationFile = d.document,
                            evaluationFileName = d.name,
                        }).FirstOrDefault();
                    return file;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.getSubmissionFile error " + ex);
                return null;
            }
        }

        //get submitted evaluation file
        public Evaluation getEvaluationFile(long submissionID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    Evaluation file = context.evaluatiorsubmissions.Where(c => c.submissionID == submissionID && c.deleted == false).FirstOrDefault() == null ? 
                        new Evaluation() : context.evaluatiorsubmissions.Where(c => c.submissionID == submissionID && c.deleted == false).FirstOrDefault().evaluationsubmitteds.
                        Select(d => new Evaluation
                        {                            
                            evaluationFile = d.evaluationFile,
                            evaluationFileName = d.evaluationName,
                        }).FirstOrDefault();
                    return file;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.getSubmissionFile error " + ex);
                return null;
            }
        }

    }


    public class SubmissionPagingQuery
    {
        public int indexPage;
        public int maxIndex;
        public int rowCount;
        public List<Submission> results;
    }

    public class CurrAndPrevSub
    {
        public long submissionID;
        public String userType;
        public long evaluatorID;
        public String submissionTitle;
        public String topic;
        public int topiccategoryID;
        public String submitterFirstName;
        public String submitterLastName;
        public String submitterEmail;
        public String submissionAbstract;
        public List<SubmissionDocument> submissionFileList;
        public String submissionType;
        public int submissionTypeID;
        public String templateName;
        public long templateID;
        public String evaluationTemplate;
        public String panelistNames;
        public String plan;
        public String guideQuestions;
        public String format;
        public String equipment;
        public String duration;
        public String delivery;
        public bool allowFinalVersion;
        public long evaluatiorSubmissionID;
        public String evaluationName;
        public String evaluationFile;
        public int? evaluationScore;
        public String publicFeedback;
        public bool subIsEvaluated;
        public long evaluationsubmittedID;
        //previous submition
        public bool hasPrevVersion;
        public long prevSubmissionID;
        public String prevSubmissionTitle;
        public String prevTopic;
        public String prevSubmissionAbstract;
        public List<SubmissionDocument> prevSubmissionFileList;
        public String prevSubmissionType;
        public String prevPanelistNames;
        public String prevPlan;
        public String prevGuideQuestions;
        public String prevFormat;
        public String prevEquipment;
        public String prevDuration;
        public String prevDelivery;
        public bool prevSubIsEvaluated;
        public String prevPublicFeedback;
        public String equipmentWorkshop;
        public String equipmentPanel;
        public String planPanel;
        public String planWorkshop;



        public CurrAndPrevSub()
        {

        }
    }

    public class Evaluation
    {
        public long submissionID;
        public long evaluatorID;
        public long evaluatorSubmissionID;
        public long templateID;
        public String templateName;
        public String evaluatorFirstName;
        public String evaluatorLastName;
        public int? score;
        public String publicFeedback;
        public String privateFeedback;
        public String evaluationFile;
        public String evaluationFileName;
        public String evaluationStatus;
        public bool isPrevSub;

    }

    public class SubmissionDocument
    {
        public long documentssubmittedID;
        public long submissionID;
        public String documentName;
        public String document;
        public bool? deleted;

        public SubmissionDocument()
        {

        }
    }

    public class Submission
    {
        public long userID;
        public String firstName;
        public String lastName;
        public String email;
        public long submissionID;
        public long evaluatorID;
        public String topic;
        public String userType;
        public int submissionTypeID;
        public String submissionTypeName;
        public String submissionTitle;
        public int topiccategoryID;
        public String status;
        public String templateName;
        public long templateID;
        public bool isEvaluated;
        public bool isAssigned;
        public bool isFinalSubmission;
        public bool finalSubmissionAllowed;
        public double? avgScore;
        public int numOfEvaluations;
        public bool changedAcceptanceStatus;
        public bool byAdmin;

        public Submission()
        {

        }
        public Submission(long userID, long submissionID, String submissionTypeName,
                            int submissionTypeID, String submissionTitle, int topiccategoryID, String topic,
                            String status, double? avgScore, int numOfEvaluations, bool byAdmin)
        {
            this.userID = userID;
            this.submissionID = submissionID;
            this.submissionTypeName = submissionTypeName;
            this.submissionTypeID = submissionTypeID;
            this.submissionTitle = submissionTitle;
            this.topiccategoryID = topiccategoryID;
            this.topic = topic;
            this.status = status;
            this.avgScore = avgScore;
            this.numOfEvaluations = numOfEvaluations;
            this.byAdmin = byAdmin;
        }

    }

    public class AssignedSubmission
    {
        public long submissionID;
        public String userType;
        public long evaluatorID;
        public Evaluation evaluation;
        public String submissionTitle;
        public String topic;
        public int topiccategoryID;
        public String submitterFirstName;
        public String submitterLastName;
        public String submissionAbstract;
        public List<SubmissionDocument> submissionFileList;
        public String submissionType;
        public int submissionTypeID;
        public long evaluationTemplateID;
        public String panelistNames;
        public String plan;
        public String guideQuestions;
        public String format;
        public String equipment;
        public String duration;
        public String delivery;
        public bool allowFinalVersion;
        public long evaluatiorSubmissionID;
        public String evaluationName;
        public String evaluationFile;
        public int? evaluationScore;
        public String privateFeedback;
        public String publicFeedback;
        public bool subIsEvaluated;
        public long evaluationsubmittedID;
        public String evaluatorFirstName;
        public String evaluatorLastName;
        public bool isFinalVersion;

        public AssignedSubmission()
        {

        }

    }
    public class SubmissionType
    {
        public int submissionTypeID;
        public String submissionTypeName;

        public SubmissionType()
        {

        }
        public SubmissionType(int submissionTypeID, String submissionTypeName)
        {
            this.submissionTypeID = submissionTypeID;
            this.submissionTypeName = submissionTypeName;
        }

    }

}
