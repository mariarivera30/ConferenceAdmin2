using NancyService.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyService.Modules
{
    public class ReportManager
    {
        public ReportManager()
        {

        }

        //Heidi: Get the bill report for the payments made to the conference. 
        //Returns the transaction id, payment date, payment ammount, payment method, name, usertype, affiliation, email, address, and phone number.
        public ReportQuery getBillReportList()
        {
            ReportQuery b = new ReportQuery();
            String csv = "";

            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //Get registration payments, complimentary payments, and sponsor payments (in the order mentioned)
                    var payments = (from s in context.registrations
                                    from bill in context.paymentbills
                                    where (s.payment.deleted != true && s.paymentID == bill.paymentID && bill.completed != false)
                                    select new BillQuery
                                        {
                                            transactionID = bill.transactionid,
                                            paymentDate = bill.payment.creationDate.ToString(),
                                            amountPaid = bill.AmountPaid,
                                            paymentMethod = bill.methodOfPayment == "default" ? "" : bill.methodOfPayment,
                                            name = s.user.firstName + " " + s.user.lastName,
                                            email = s.user.membership.email == "default" ? "" : s.user.membership.email,
                                            affiliation = s.user.affiliationName == "Paradigm Innovation" ? "" : s.user.affiliationName,
                                            userType = s.user.usertype.userTypeName,
                                            phoneNumber = bill.telephone == "default" ? "" : bill.telephone,
                                            address1 = s.user.address.line1 == "default" ? "" : s.user.address.line1,
                                            address2 = s.user.address.line2 == "default" ? "" : s.user.address.line2,
                                            city = s.user.address.city == "default" ? "" : s.user.address.city,
                                            state = s.user.address.state == "default" ? "" : s.user.address.state,
                                            country = s.user.address.country == "default" ? "" : s.user.address.country,
                                            zipCode = s.user.address.line1 == "default" ? "" : s.user.address.zipcode

                                        }).Concat((from s in context.registrations
                                                   from bill in context.paymentcomplementaries
                                                   where (s.payment.deleted != true && s.paymentID == bill.paymentID)
                                                   select new BillQuery
                                                        {
                                                            transactionID = "N/A",
                                                            paymentDate = bill.payment.creationDate.ToString(),
                                                            amountPaid = 0,
                                                            paymentMethod = "Key:" + bill.complementarykey.key,
                                                            name = s.user.firstName + " " + s.user.lastName,
                                                            email = s.user.membership.email == "default" ? "" : s.user.membership.email,
                                                            affiliation = s.user.affiliationName == "Paradigm Innovation" ? "" : s.user.affiliationName,
                                                            userType = s.user.usertype.userTypeName,
                                                            phoneNumber = s.user.phone == "default" ? "" : s.user.phone,
                                                            address1 = s.user.address.line1 == "default" ? "" : s.user.address.line1,
                                                            address2 = s.user.address.line2 == "default" ? "" : s.user.address.line2,
                                                            city = s.user.address.city == "default" ? "" : s.user.address.city,
                                                            state = s.user.address.state == "default" ? "" : s.user.address.state,
                                                            country = s.user.address.country == "default" ? "" : s.user.address.country,
                                                            zipCode = s.user.address.line1 == "default" ? "" : s.user.address.zipcode

                                                        })).Concat((from s in context.sponsor2
                                                                    from bill in context.paymentbills
                                                                    where (s.payment.deleted != true && s.paymentID == bill.paymentID && s.sponsorID != 1 && s.paymentID != 1 && bill.completed != false && s.active != false)
                                                                    select new BillQuery
                                                                    {
                                                                         transactionID = bill.transactionid,
                                                                         paymentDate = bill.payment.creationDate.ToString(),
                                                                         amountPaid = bill.AmountPaid,
                                                                         paymentMethod = bill.methodOfPayment,
                                                                         name = s.user.firstName + " " + s.user.lastName,
                                                                         email = s.byAdmin == false ? s.user.membership.email : s.emailInfo,
                                                                         affiliation = s.user.affiliationName,
                                                                         userType = "Sponsor",
                                                                         phoneNumber = bill.telephone == "default" ? "" : bill.telephone,
                                                                         address1 = s.user.address.line1 == "default" ? "" : s.user.address.line1,
                                                                         address2 = s.user.address.line2 == "default" ? "" : s.user.address.line2,
                                                                         city = s.user.address.city == "default" ? "" : s.user.address.city,
                                                                         state = s.user.address.state == "default" ? "" : s.user.address.state,
                                                                         country = s.user.address.country == "default" ? "" : s.user.address.country,
                                                                         zipCode = s.user.address.line1 == "default" ? "" : s.user.address.zipcode
                                                 
                                                                    }));

                    if (payments.Count() > 0)
                    {
                        //Create csv string: Column titles
                            csv += ("\"Transaction ID\"," +
                                    "\"Payment Date\"," +
                                    "\"Amount Paid\"," +
                                    "\"Payment Method\"," +
                                    "\"Name\"," +
                                    "\"Email\"," +
                                    "\"Affiliation\"," +
                                    "\"User Type\"," +
                                    "\"Phone Number\"," +
                                    "\"Address Line 1\"," +
                                    "\"Address Line 2\"," +
                                    "\"City\"," +
                                    "\"State\"," +
                                    "\"Country\"," +
                                    "\"Zip Code\"\r\n");

                        foreach (var p in payments)
                        {
                            //Append to the csv string each record
                            csv += ("\"" + p.transactionID + "\"," +
                                    "\"" + p.paymentDate + "\"," +
                                    "\"" + p.amountPaid + "\"," +
                                    "\"" + p.paymentMethod + "\"," +
                                    "\"" + p.name + "\"," +
                                    "\"" + p.email + "\"," +
                                    "\"" + p.affiliation + "\"," +
                                    "\"" + p.userType + "\"," +
                                    "\"" + p.phoneNumber + "\"," +
                                    "\"" + p.address1 + "\"," +
                                    "\"" + p.address2 + "\"," +
                                    "\"" + p.city + "\"," +
                                    "\"" + p.state + "\"," +
                                    "\"" + p.country + "\"," +
                                    "\"" + p.zipCode + "\"\r\n");
                        }
                        
                        b.results = csv;
                    }
                }

                return b;

            }
            catch (Exception ex)
            {
                Console.Write("WebManager.getBillReport error " + ex);
                return null;
            }
        }

        //Heidi: Get registrations
        //Returns the transaction id, payment date, payment method payment ammount, name, usertype, affiliation, email.
        public BillPagingQuery getRegistrationPayments(int index)
        {
            BillPagingQuery page = new BillPagingQuery();

            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    int pageSize = 10;
                    //Get registration both the paid registrations, and the complimentary registrations
                    var query = (from s in context.registrations
                                 from bill in context.paymentcomplementaries
                                 where (s.payment.deleted != true && s.paymentID == bill.paymentID)
                                 select new BillQuery
                                    {
                                        transactionID = "N/A",
                                        paymentDate = bill.payment.creationDate.ToString(),
                                        name = s.user.firstName + " " + s.user.lastName,
                                        email=s.user.membership.email,
                                        affiliation = s.user.affiliationName,
                                        userType = s.user.usertype.userTypeName,
                                        amountPaid = 0,
                                        paymentMethod = "Complimentary Key:    " + bill.complementarykey.key

                                     }).Concat((from s in context.registrations
                                                from bill in context.paymentbills
                                                where (s.payment.deleted != true && s.paymentID == bill.paymentID && bill.completed != false)
                                                select new BillQuery
                                                    {
                                                        transactionID = bill.transactionid,
                                                        paymentDate = bill.payment.creationDate.ToString(),
                                                        name = s.user.firstName + " " + s.user.lastName,
                                                        email=s.user.membership.email,
                                                        affiliation = s.user.affiliationName,
                                                        userType = s.user.usertype.userTypeName,
                                                        amountPaid = bill.AmountPaid,
                                                        paymentMethod = bill.methodOfPayment
                                                    })).OrderBy(x => x.name);
                    
                    //Paging->Filter results to a limit of 10 records per page
                    page.rowCount= query.Count();
                    if (page.rowCount > 0)
                    {
                        page.maxIndex = (int)Math.Ceiling(page.rowCount / (double)pageSize);
                        var registrationPayments = query.Skip(pageSize * index).Take(pageSize).ToList(); //Skip past rows and take new elements
                        page.results = registrationPayments;
                    }
                }

                return page;

            }

            catch (Exception ex)
            {
                Console.Write("WebManager.getRegistrationPayments error " + ex);
                return null;
            }
        }

        //Heidi: Get the Sponsor Payments
        //Returns the transaction id, payment date, payment method payment ammount, name, usertype, affiliation, email.
        public BillPagingQuery getSponsorPayments(int index)
        {
            BillPagingQuery page = new BillPagingQuery();

            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    int pageSize = 10;
                    //Get sponsor payments
                    var query = (from s in context.sponsor2
                                 from bill in context.paymentbills
                                 where (s.payment.deleted != true && s.paymentID == bill.paymentID && s.sponsorID != 1 && s.paymentID != 1 && bill.completed != false && s.active != false)
                                 select new BillQuery
                                 {
                                     transactionID = bill.transactionid,
                                     paymentDate = bill.payment.creationDate.ToString(),
                                     name = s.user.firstName + " " + s.user.lastName,
                                     email = s.byAdmin == false ? s.user.membership.email : s.emailInfo,
                                     affiliation = s.user.affiliationName,
                                     userType = "Sponsor",
                                     sponsorType= s.sponsortype1.name,
                                     amountPaid = bill.AmountPaid,
                                     paymentMethod = bill.methodOfPayment
                                 }).OrderBy(x => x.name);

                    //Paging->Filter results to a limit of 10 records per page
                    page.rowCount = query.Count();
                    if (page.rowCount > 0)
                    {
                        page.maxIndex = (int)Math.Ceiling(page.rowCount / (double)pageSize);
                        var registrationPayments = query.Skip(pageSize * index).Take(pageSize).ToList(); //Skip past rows and take new elements
                        page.results = registrationPayments;
                    }
                }

                return page;

            }

            catch (Exception ex)
            {
                Console.Write("WebManager.getSponsorPayments error " + ex);
                return null;
            }
        }

        //Heidi: Search reports that meet specified criteria
        public BillPagingQuery searchReport(int index, String criteria)
        {
            BillPagingQuery b = new BillPagingQuery();

            try
            {
                int pageSize = 10;
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //Get registrations, and sponsor payments
                    var payments = (from s in context.registrations
                                    from bill in context.paymentbills
                                    where ((s.payment.deleted != true && s.paymentID == bill.paymentID) && ((s.user.firstName.ToLower() + " " + s.user.lastName.ToLower()).Contains(criteria.ToLower()) || s.user.membership.email.ToLower().Contains(criteria.ToLower())))
                                    select new BillQuery
                                    {
                                        transactionID = bill.transactionid,
                                        paymentDate = bill.payment.creationDate.ToString(),
                                        name = s.user.firstName + " " + s.user.lastName,
                                        email= s.user.membership.email,
                                        affiliation = s.user.affiliationName,
                                        userType = s.user.usertype.userTypeName,
                                        amountPaid = bill.AmountPaid,
                                        paymentMethod = bill.methodOfPayment

                                    }).Concat((from s in context.registrations
                                               from bill in context.paymentcomplementaries
                                               where ((s.payment.deleted != true && s.paymentID == bill.paymentID) && ((s.user.firstName.ToLower() + " " + s.user.lastName.ToLower()).Contains(criteria.ToLower()) || s.user.membership.email.ToLower().Contains(criteria.ToLower())))
                                               select new BillQuery
                                               {
                                                   transactionID = "N/A",
                                                   paymentDate = bill.payment.creationDate.ToString(),
                                                   name = s.user.firstName + " " + s.user.lastName,
                                                   email= s.user.membership.email,
                                                   affiliation = s.user.affiliationName,
                                                   userType = s.user.usertype.userTypeName,
                                                   amountPaid = 0,
                                                   paymentMethod = "Complimentary Key:    " + bill.complementarykey.key

                                               })).Concat((from s in context.sponsor2
                                                           from bill in context.paymentbills
                                                           where ((s.payment.deleted != true && s.paymentID == bill.paymentID && s.sponsorID != 1 && s.paymentID != 1 && bill.completed != false && s.active != false) && ((s.user.firstName.ToLower() + " " + s.user.lastName.ToLower()).Contains(criteria.ToLower()) || s.emailInfo.ToLower().Contains(criteria.ToLower()) || s.user.membership.email.ToLower().Contains(criteria.ToLower())))
                                                           select new BillQuery
                                                           {
                                                               transactionID = bill.transactionid,
                                                               paymentDate = bill.payment.creationDate.ToString(),
                                                               name = s.user.firstName + " " + s.user.lastName,
                                                               email = s.byAdmin == false ? s.user.membership.email : s.emailInfo,
                                                               affiliation = s.user.affiliationName,
                                                               userType = "Sponsor",
                                                               amountPaid = bill.AmountPaid,
                                                               paymentMethod = bill.methodOfPayment
                                                           })).OrderBy(x => x.name);
                    //Paging->Filter results to a limit of 10 records per page
                    if (payments.Count() > 0)
                    {
                        b.maxIndex = (int)Math.Ceiling(payments.Count() / (double)pageSize);
                        var report = payments.Skip(pageSize * index).Take(pageSize).ToList(); //Skip past rows and take new elements
                        b.results = report;
                    }

                }

                return b;

            }
            catch (Exception ex)
            {
                Console.Write("WebManager.searchReport error " + ex);
                return null;
            }
        }

        //Heidi: get record of users that have registered for the conference
        //Returns registrationID, name, email, phone, usertype, dates of attendance, affiliation, address and special notes
        public ReportQuery getAttendanceReport()
        {
            ReportQuery b = new ReportQuery();
            String csv = "";
            RegistrationManager registration = new RegistrationManager();
            List<String> conferenceDates = registration.getDates();

            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //Get conference registrations A.Get adults, B. Get minors
                    var registrationList = new List<RegisteredUserInformation>();
                    registrationList = (from reg in context.registrations
                                        where reg.deleted == false && context.minors.Where(m => m.userID == reg.userID).Select(m=>m.userID).Count() == 0
                                        select new RegisteredUserInformation
                                        {
                                            registrationID = reg.registrationID,
                                            name = reg.user.firstName+ " " +reg.user.lastName,
                                            email = reg.byAdmin == true ? "" : reg.user.membership.email,
                                            phone = reg.byAdmin == true ? "" : reg.user.phone,
                                            usertypeid = reg.user.usertype.userTypeName,
                                            companion = "",
                                            date1 = reg.date1,
                                            date2 = reg.date2,
                                            date3 = reg.date3,
                                            affiliationName = reg.user.affiliationName,
                                            line1 = reg.byAdmin == true ? "" : reg.user.address.line1,
                                            line2 = reg.byAdmin == true ? "" : reg.user.address.line2,
                                            city = reg.byAdmin == true ? "" : reg.user.address.city,
                                            state = reg.byAdmin == true ? "" : reg.user.address.state,
                                            country = reg.byAdmin == true ? "" : reg.user.address.country,
                                            zipCode = reg.byAdmin == true ? "" : reg.user.address.zipcode,
                                            notes = reg.note,
                                            usertype = reg.user.usertype.userTypeName

                                        }).Concat((from reg in context.registrations
                                                   from c in context.companionminors
                                                   from m in context.minors
                                                   where reg.deleted == false && m.minorsID == c.minorID && reg.userID == m.userID
                                                   select new RegisteredUserInformation
                                                   {
                                                       registrationID = reg.registrationID,
                                                       name = reg.user.firstName + " " + reg.user.lastName,
                                                       email = reg.byAdmin == true ? "" : reg.user.membership.email,
                                                       phone = reg.byAdmin == true ? "" : reg.user.phone,
                                                       usertypeid = reg.user.usertype.userTypeName,
                                                       companion = c.companion.user.firstName+" "+ c.companion.user.lastName,
                                                       date1 = reg.date1,
                                                       date2 = reg.date2,
                                                       date3 = reg.date3,
                                                       affiliationName = reg.user.affiliationName,
                                                       line1 = reg.byAdmin == true ? "" : reg.user.address.line1,
                                                       line2 = reg.byAdmin == true ? "" : reg.user.address.line2,
                                                       city = reg.byAdmin == true ? "" : reg.user.address.city,
                                                       state = reg.byAdmin == true ? "" : reg.user.address.state,
                                                       country = reg.byAdmin == true ? "" : reg.user.address.country,
                                                       zipCode = reg.byAdmin == true ? "" : reg.user.address.zipcode,
                                                       notes = reg.note,
                                                       usertype = reg.user.usertype.userTypeName

                                                   })).OrderBy(f => f.registrationID).ToList();

                    //Converto to csv string
                    if (registrationList.Count() > 0)
                    {
                            //Column titles
                            csv += ("\"Registration ID\"," +
                                    "\"Name\"," +
                                    "\"Email\"," +
                                    "\"Phone Number\"," +
                                    "\"User Type\"," +
                                    "\"Companion\",");

                            //Add each date of the conference as column title
                            if (conferenceDates.Count() > 0)
                            {
                                string[] date = conferenceDates[0].Split(',');
                                if(date.Count() ==3)
                                csv += ("\""+date[1]+","+date[2]+"\",");
                            }
                            if (conferenceDates.Count() > 1)
                            {
                                string[] date = conferenceDates[1].Split(',');
                                if (date.Count() == 3)
                                csv += ("\"" + date[1] + "," + date[2] + "\",");
                            }
                            if (conferenceDates.Count() > 2)
                            {
                                string[] date = conferenceDates[2].Split(',');
                                if (date.Count() == 3)
                                csv += ("\"" + date[1] + "," + date[2] + "\",");
                            }

                            csv += ("\"Affiliation\"," +
                                    "\"Address Line 1\"," +
                                    "\"Address Line 2\"," +
                                    "\"City\"," +
                                    "\"State\"," +
                                    "\"Country\"," +
                                    "\"Zip Code\"," +
                                    "\"Notes\"\r\n");

                            //Append each registration to csv string. 
                            foreach (var p in registrationList)
                            {
                            csv += ("\"" + p.registrationID + "\"," +
                                    "\"" + p.name + "\"," +
                                    "\"" + p.email + "\"," +
                                    "\"" + p.phone + "\"," +
                                    "\"" + p.usertype + "\"," +
                                    "\"" + p.companion + "\",");

                            //Mark days the user will attend the conference.
                            if (conferenceDates.Count() > 0 && conferenceDates[0].Split(',').Count() ==3)
                            {
                                string date = p.date1 == true ? "X" : "";
                                csv += "\"" + date + "\",";
                            }
                            if (conferenceDates.Count() > 1 && conferenceDates[1].Split(',').Count() ==3)
                            {
                                string date = p.date2 == true ? "X" : "";
                                csv += "\"" + date + "\",";
                            }
                            if (conferenceDates.Count() > 2 && conferenceDates[2].Split(',').Count() == 3)
                            {
                                string date = p.date3 == true ? "X" : "";
                                csv += "\"" + date + "\",";
                            }

                            csv +=  ("\"" + p.affiliationName + "\"," +
                                    "\"" + p.line1 + "\"," +
                                    "\"" + p.line2 + "\"," +
                                    "\"" + p.city + "\"," +
                                    "\"" + p.state + "\"," +
                                    "\"" + p.country + "\"," +
                                    "\"" + p.zipCode + "\"," +
                                    "\"" + p.notes + "\"\r\n");
                        }

                        b.results = csv;
                    }
                }

                return b;

            }
            catch (Exception ex)
            {
                Console.Write("WebManager.getAttendanceReport error " + ex);
                return null;
            }
        }

        //Jaimeiris- Get submission reports. Returns submission title, topic, status, userID, submissionID
        public String getSubmissionsReport()
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
                            numOfEvaluations = 0;
                        }
                        else
                        {
                            userSubmissions.Add(new Submission(userID, submissionID, submissionTypeName,
                            submissionTypeID, submissionTitle, topiccategoryID, topic, status, 0, numOfEvaluations, byAdmin));
                            numOfEvaluations = 0;
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
                            numOfEvaluations = 0;
                        }
                        else
                        {
                            userSubmissions.Add(new Submission(userID, submissionID, submissionTypeName,
                            submissionTypeID, submissionTitle, topiccategoryID, topic, status, 0, numOfEvaluations, byAdmin));
                            numOfEvaluations = 0;
                        }
                    }
                    userSubmissions = userSubmissions.OrderBy(c => -c.avgScore).ToList();
                    String csv = "";
                    csv += (        "\"Title\"," +
                                    "\"Type of Submission\"," +
                                    "\"Topic\"," +
                                    "\"Average Score\"," +
                                    "\"Status\"," +
                                    "\"Number of Evaluations\"\r\n");
                    foreach (var sub in userSubmissions)
                    {
                        csv += ("\"" + sub.submissionTitle + "\"," +
                                "\"" + sub.submissionTypeName + "\"," +
                                "\"" + sub.topic + "\"," +
                                "\"" + sub.avgScore + "\"," +
                                "\"" + sub.status + "\"," +
                                "\"" + sub.numOfEvaluations + "\"\r\n");
                    }

                    return csv;
                }
            }
            catch (Exception ex)
            {
                Console.Write("SubmissionManager.getAllSubmissions error " + ex);
                return null;
            }
        }
    }

    public class BillQuery
    {
        public String csvString;
        public String transactionID;
        public String paymentDate;
        public String name;
        public String email;
        public String affiliation;
        public String userType;
        public double amountPaid;
        public String paymentMethod;
        public String phoneNumber;
        public String address1;
        public String address2;
        public String city;
        public String state;
        public String country;
        public String zipCode;
        public String sponsorType;

    }

    public class BillPagingQuery
    {
        public int indexPage;
        public int maxIndex;
        public int rowCount;
        public List<BillQuery> results;

        public BillPagingQuery()
        {
            results = new List<BillQuery>();
        }
    }

    public class ReportQuery
    {
        public String results;
        public double totalAmount;
        public int maxIndex;

        public ReportQuery()
        {
        }
    }

    public class RegisteredUserInformation
    {
        public long registrationID;
        public string name;
        public string phone;
        public string email;
        //Address
        public string line1;
        public string line2;
        public string city;
        public string state;
        public string country;
        public string zipCode;
        //General
        public string usertypeid;
        public String companion;
        public bool? date1;
        public bool? date2;
        public bool? date3;
        public string affiliationName;
        public bool? byAdmin;
        public string usertype;
        public string notes;

        public RegisteredUserInformation() { }

    }
        
}