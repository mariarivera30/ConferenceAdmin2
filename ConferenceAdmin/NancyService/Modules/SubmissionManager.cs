using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NancyService.Modules
{
    //class SubmissionManager
    //{

    //    public AssignedSubmission getSubmission(long submissionID)
    //    {
    //        try
    //        {
    //            using (conferenceadminContext context = new conferenceadminContext())
    //            {
    //                //gets all the evaluations assigned to the given evaluator
    //                AssignedSubmission subs = new AssignedSubmission();
    //                evaluatiorsubmission sub = context.evaluatiorsubmissions.Where(c => c.submissionID == submissionID && c.deleted == false).FirstOrDefault();

    //                if (sub.submission.submissiontype.name == "Paper" || sub.submission.submissiontype.name == "Poster" || sub.submission.submissiontype.name == "BOF")
    //                {

    //                    subs = new AssignedSubmission
    //                    {
    //                        submissionID = sub.submissionID,
    //                        userType = sub.submission.user.usertype.userTypeName,
    //                        evaluatorID = sub.evaluatorID,
    //                        submissionTitle = sub.submission.title,
    //                        topic = sub.submission.topiccategory.name,
    //                        submitterFirstName = sub.submission.user.firstName,
    //                        submitterLastName = sub.submission.user.lastName,
    //                        submissionAbstract = sub.submission.submissionAbstract,
    //                        submissionFileList = sub.submission.documentssubmitteds.Where(u => u.deleted == false).
    //                            Select(c => new SubmissionDocument
    //                            {
    //                                documentssubmittedID = c.documentssubmittedID,
    //                                submissionID = c.submissionID,
    //                                documentName = c.documentName,
    //                                document = c.document,
    //                                deleted = c.deleted
    //                            }).ToList(),
    //                        submissionType = sub.submission.submissiontype.name,
    //                        evaluationTemplate = sub.submission.templatesubmissions.Where(u => u.deleted == false).FirstOrDefault().template.document,
    //                        panelistNames = null,
    //                        plan = null,
    //                        guideQuestions = null,
    //                        format = null,
    //                        equipment = null,
    //                        duration = null,
    //                        delivery = null,
    //                        evaluationsubmittedID = (sub.evaluationsubmitteds.FirstOrDefault() == null ? -1 : sub.evaluationsubmitteds.FirstOrDefault().evaluationsubmittedID),
    //                        evaluatiorSubmissionID = sub.evaluationsubmissionID,
    //                        evaluationFile = sub.evaluationsubmitteds.Where(d => d.deleted == false).Select(r => r.evaluationFile).FirstOrDefault(),
    //                        evaluationScore = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.score).FirstOrDefault(),
    //                        publicFeedback = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.publicFeedback).FirstOrDefault(),
    //                        privateFeedback = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.privateFeedback).FirstOrDefault(),
    //                        subIsEvaluated = (sub.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault() == null ? false : true)

    //                    };
    //                }
    //                else if (sub.submission.submissiontype.name == "Pannel")
    //                {

    //                    subs = new AssignedSubmission
    //                    {
    //                        submissionID = sub.submissionID,
    //                        userType = sub.submission.user.usertype.userTypeName,
    //                        evaluatorID = sub.evaluatorID,
    //                        submissionTitle = sub.submission.title,
    //                        topic = sub.submission.topiccategory.name,
    //                        submitterFirstName = sub.submission.user.firstName,
    //                        submitterLastName = sub.submission.user.lastName,
    //                        submissionAbstract = sub.submission.submissionAbstract,
    //                        submissionFileList = sub.submission.documentssubmitteds.Where(u => u.deleted == false).
    //                            Select(c => new SubmissionDocument
    //                            {
    //                                documentssubmittedID = c.documentssubmittedID,
    //                                submissionID = c.submissionID,
    //                                documentName = c.documentName,
    //                                document = c.document,
    //                                deleted = c.deleted
    //                            }).ToList(),
    //                        submissionType = sub.submission.submissiontype.name,
    //                        evaluationTemplate = sub.submission.templatesubmissions.Where(u => u.deleted == false).FirstOrDefault().template.document,
    //                        panelistNames = sub.submission.panels.Where(y => y.deleted == false).FirstOrDefault().panelistNames,
    //                        plan = sub.submission.panels.Where(y => y.deleted == false).FirstOrDefault().plan,
    //                        guideQuestions = sub.submission.panels.Where(y => y.deleted == false).FirstOrDefault().guideQuestion,
    //                        format = sub.submission.panels.Where(y => y.deleted == false).FirstOrDefault().formatDescription,
    //                        equipment = sub.submission.panels.Where(y => y.deleted == false).FirstOrDefault().necessaryEquipment,
    //                        duration = null,
    //                        delivery = null,
    //                        evaluationsubmittedID = (sub.evaluationsubmitteds.FirstOrDefault() == null ? -1 : sub.evaluationsubmitteds.FirstOrDefault().evaluationsubmittedID),
    //                        evaluatiorSubmissionID = sub.evaluationsubmissionID,
    //                        evaluationFile = sub.evaluationsubmitteds.Where(d => d.deleted == false).Select(r => r.evaluationFile).FirstOrDefault(),
    //                        evaluationScore = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.score).FirstOrDefault(),
    //                        publicFeedback = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.publicFeedback).FirstOrDefault(),
    //                        privateFeedback = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.privateFeedback).FirstOrDefault(),
    //                        subIsEvaluated = (sub.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault() == null ? false : true)

    //                    };
    //                }
    //                else if (sub.submission.submissiontype.name == "Workshop")
    //                {

    //                    subs = new AssignedSubmission
    //                    {
    //                        submissionID = sub.submissionID,
    //                        userType = sub.submission.user.usertype.userTypeName,
    //                        evaluatorID = sub.evaluatorID,
    //                        submissionTitle = sub.submission.title,
    //                        topic = sub.submission.topiccategory.name,
    //                        submitterFirstName = sub.submission.user.firstName,
    //                        submitterLastName = sub.submission.user.lastName,
    //                        submissionAbstract = sub.submission.submissionAbstract,
    //                        submissionFileList = sub.submission.documentssubmitteds.Where(u => u.deleted == false).
    //                            Select(c => new SubmissionDocument
    //                            {
    //                                documentssubmittedID = c.documentssubmittedID,
    //                                submissionID = c.submissionID,
    //                                documentName = c.documentName,
    //                                document = c.document,
    //                                deleted = c.deleted
    //                            }).ToList(),
    //                        submissionType = sub.submission.submissiontype.name,
    //                        evaluationTemplate = sub.submission.templatesubmissions.Where(y => y.deleted == false).FirstOrDefault().template.document,
    //                        panelistNames = null,
    //                        plan = sub.submission.workshops.Where(y => y.deleted == false).FirstOrDefault().plan,
    //                        guideQuestions = null,
    //                        format = null,
    //                        equipment = sub.submission.workshops.Where(y => y.deleted == false).FirstOrDefault().necessary_equipment,
    //                        duration = sub.submission.workshops.Where(y => y.deleted == false).FirstOrDefault().duration,
    //                        delivery = sub.submission.workshops.Where(y => y.deleted == false).FirstOrDefault().delivery,
    //                        evaluationsubmittedID = (sub.evaluationsubmitteds.FirstOrDefault() == null ? -1 : sub.evaluationsubmitteds.FirstOrDefault().evaluationsubmittedID),
    //                        evaluatiorSubmissionID = sub.evaluationsubmissionID,
    //                        evaluationFile = sub.evaluationsubmitteds.Where(d => d.deleted == false).Select(r => r.evaluationFile).FirstOrDefault(),
    //                        evaluationScore = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.score).FirstOrDefault(),
    //                        publicFeedback = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.publicFeedback).FirstOrDefault(),
    //                        privateFeedback = sub.evaluationsubmitteds.Where(c => c.deleted == false).Select(r => r.privateFeedback).FirstOrDefault(),
    //                        subIsEvaluated = (sub.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault() == null ? false : true)
    //                    };
    //                }
    //                return subs;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.Write("SubmissionManager.getSubmission error " + ex);
    //            return null;
    //        }
    //    }

    //    public List<Submission> getAssignedSubmissions(long userID)
    //    {
    //        try
    //        {
    //            using (conferenceadminContext context = new conferenceadminContext())
    //            {
    //                //gets all the evaluations assigned to the given evaluator
    //                List<Submission> assignedSubmissions = context.evaluatiorsubmissions.Where(c => c.evaluator.userID == userID && c.deleted == false).
    //                    Select(i => new Submission
    //                    {
    //                        submissionID = i.submissionID,
    //                        userType = i.submission.user.usertype.userTypeName,
    //                        submissionTitle = i.submission.title,
    //                        topic = i.submission.topiccategory.name,
    //                        isEvaluated = (i.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault() == null ? false : true)
    //                    }).ToList();
    //                return assignedSubmissions;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.Write("SubmissionManager.getAssignedSubmissions error " + ex);
    //            return null;
    //        }
    //    }

    //    public bool addEvaluation(evaluationsubmitted evaluation)
    //    {
    //        try
    //        {
    //            using (conferenceadminContext context = new conferenceadminContext())
    //            {
    //                evaluation.deleted = false;
    //                context.evaluationsubmitteds.Add(evaluation);
    //                context.SaveChanges();
    //                return true;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.Write("SubmissionManager.addEvaluation error " + ex);
    //            return false;
    //        }
    //    }

    //    public bool editEvaluation(evaluationsubmitted evaluation)
    //    {
    //        try
    //        {
    //            using (conferenceadminContext context = new conferenceadminContext())
    //            {
    //                evaluationsubmitted dbEvaluation = context.evaluationsubmitteds.Where(c => c.evaluationsubmittedID == evaluation.evaluationsubmittedID).FirstOrDefault();
    //                dbEvaluation.deleted = false;
    //                dbEvaluation.evaluationFile = evaluation.evaluationFile;
    //                dbEvaluation.score = evaluation.score;
    //                dbEvaluation.publicFeedback = evaluation.publicFeedback;
    //                dbEvaluation.privateFeedback = evaluation.privateFeedback;


    //                context.SaveChanges();
    //                return true;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.Write("SubmissionManager.editEvaluation error " + ex);
    //            return false;
    //        }
    //    }
    //}

    //public class SubmissionDocument
    //{
    //    public long documentssubmittedID;
    //    public long submissionID;
    //    public String documentName;
    //    public String document;
    //    public bool? deleted;

    //    public SubmissionDocument()
    //    {

    //    }
    //}

    //public class Submission
    //{
    //    public long submissionID;
    //    public String topic;
    //    public String userType;
    //    public String submissionTitle;
    //    public bool isEvaluated;

    //    public Submission()
    //    {

    //    }
    //}

    //public class AssignedSubmission
    //{
    //    public long submissionID;
    //    public String userType;
    //    public long evaluatorID;
    //    public String submissionTitle;
    //    public String topic;
    //    public String submitterFirstName;
    //    public String submitterLastName;
    //    public String submissionAbstract;
    //    public List<SubmissionDocument> submissionFileList;
    //    public String submissionType;
    //    public String evaluationTemplate;
    //    public String panelistNames;
    //    public String plan;
    //    public String guideQuestions;
    //    public String format;
    //    public String equipment;
    //    public String duration;
    //    public String delivery;
    //    public long evaluatiorSubmissionID;
    //    public String evaluationFile;
    //    public int? evaluationScore;
    //    public String privateFeedback;
    //    public String publicFeedback;
    //    public bool subIsEvaluated;
    //    public long evaluationsubmittedID;

    //    public AssignedSubmission()
    //    {

    //    }
    //}
}
