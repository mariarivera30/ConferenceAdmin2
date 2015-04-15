using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NancyService.Modules
{
    class SubmissionManager
    {
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
                                    document = c.document,
                                    deleted = c.deleted
                                }).ToList(),
                            submissionType = sub.submission.submissiontype.name,
                            evaluationTemplate = sub.submission.templatesubmissions.Where(u => u.deleted == false).FirstOrDefault() == null ? null : sub.submission.templatesubmissions.Where(u => u.deleted == false).FirstOrDefault().template.document,
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
                                    document = c.document,
                                    deleted = c.deleted
                                }).ToList(),
                            submissionType = sub.submission.submissiontype.name,
                            evaluationTemplate = sub.submission.templatesubmissions.Where(u => u.deleted == false).FirstOrDefault() == null ? null : sub.submission.templatesubmissions.Where(u => u.deleted == false).FirstOrDefault().template.document,
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
                                    document = c.document,
                                    deleted = c.deleted
                                }).ToList(),
                            submissionType = sub.submission.submissiontype.name,
                            evaluationTemplate = sub.submission.templatesubmissions.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.submission.templatesubmissions.Where(y => y.deleted == false).FirstOrDefault().template.document,
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

        public List<Submission> getAssignedSubmissions(long userID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
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

                    return assignedSubmissions;
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
                    evaluation.deleted = false;
                    context.evaluationsubmitteds.Add(evaluation);
                    context.SaveChanges();
                    context.evaluatiorsubmissions.Where(c => c.evaluationsubmissionID == evaluation.evaluatiorSubmissionID).FirstOrDefault().statusEvaluation = "Evaluated";
                    context.SaveChanges();
                    usersubmission userSub = context.usersubmission.Where(c => c.initialSubmissionID == usersubIn.initialSubmissionID).FirstOrDefault();
                    userSub.allowFinalVersion = usersubIn.allowFinalVersion;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.addEvaluation error " + ex);
                return false;
            }
        }

        public bool editEvaluation(evaluationsubmitted evaluation, usersubmission userSubIn)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
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
                    usersubmission userSub = context.usersubmission.Where(c => c.initialSubmissionID == userSubIn.initialSubmissionID).FirstOrDefault();
                    userSub.allowFinalVersion = userSubIn.allowFinalVersion;
                    context.SaveChanges();
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
                    return userSubmissions;
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
                    if (sub.submissionTypeID == 1 || sub.submissionTypeID == 2 || sub.submissionTypeID == 4)
                    {

                        subs = new CurrAndPrevSub
                        {
                            submissionID = sub.submissionID,
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
                                    document = c.document,
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
                                    document = c.document,
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
                                    document = c.document,
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
                                    document = c.document,
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
                                    document = c.document,
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
                                    document = c.document,
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

        public bool deleteSubmission(long submissionID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    submission sub = context.submissions.Where(c => c.submissionID == submissionID).FirstOrDefault();
                    bool isFinalVersion = context.usersubmission.Where(c => c.deleted == false && c.finalSubmissionID == submissionID).FirstOrDefault() == null ? false : true;
                    //if submission to be deleted is final version disconnect the final version from the previous one
                    if (isFinalVersion)
                    {
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
                    if(sub.usersubmissions.FirstOrDefault() != null)
                    {
                    sub.usersubmissions.FirstOrDefault().deleted = true;
                    }
                    if (sub.usersubmissions1.FirstOrDefault() != null)
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
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.deleteSubmission error " + ex);
                return false;
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
                        //delete every existent document bound to the submission
                        List<documentssubmitted> documents = context.documentssubmitteds.Where(d => d.submissionID == sub.submissionID).ToList<documentssubmitted>();
                        foreach (var doc in documents)
                        {
                            context.documentssubmitteds.Remove(doc);
                        }
                        context.SaveChanges();
                        
                        //replace every document bound to the submission
                        documentssubmitted subDocs = new documentssubmitted();
                        foreach (var docs in submissionToEdit.documentssubmitteds)
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

        public List<Submission> getAllSubmissions()
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
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
                        int submissionTypeID = sub.submission1 == null? -1 : sub.submission1.submissionTypeID;
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
                    return userSubmissions;
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
                    //table documents submitted
                    if (submissionToAdd.submissionTypeID != 4 && submissionDocuments != null)
                    {
                        documentssubmitted subDocs = new documentssubmitted();
                        subDocs.submissionID = finalSubmissionID;
                        subDocs.documentName = submissionDocuments.documentName;
                        subDocs.document = submissionDocuments.document;
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
                    //table documents submitted
                    if (submissionToAdd.submissionTypeID != 4 && submissionDocuments != null)
                    {
                        documentssubmitted subDocs = new documentssubmitted();
                        subDocs.submissionID = finalSubmissionID;
                        subDocs.documentName = submissionDocuments.documentName;
                        subDocs.document = submissionDocuments.document;
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
                    if(initialSubmissionID > -1)//if submissionID belong to a submission that does has a previous version
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
                        context.SaveChanges();
                        changedAcceptanceStatus = true;
                    }
                    Submission subAltered = new Submission();
                    subAltered.changedAcceptanceStatus = changedAcceptanceStatus;
                    subAltered.submissionID = sub.submissionID;
                    subAltered.status = newStatus;

                    return subAltered;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.changeSubmissionStatus error " + ex);
                return null;
            }
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
        public List<Submission> getDeletedSubmissions()
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
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
                        }).ToList();
                    return subs;
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
                                    document = c.document,
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
