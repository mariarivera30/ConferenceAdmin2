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
                            userType = sub.submission.usersubmissions.FirstOrDefault() == null ? null : sub.submission.usersubmissions.FirstOrDefault().user.usertype.userTypeName,
                            evaluatorID = sub.evaluatorID,
                            submissionTitle = sub.submission.title,
                            topic = sub.submission.topiccategory.name,
                            submitterFirstName = sub.submission.usersubmissions.FirstOrDefault() == null ? null : sub.submission.usersubmissions.FirstOrDefault().user.firstName,
                            submitterLastName = sub.submission.usersubmissions.FirstOrDefault() == null ? null : sub.submission.usersubmissions.FirstOrDefault().user.lastName,
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
                            subIsEvaluated = (sub.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault() == null ? false : true)

                        };
                    }
                    else if (sub.submission.submissionTypeID == 3)
                    {

                        subs = new AssignedSubmission
                        {
                            submissionID = sub.submissionID,
                            userType = sub.submission.usersubmissions.FirstOrDefault() == null ? null : sub.submission.usersubmissions.FirstOrDefault().user.usertype.userTypeName,
                            evaluatorID = sub.evaluatorID,
                            submissionTitle = sub.submission.title,
                            topic = sub.submission.topiccategory.name,
                            submitterFirstName = sub.submission.usersubmissions.FirstOrDefault() == null ? null : sub.submission.usersubmissions.FirstOrDefault().user.firstName,
                            submitterLastName = sub.submission.usersubmissions.FirstOrDefault() == null ? null : sub.submission.usersubmissions.FirstOrDefault().user.lastName,
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
                            subIsEvaluated = (sub.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault() == null ? false : true)

                        };
                    }
                    else if (sub.submission.submissionTypeID == 5)
                    {

                        subs = new AssignedSubmission
                        {
                            submissionID = sub.submissionID,
                            userType = sub.submission.usersubmissions.FirstOrDefault() == null ? null : sub.submission.usersubmissions.FirstOrDefault().user.usertype.userTypeName,
                            evaluatorID = sub.evaluatorID,
                            submissionTitle = sub.submission.title,
                            topic = sub.submission.topiccategory.name,
                            submitterFirstName = sub.submission.usersubmissions.FirstOrDefault() == null ? null : sub.submission.usersubmissions.FirstOrDefault().user.firstName,
                            submitterLastName = sub.submission.usersubmissions.FirstOrDefault() == null ? null : sub.submission.usersubmissions.FirstOrDefault().user.lastName,
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
                            subIsEvaluated = (sub.evaluationsubmitteds.Where(c => c.deleted == false).FirstOrDefault() == null ? false : true)
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
                    List<Submission> assignedSubmissions = context.evaluatiorsubmissions.Where(c => c.evaluator.userID == userID && c.deleted == false).
                        Select(i => new Submission
                        {
                            submissionID = i.submissionID,
                            evaluatorID = i.evaluatorID,
                            userType = i.submission.usersubmissions.FirstOrDefault() == null ? null : i.submission.usersubmissions.FirstOrDefault().user.usertype.userTypeName,
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

        public bool addEvaluation(evaluationsubmitted evaluation)
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
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.addEvaluation error " + ex);
                return false;
            }
        }

        public bool editEvaluation(evaluationsubmitted evaluation)
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
                            status = i.submission == null ? null : i.submission.status,
                            isEvaluated = (i.submission.evaluatiorsubmissions.FirstOrDefault() == null ? null : i.submission.evaluatiorsubmissions.FirstOrDefault().statusEvaluation) == "Evaluated" ? true : false

                        }).ToList();
                    //get all submissions that do no have a final submission
                    List<Submission> userSubmissions = context.usersubmission.Where(c => c.userID == userID && c.deleted == false && c.finalSubmissionID == null).
                        Select(i => new Submission
                        {
                            submissionID = i.submission1 == null ? -1 : i.submission1.submissionID,
                            submissionTypeName = i.submission1 == null ? null : i.submission1.submissiontype.name,
                            submissionTypeID = i.submission1 == null ? -1 : i.submission1.submissionTypeID,
                            submissionTitle = i.submission1 == null ? null : i.submission1.title,
                            status = i.submission1 == null ? null : i.submission1.status,
                            isEvaluated = (i.submission1.evaluatiorsubmissions.FirstOrDefault() == null ? null : i.submission.evaluatiorsubmissions.FirstOrDefault().statusEvaluation) == "Evaluated" ? true : false

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
                    submission sub;

                    sub = context.submissions.Where(c => c.submissionID == submissionID && c.deleted == false).FirstOrDefault();

                    if (sub.submissionTypeID == 1 || sub.submissionTypeID == 2 || sub.submissionTypeID == 4)
                    {

                        subs = new AssignedSubmission
                        {
                            submissionID = sub.submissionID,
                            submissionTitle = sub.title,
                            topic = sub.topiccategory.name,
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
                            panelistNames = null,
                            plan = null,
                            guideQuestions = null,
                            format = null,
                            equipment = null,
                            duration = null,
                            delivery = null,
                            subIsEvaluated = (sub.evaluatiorsubmissions.FirstOrDefault() == null ? null : sub.evaluatiorsubmissions.FirstOrDefault().statusEvaluation) == "Evaluated" ? true : false,
                            publicFeedback = (sub.evaluatiorsubmissions.FirstOrDefault() == null ? null : sub.evaluatiorsubmissions.FirstOrDefault().evaluationsubmitteds.FirstOrDefault()) == null ?
                            null : sub.evaluatiorsubmissions.FirstOrDefault().evaluationsubmitteds.FirstOrDefault().publicFeedback
                        };
                    }
                    else if (sub.submissionTypeID == 3)
                    {

                        subs = new AssignedSubmission
                        {
                            submissionID = sub.submissionID,
                            submissionTitle = sub.title,
                            topic = sub.topiccategory.name,
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
                            panelistNames = (sub.panels.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.panels.Where(y => y.deleted == false).FirstOrDefault().panelistNames),
                            plan = (sub.panels.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.panels.Where(y => y.deleted == false).FirstOrDefault().plan),
                            guideQuestions = (sub.panels.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.panels.Where(y => y.deleted == false).FirstOrDefault().guideQuestion),
                            format = (sub.panels.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.panels.Where(y => y.deleted == false).FirstOrDefault().formatDescription),
                            equipment = (sub.panels.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.panels.Where(y => y.deleted == false).FirstOrDefault().necessaryEquipment),
                            duration = null,
                            delivery = null,
                            subIsEvaluated = (sub.evaluatiorsubmissions.FirstOrDefault() == null ? null : sub.evaluatiorsubmissions.FirstOrDefault().statusEvaluation) == "Evaluated" ? true : false,
                            publicFeedback = (sub.evaluatiorsubmissions.FirstOrDefault() == null ? null : sub.evaluatiorsubmissions.FirstOrDefault().evaluationsubmitteds.FirstOrDefault()) == null ?
                            null : sub.evaluatiorsubmissions.FirstOrDefault().evaluationsubmitteds.FirstOrDefault().publicFeedback
                        };
                    }
                    else if (sub.submissionTypeID == 5)
                    {

                        subs = new AssignedSubmission
                        {
                            submissionID = sub.submissionID,
                            submissionTitle = sub.title,
                            topic = sub.topiccategory.name,
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
                            panelistNames = null,
                            plan = (sub.workshops.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.workshops.Where(y => y.deleted == false).FirstOrDefault().plan),
                            guideQuestions = null,
                            format = null,
                            equipment = (sub.workshops.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.workshops.Where(y => y.deleted == false).FirstOrDefault().necessary_equipment),
                            duration = (sub.workshops.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.workshops.Where(y => y.deleted == false).FirstOrDefault().duration),
                            delivery = (sub.workshops.Where(y => y.deleted == false).FirstOrDefault() == null ? null : sub.workshops.Where(y => y.deleted == false).FirstOrDefault().delivery),
                            subIsEvaluated = (sub.evaluatiorsubmissions.FirstOrDefault() == null ? null : sub.evaluatiorsubmissions.FirstOrDefault().statusEvaluation) == "Evaluated" ? true : false,
                            publicFeedback = (sub.evaluatiorsubmissions.FirstOrDefault() == null ? null : sub.evaluatiorsubmissions.FirstOrDefault().evaluationsubmitteds.FirstOrDefault()) == null ?
                            null : sub.evaluatiorsubmissions.FirstOrDefault().evaluationsubmitteds.FirstOrDefault().publicFeedback
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
        public String status;
        public bool isEvaluated;

        public Submission()
        {

        }

    }

    public class AssignedSubmission
    {
        public long submissionID;
        public String userType;
        public long evaluatorID;
        public String submissionTitle;
        public String topic;
        public String submitterFirstName;
        public String submitterLastName;
        public String submissionAbstract;
        public List<SubmissionDocument> submissionFileList;
        public String submissionType;
        public String evaluationTemplate;
        public String panelistNames;
        public String plan;
        public String guideQuestions;
        public String format;
        public String equipment;
        public String duration;
        public String delivery;
        public long evaluatiorSubmissionID;
        public String evaluationName;
        public String evaluationFile;
        public int? evaluationScore;
        public String privateFeedback;
        public String publicFeedback;
        public bool subIsEvaluated;
        public long evaluationsubmittedID;

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
