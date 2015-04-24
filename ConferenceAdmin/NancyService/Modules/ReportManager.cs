using NancyService.Models;
using System;
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

        public BillReportQuery getBillReportList(int index)
        {
            BillReportQuery b = new BillReportQuery();

            try
            {
                int pageSize = 10;
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var payments = (from s in context.registrations
                                    from bill in context.paymentbills
                                    where (s.payment.deleted != true && s.paymentID == bill.paymentID)
                                    select new BillQuery
                                        {
                                            transactionID = bill.transactionid,
                                            paymentDate = bill.payment.creationDate.ToString(),
                                            name = s.user.firstName + " " + s.user.lastName,
                                            affiliation = s.user.affiliationName,
                                            userType = s.user.usertype.userTypeName,
                                            amountPaid = bill.AmountPaid,
                                            paymentMethod = bill.methodOfPayment

                                        }).Concat((from s in context.registrations
                                                   from bill in context.paymentcomplementaries
                                                   where (s.payment.deleted != true && s.paymentID == bill.paymentID)
                                                   select new BillQuery
                                                        {
                                                            transactionID = "N/A",
                                                            paymentDate = bill.payment.creationDate.ToString(),
                                                            name = s.user.firstName + " " + s.user.lastName,
                                                            affiliation = s.user.affiliationName,
                                                            userType = s.user.usertype.userTypeName,
                                                            amountPaid = 0,
                                                            paymentMethod = "Complimentary Key:    " + bill.complementarykey.key

                                                        })).Concat((from s in context.sponsors
                                                                    from bill in context.paymentbills
                                                                    where (s.payment.deleted != true && s.paymentID == bill.paymentID && s.paymentID != 1)
                                                                    select new BillQuery
                                                                    {
                                                                         transactionID = bill.transactionid,
                                                                         paymentDate = bill.payment.creationDate.ToString(),
                                                                         name = s.firstName + " " + s.lastName,
                                                                         affiliation = s.company,
                                                                         userType = "Sponsor",
                                                                         amountPaid = bill.AmountPaid,
                                                                         paymentMethod = bill.methodOfPayment
                                                                    })).OrderBy(x => x.name);

                    if (payments.Count() > 0)
                    {
                        b.maxIndex = (int)Math.Ceiling(payments.Count() / (double)pageSize);
                        var report = payments.Skip(pageSize * index).Take(pageSize).ToList(); //Skip past rows and take new elements
                        b.results = report;
                    }

                    b.totalAmount = context.paymentbills.Where(x => x.deleted != true).Sum(x => x.AmountPaid);
                }

                return b;

            }
            catch (Exception ex)
            {
                Console.Write("WebManager.getBillReport error " + ex);
                return null;
            }
        }

        public BillPagingQuery getRegistrationPayments(int index)
        {
            BillPagingQuery page = new BillPagingQuery();

            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    int pageSize = 10;
                    var query = (from s in context.registrations
                                 from bill in context.paymentcomplementaries
                                 where (s.payment.deleted != true && s.paymentID == bill.paymentID)
                                 select new BillQuery
                                    {
                                        transactionID = "N/A",
                                        paymentDate = bill.payment.creationDate.ToString(),
                                        name = s.user.firstName + " " + s.user.lastName,
                                        affiliation = s.user.affiliationName,
                                        userType = s.user.usertype.userTypeName,
                                        amountPaid = 0,
                                        paymentMethod = "Complimentary Key:    " + bill.complementarykey.key

                                     }).Concat((from s in context.registrations
                                                from bill in context.paymentbills
                                                where (s.payment.deleted != true && s.paymentID == bill.paymentID)
                                                select new BillQuery
                                                    {
                                                        transactionID = bill.transactionid,
                                                        paymentDate = bill.payment.creationDate.ToString(),
                                                        name = s.user.firstName + " " + s.user.lastName,
                                                        affiliation = s.user.affiliationName,
                                                        userType = s.user.usertype.userTypeName,
                                                        amountPaid = bill.AmountPaid,
                                                        paymentMethod = bill.methodOfPayment
                                                    })).OrderBy(x => x.name);
                    
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

        public BillPagingQuery getSponsorPayments(int index)
        {
            BillPagingQuery page = new BillPagingQuery();

            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    int pageSize = 10;
                    var query = (from s in context.sponsors
                                 from bill in context.paymentbills
                                 where (s.payment.deleted != true && s.paymentID == bill.paymentID && s.paymentID != 1)
                                 select new BillQuery
                                 {
                                     transactionID = bill.transactionid,
                                     paymentDate = bill.payment.creationDate.ToString(),
                                     name = s.firstName + " " + s.lastName,
                                     affiliation = s.company,
                                     userType = "Sponsor",
                                     amountPaid = bill.AmountPaid,
                                     paymentMethod = bill.methodOfPayment
                                 }).OrderBy(x => x.name); 

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

        public BillReportQuery searchReport(int index, String criteria)
        {
            BillReportQuery b = new BillReportQuery();

            try
            {
                int pageSize = 10;
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var payments = (from s in context.registrations
                                    from bill in context.paymentbills
                                    where ((s.payment.deleted != true && s.paymentID == bill.paymentID) && ((s.user.firstName.ToLower() + " " + s.user.lastName.ToLower()).Contains(criteria.ToLower()) || s.user.membership.email.ToLower().Contains(criteria.ToLower())))
                                    select new BillQuery
                                    {
                                        transactionID = bill.transactionid,
                                        paymentDate = bill.payment.creationDate.ToString(),
                                        name = s.user.firstName + " " + s.user.lastName,
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
                                                   affiliation = s.user.affiliationName,
                                                   userType = s.user.usertype.userTypeName,
                                                   amountPaid = 0,
                                                   paymentMethod = "Complimentary Key:    " + bill.complementarykey.key

                                               })).Concat((from s in context.sponsors
                                                           from bill in context.paymentbills
                                                           where ((s.payment.deleted != true && s.paymentID == bill.paymentID) && ((s.firstName.ToLower() + " " + s.lastName.ToLower()).Contains(criteria.ToLower()) || s.email.ToLower().Contains(criteria.ToLower())))
                                                           select new BillQuery
                                                           {
                                                               transactionID = bill.transactionid,
                                                               paymentDate = bill.payment.creationDate.ToString(),
                                                               name = s.firstName + " " + s.lastName,
                                                               affiliation = s.company,
                                                               userType = "Sponsor",
                                                               amountPaid = bill.AmountPaid,
                                                               paymentMethod = bill.methodOfPayment
                                                           })).OrderBy(x => x.name);

                    if (payments.Count() > 0)
                    {
                        b.maxIndex = (int)Math.Ceiling(payments.Count() / (double)pageSize);
                        var report = payments.Skip(pageSize * index).Take(pageSize).ToList(); //Skip past rows and take new elements
                        b.results = report;
                    }

                    b.totalAmount = context.paymentbills.Where(x => x.deleted != true).Sum(x => x.AmountPaid);
                }

                return b;

            }
            catch (Exception ex)
            {
                Console.Write("WebManager.searchReport error " + ex);
                return null;
            }
        }


    }

    public class BillQuery
    {

        public String transactionID;
        public String paymentDate;
        public String name;
        public String affiliation;
        public String userType;
        public double amountPaid;
        public String paymentMethod;

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

    public class BillReportQuery
    {
        public List<BillQuery> results;
        public double totalAmount;
        public int maxIndex;

        public BillReportQuery()
        {
            results = new List<BillQuery>();
        }
    }
        
}