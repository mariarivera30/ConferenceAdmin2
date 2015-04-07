using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NancyService.Modules
{
    class SubmissionManager
    {

        public AssignedSubmission getSubmission(long submissionID, long evaluatorID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //gets all the evaluations assigned to the given evaluator
                    AssignedSubmission subs = new AssignedSubmission();
                    evaluatiorsubmission sub;

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
                    return subs;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.getSubmission error " + ex);
                return null;
            }
        }

        public List<Submission> getAssignedSubmissions(long userID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //gets all the evaluations assigned to the given evaluator
                    List<Submission> assignedSubmissions = context.evaluatiorsubmissions.Where(c => c.evaluator.userID == userID && c.deleted == false && c.submission.usersubmissions1.FirstOrDefault() != null).
                        Select(i => new Submission
                        {
                            submissionID = i.submissionID,
                            evaluatorID = i.evaluatorID,
                            userType = i.submission.usersubmissions1.FirstOrDefault() == null ? null : i.submission.usersubmissions1.FirstOrDefault().user.usertype.userTypeName,
                            submissionTitle = i.submission.title,
                            topic = i.submission.topiccategory.name,
                            isEvaluated = (i.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault() == null ? false : true)
                        }).ToList();
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
                    userSub.allowFinalVersion = true;
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
                    List<Submission> userFinalSubmissions = context.usersubmission.Where(c => c.userID == userID && c.deleted == false && c.finalSubmissionID != null).
                        Select(i => new Submission
                        {
                            submissionID = i.submission == null ? -1 : i.submission.submissionID,
                            submissionTypeName = i.submission == null ? null : i.submission.submissiontype.name,
                            submissionTypeID = i.submission == null ? -1 : i.submission.submissionTypeID,
                            submissionTitle = i.submission == null ? null : i.submission.title,
                            topiccategoryID = i.submission == null ? -1 : i.submission.topicID,
                            status = i.submission == null ? null : i.submission.status,
                            isEvaluated = (i.submission.evaluatiorsubmissions.FirstOrDefault() == null ? null : i.submission.evaluatiorsubmissions.FirstOrDefault().statusEvaluation) == "Evaluated" ? true : false,
                            isFinalSubmission = true

                        }).ToList();
                    //get all submissions that do no have a final submission
                    List<Submission> userSubmissions = context.usersubmission.Where(c => c.userID == userID && c.deleted == false && c.finalSubmissionID == null).
                        Select(i => new Submission
                        {
                            submissionID = i.submission1 == null ? -1 : i.submission1.submissionID,
                            submissionTypeName = i.submission1 == null ? null : i.submission1.submissiontype.name,
                            submissionTypeID = i.submission1 == null ? -1 : i.submission1.submissionTypeID,
                            submissionTitle = i.submission1 == null ? null : i.submission1.title,
                            topiccategoryID = i.submission1 == null ? -1 : i.submission1.topicID,
                            status = i.submission1 == null ? null : i.submission1.status,
                            isEvaluated = (i.submission1.evaluatiorsubmissions.FirstOrDefault() == null ? null : i.submission1.evaluatiorsubmissions.FirstOrDefault().statusEvaluation) == "Evaluated" ? true : false,
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

        public AssignedSubmission getUserSubmission(long submissionID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //gets all the evaluations assigned to the given evaluator
                    AssignedSubmission subs = new AssignedSubmission();

                    submission sub = context.submissions.Where(c => c.submissionID == submissionID && c.deleted == false).FirstOrDefault();
                    if (sub.submissionTypeID == 1 || sub.submissionTypeID == 2 || sub.submissionTypeID == 4)
                    {

                        subs = new AssignedSubmission
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
                            panelistNames = null,
                            plan = null,
                            guideQuestions = null,
                            format = null,
                            equipment = null,
                            duration = null,
                            delivery = null,
                            subIsEvaluated = (sub.evaluatiorsubmissions.FirstOrDefault() == null ? null : sub.evaluatiorsubmissions.FirstOrDefault().statusEvaluation) == "Evaluated" ? true : false,
                            publicFeedback = (sub.evaluatiorsubmissions.FirstOrDefault() == null ? 
                            null : sub.evaluatiorsubmissions.FirstOrDefault().evaluationsubmitteds.FirstOrDefault()) == null ?
                            null : sub.evaluatiorsubmissions.FirstOrDefault().evaluationsubmitteds.FirstOrDefault().publicFeedback,
                            //get previous submission if possible
                            hasPrevVersion = sub.usersubmissions.FirstOrDefault() == null ? false: true,
                            prevSubmissionID = sub.usersubmissions.FirstOrDefault() == null ? -1 : sub.usersubmissions.FirstOrDefault().submission1.submissionID,
                            prevSubmissionTitle = sub.usersubmissions.FirstOrDefault() == null ? null : sub.usersubmissions.FirstOrDefault().submission1.title,
                            prevTopic = sub.usersubmissions.FirstOrDefault() == null ? null : sub.usersubmissions.FirstOrDefault().submission1.topiccategory.name,
                            prevSubmissionAbstract = sub.usersubmissions.FirstOrDefault() == null ? null : sub.usersubmissions.FirstOrDefault().submission1.submissionAbstract,
                            prevSubmissionFileList = sub.usersubmissions.FirstOrDefault() == null ? null : sub.usersubmissions.FirstOrDefault().submission1.documentssubmitteds.Where(u => u.deleted == false).
                                Select(c => new SubmissionDocument
                                {
                                    documentssubmittedID = c.documentssubmittedID,
                                    submissionID = c.submissionID,
                                    documentName = c.documentName,
                                    document = c.document,
                                    deleted = c.deleted
                                }).ToList(),
                            prevSubmissionType = sub.usersubmissions.FirstOrDefault() == null ? null : sub.usersubmissions.FirstOrDefault().submission1.submissiontype.name,
                            prevPanelistNames = null,
                            prevPlan = null,
                            prevGuideQuestions = null,
                            prevFormat = null,
                            prevEquipment = null,
                            prevDuration = null,
                            prevDelivery = null,
                            prevSubIsEvaluated = true,
                            prevPublicFeedback = ((sub.usersubmissions.FirstOrDefault() == null ?
                        null : sub.usersubmissions.FirstOrDefault().submission1 == null ?
                        null : sub.usersubmissions.FirstOrDefault().submission1.evaluatiorsubmissions.FirstOrDefault()) == null ?
                        null : sub.usersubmissions.FirstOrDefault().submission1.evaluatiorsubmissions.FirstOrDefault().evaluationsubmitteds.FirstOrDefault()) == null ?
                        null : sub.usersubmissions.FirstOrDefault().submission1.evaluatiorsubmissions.FirstOrDefault().evaluationsubmitteds.FirstOrDefault().publicFeedback


                        };
                    }
                    else if (sub.submissionTypeID == 3)
                    {

                        subs = new AssignedSubmission
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
                            panelistNames = (sub.panels.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.panels.Where(y => y.deleted == false).FirstOrDefault().panelistNames),
                            plan = (sub.panels.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.panels.Where(y => y.deleted == false).FirstOrDefault().plan),
                            guideQuestions = (sub.panels.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.panels.Where(y => y.deleted == false).FirstOrDefault().guideQuestion),
                            format = (sub.panels.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.panels.Where(y => y.deleted == false).FirstOrDefault().formatDescription),
                            equipment = (sub.panels.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.panels.Where(y => y.deleted == false).FirstOrDefault().necessaryEquipment),
                            duration = null,
                            delivery = null,
                            subIsEvaluated = (sub.evaluatiorsubmissions.FirstOrDefault() == null ? null : sub.evaluatiorsubmissions.FirstOrDefault().statusEvaluation) == "Evaluated" ? true : false,
                            publicFeedback = (sub.evaluatiorsubmissions.FirstOrDefault() == null ? 
                            null : sub.evaluatiorsubmissions.FirstOrDefault().evaluationsubmitteds.FirstOrDefault()) == null ?
                            null : sub.evaluatiorsubmissions.FirstOrDefault().evaluationsubmitteds.FirstOrDefault().publicFeedback,
                            //previous
                            hasPrevVersion = sub.usersubmissions.FirstOrDefault() == null ? false: true,
                            prevSubmissionID = sub.usersubmissions.FirstOrDefault() == null ? -1 : sub.usersubmissions.FirstOrDefault().submission1.submissionID,
                            prevSubmissionTitle = sub.usersubmissions.FirstOrDefault() == null ? null : sub.usersubmissions.FirstOrDefault().submission1.title,
                            prevTopic = sub.usersubmissions.FirstOrDefault() == null ? null : sub.usersubmissions.FirstOrDefault().submission1.topiccategory.name,
                            prevSubmissionAbstract = sub.usersubmissions.FirstOrDefault() == null ? null : sub.usersubmissions.FirstOrDefault().submission1.submissionAbstract,
                            prevSubmissionFileList = sub.usersubmissions.FirstOrDefault() == null ? null : sub.usersubmissions.FirstOrDefault().submission1.documentssubmitteds.Where(u => u.deleted == false).
                                Select(c => new SubmissionDocument
                                {
                                    documentssubmittedID = c.documentssubmittedID,
                                    submissionID = c.submissionID,
                                    documentName = c.documentName,
                                    document = c.document,
                                    deleted = c.deleted
                                }).ToList(),
                            prevSubmissionType = sub.usersubmissions.FirstOrDefault() == null ? 
                            null : sub.usersubmissions.FirstOrDefault().submission1.submissiontype.name,
                            prevPanelistNames = (sub.usersubmissions.FirstOrDefault() == null ? 
                            null : sub.usersubmissions.FirstOrDefault().submission1.panels.Where(y => y.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.FirstOrDefault().submission1.panels.Where(y => y.deleted == false).FirstOrDefault().panelistNames),                            
                            prevPlan = (sub.usersubmissions.FirstOrDefault() == null ? 
                            null : sub.usersubmissions.FirstOrDefault().submission1.panels.Where(y => y.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.FirstOrDefault().submission1.panels.Where(y => y.deleted == false).FirstOrDefault().plan),                            
                            prevGuideQuestions = (sub.usersubmissions.FirstOrDefault() == null ? 
                            null : sub.usersubmissions.FirstOrDefault().submission1.panels.Where(y => y.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.FirstOrDefault().submission1.panels.Where(y => y.deleted == false).FirstOrDefault().guideQuestion),
                            prevFormat = (sub.usersubmissions.FirstOrDefault() == null ? 
                            null : sub.usersubmissions.FirstOrDefault().submission1.panels.Where(y => y.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.FirstOrDefault().submission1.panels.Where(y => y.deleted == false).FirstOrDefault().formatDescription),                            
                            prevEquipment = (sub.usersubmissions.FirstOrDefault() == null ? 
                            null : sub.usersubmissions.FirstOrDefault().submission1.panels.Where(y => y.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.FirstOrDefault().submission1.panels.Where(y => y.deleted == false).FirstOrDefault().necessaryEquipment),
                            prevDuration = null,
                            prevDelivery = null,
                            prevSubIsEvaluated = true,
                            prevPublicFeedback = ((sub.usersubmissions.FirstOrDefault() == null ?
                        null : sub.usersubmissions.FirstOrDefault().submission1 == null ?
                        null : sub.usersubmissions.FirstOrDefault().submission1.evaluatiorsubmissions.FirstOrDefault()) == null ?
                        null : sub.usersubmissions.FirstOrDefault().submission1.evaluatiorsubmissions.FirstOrDefault().evaluationsubmitteds.FirstOrDefault()) == null ?
                        null : sub.usersubmissions.FirstOrDefault().submission1.evaluatiorsubmissions.FirstOrDefault().evaluationsubmitteds.FirstOrDefault().publicFeedback

                        };                        
                    }
                    else if (sub.submissionTypeID == 5)
                    {

                        subs = new AssignedSubmission
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
                            panelistNames = null,
                            plan = (sub.workshops.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.workshops.Where(y => y.deleted == false).FirstOrDefault().plan),
                            guideQuestions = null,
                            format = null,
                            equipment = (sub.workshops.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.workshops.Where(y => y.deleted == false).FirstOrDefault().necessary_equipment),
                            duration = (sub.workshops.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.workshops.Where(y => y.deleted == false).FirstOrDefault().duration),
                            delivery = (sub.workshops.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.workshops.Where(y => y.deleted == false).FirstOrDefault().delivery),
                            subIsEvaluated = (sub.evaluatiorsubmissions.FirstOrDefault() == null ? null : sub.evaluatiorsubmissions.FirstOrDefault().statusEvaluation) == "Evaluated" ? true : false,
                            publicFeedback = (sub.evaluatiorsubmissions.FirstOrDefault() == null ? null : sub.evaluatiorsubmissions.FirstOrDefault().evaluationsubmitteds.FirstOrDefault()) == null ?
                            null : sub.evaluatiorsubmissions.FirstOrDefault().evaluationsubmitteds.FirstOrDefault().publicFeedback,
                            //previous
                            hasPrevVersion = sub.usersubmissions.FirstOrDefault() == null ? false : true,
                            prevSubmissionID = sub.usersubmissions.FirstOrDefault() == null ? -1 : sub.usersubmissions.FirstOrDefault().submission1.submissionID,
                            prevSubmissionTitle = sub.usersubmissions.FirstOrDefault() == null ? null : sub.usersubmissions.FirstOrDefault().submission1.title,
                            prevTopic = sub.usersubmissions.FirstOrDefault() == null ? null : sub.usersubmissions.FirstOrDefault().submission1.topiccategory.name,
                            prevSubmissionAbstract = sub.usersubmissions.FirstOrDefault() == null ? null : sub.usersubmissions.FirstOrDefault().submission1.submissionAbstract,
                            prevSubmissionFileList = sub.usersubmissions.FirstOrDefault() == null ? null : sub.usersubmissions.FirstOrDefault().submission1.documentssubmitteds.Where(u => u.deleted == false).
                                Select(c => new SubmissionDocument
                                {
                                    documentssubmittedID = c.documentssubmittedID,
                                    submissionID = c.submissionID,
                                    documentName = c.documentName,
                                    document = c.document,
                                    deleted = c.deleted
                                }).ToList(),
                            prevSubmissionType = sub.usersubmissions.FirstOrDefault() == null ? 
                            null : sub.usersubmissions.FirstOrDefault().submission1.submissiontype.name,
                            prevPanelistNames = null, 
                            prevPlan = (sub.usersubmissions.FirstOrDefault() == null ? 
                            null : sub.usersubmissions.FirstOrDefault().submission1.workshops.Where(y => y.deleted == false).FirstOrDefault() == null ? 
                            null : sub.usersubmissions.FirstOrDefault().submission1.workshops.Where(y => y.deleted == false).FirstOrDefault().plan),
                            prevGuideQuestions = null,
                            prevFormat = null, 
                            prevEquipment = (sub.usersubmissions.FirstOrDefault() == null ? 
                            null : sub.usersubmissions.FirstOrDefault().submission1.workshops.Where(y => y.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.FirstOrDefault().submission1.workshops.Where(y => y.deleted == false).FirstOrDefault().necessary_equipment),
                            prevDuration = (sub.usersubmissions.FirstOrDefault() == null ? 
                            null : sub.usersubmissions.FirstOrDefault().submission1.workshops.Where(y => y.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.FirstOrDefault().submission1.workshops.Where(y => y.deleted == false).FirstOrDefault().duration),
                            prevDelivery = (sub.usersubmissions.FirstOrDefault() == null ? 
                            null : sub.usersubmissions.FirstOrDefault().submission1.workshops.Where(y => y.deleted == false).FirstOrDefault() == null ?
                            null : sub.usersubmissions.FirstOrDefault().submission1.workshops.Where(y => y.deleted == false).FirstOrDefault().delivery),                 
                            prevSubIsEvaluated = true,
                            prevPublicFeedback = ((sub.usersubmissions.FirstOrDefault() == null ?
                        null : sub.usersubmissions.FirstOrDefault().submission1 == null ?
                        null : sub.usersubmissions.FirstOrDefault().submission1.evaluatiorsubmissions.FirstOrDefault()) == null ?
                        null : sub.usersubmissions.FirstOrDefault().submission1.evaluatiorsubmissions.FirstOrDefault().evaluationsubmitteds.FirstOrDefault()) == null ?
                        null : sub.usersubmissions.FirstOrDefault().submission1.evaluatiorsubmissions.FirstOrDefault().evaluationsubmitteds.FirstOrDefault().publicFeedback

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

                public object getAllSubmissions()
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //get all final submissions.
                    List<Submission> userSubmissions = new List<Submission>();
                    List<usersubmission> subList = context.usersubmission.Where(c => c.deleted == false && c.finalSubmissionID != null).ToList();
                    foreach (var sub in subList)
                    {
                            
                            long submissionID = sub.submission == null ? -1 : sub.submission.submissionID;
                            String submissionTypeName = sub.submission == null ? null : sub.submission.submissiontype == null ? null : sub.submission.submissiontype.name;
                            int submissionTypeID = sub.submission == null ? -1 : sub.submission.submissionTypeID;
                            String submissionTitle = sub.submission == null ? null : sub.submission.title;
                            int topiccategoryID = sub.submission == null ? -1 : sub.submission.topicID;
                            String topic = sub.submission == null ? null : sub.submission.topiccategory == null ? null : sub.submission.topiccategory.name;
                            String status = sub.submission == null ? null : sub.submission.status;
                        String acceptanceStatus = sub.submission == null ? 
                                                null : sub.submission.usersubmissions.FirstOrDefault() == null ?
                                                null : sub.submission.usersubmissions.FirstOrDefault().user.acceptanceStatus;
                        double? avgScore = sub.submission == null ? null : sub.submission.evaluatiorsubmissions.FirstOrDefault() == null ?
                                0 : sub.submission.evaluatiorsubmissions.FirstOrDefault().evaluationsubmitteds.FirstOrDefault() == null ?
                                0 : sub.submission.evaluatiorsubmissions.FirstOrDefault().evaluationsubmitteds.Average(q => q.score);

                            userSubmissions.Add(new Submission(submissionID, submissionTypeName, 
                            submissionTypeID, submissionTitle,topiccategoryID, topic, status, acceptanceStatus, avgScore));
                    }

                    //get all submissions that do no have a final submission
                    List<usersubmission> subList2 = context.usersubmission.Where(c => c.deleted == false && c.finalSubmissionID == null).ToList();
                    foreach (var sub in subList2)
                    {
                        long submissionID = sub.submission1 == null ? -1 : sub.submission1.submissionID;
                        String submissionTypeName = sub.submission1 == null ? null : sub.submission1.submissiontype == null ? null : sub.submission1.submissiontype.name;
                        int submissionTypeID = sub.submission1 == null? -1 : sub.submission1.submissionTypeID;
                            String submissionTitle = sub.submission1.title;
                        int topiccategoryID = sub.submission1 == null ? -1 : sub.submission1.topicID;
                        String topic = sub.submission1 == null ? null : sub.submission1.topiccategory == null ? null : sub.submission1.topiccategory.name;
                        String status = sub.submission1 == null ? null : sub.submission1.status;
                        String acceptanceStatus = sub.submission1 == null ? null : sub.submission1.usersubmissions.FirstOrDefault() == null ? 
                                                null : sub.submission1.usersubmissions.FirstOrDefault().user.acceptanceStatus;
                        double? avgScore = sub.submission1 == null ? null : sub.submission1.evaluatiorsubmissions.FirstOrDefault() == null ?
                                0 : sub.submission1.evaluatiorsubmissions.FirstOrDefault().evaluationsubmitteds.FirstOrDefault() == null ?
                                0 : sub.submission1.evaluatiorsubmissions.FirstOrDefault().evaluationsubmitteds.Average(q => q.score);
                               
                        userSubmissions.Add(new Submission(submissionID, submissionTypeName, 
                            submissionTypeID, submissionTitle,topiccategoryID, topic, status, acceptanceStatus, avgScore));
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
                    sub.status = "Accepted";//------------------------make sure!!!????
                    sub.creationDate = DateTime.Now;
                    sub.deleted = false;
                    sub.byAdmin = false;
                    context.submissions.Add(sub);
                    context.SaveChanges();
                    //table usersubmission
                    long finalSubmissionID = sub.submissionID;
                    usersubmission usersub = context.usersubmission.Where(c => c.initialSubmissionID == usersubTA.initialSubmissionID).FirstOrDefault();
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
        public long submissionID;
        public long evaluatorID;
        public String topic;
        public String userType;
        public int submissionTypeID;
        public String submissionTypeName;
        public String submissionTitle;
        public int topiccategoryID;
        public String status;
        public bool isEvaluated;
        public bool isFinalSubmission;
        public bool finalSubmissionAllowed;
        public String acceptanceStatus;
        public double? avgScore;

        public Submission()
        {

        }
        public Submission(long submissionID, String submissionTypeName,
                            int submissionTypeID, String submissionTitle, int topiccategoryID, String topic,
                            String status, String acceptanceStatus, double? avgScore)
        {
            this.submissionID = submissionID;
            this.submissionTypeName = submissionTypeName;
            this.submissionTypeID = submissionTypeID;
            this.submissionTitle = submissionTitle;
            this.topiccategoryID = topiccategoryID;
            this.topic = topic;
            this.status = status;
            this.acceptanceStatus = acceptanceStatus;
            this.avgScore = avgScore;
        }

    }

    public class AssignedSubmission
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
