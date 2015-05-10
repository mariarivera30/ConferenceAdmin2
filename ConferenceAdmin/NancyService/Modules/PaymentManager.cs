using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System;
using System.IO;
using System.Text.RegularExpressions;
using NancyService.Models;


/*Created by: Maria Rivera
 * This module contains the implementation of the function use by the PaymentModule 
 * 
 
 */
namespace NancyService.Modules
{
    public class PaymentQuery
    {
        public long paymentBillID { get; set; }
        public DateTime date { get; set; }
        public string transactionid { get; set; }
        public double AmountPaid { get; set; }
        public string authorizationID { get; set; }
        public string methodOfPayment { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string tandemID { get; set; }
        public string batchID { get; set; }
        public string ip { get; set; }
        public string telephone { get; set; }
        public string userFirstName { get; set; }
        public string userLastName { get; set; }
        public string affiliationName { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string complementaryKey { get; set; }
        public string sponsorBy { get; set; }
    }
    public class XMLReceiptInfo
    {

        public string transactionID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string merchantName { get; set; }
        public string merchantURL { get; set; }
        public string tandemID { get; set; }
        public string batchId { get; set; }
        public string transactionType { get; set; }
        public string error { get; set; }
        public string message { get; set; }
        public string methodOfPayment { get; set; }
    }

    public class xmlTransacctionID
    {
        public string error { get; set; }
        public string transactionID { get; set; }
    }

    public class PaymentXML
    {

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string city { get; set; }
        public string zipCode { get; set; }
        public string phone { get; set; }
        public string quantity { get; set; }
        public string IP { get; set; }
        public long paymentID { get; set; }
        public string productID { get; set; }



    }
    public class PaymentManager
    {   //RECA0185 Sponsor
        private string sponsorProductID = "RECA0185";//registro
        private string userProductID = "RECA0186";//registro
        private string athorizationID = "DMKRZ72";
        private string version = "1.1.0";
        public PaymentManager()
        { }

        private bool getDeadlineStatus(string deadlineName)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {

                    WebManager webManager = new WebManager();
                    string deadline = webManager.getInterfaceElement(deadlineName).content;

                    var Day = Convert.ToInt32(deadline.Split('/')[1]);
                    var Month = Convert.ToInt32(deadline.Split('/')[0]);
                    var Year = Convert.ToInt32(deadline.Split('/')[2]);

                    DateTime submissionDeadline = new DateTime(Year, Month, Day);


                    return (DateTime.Compare(submissionDeadline, DateTime.Now.Date) >= 0);
                }
            }
            catch (Exception ex)
            {
                Console.Write("PaymentManager.getAmountInDeadline error " + ex);
                return false;
            }
        }
        public AmountSatusRegistration getUserPriceInDeadline(int typeID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var type = context.usertypes.Where(x => x.userTypeID == typeID).First();
                    AmountSatusRegistration amountStatus = new AmountSatusRegistration();
                    if (getDeadlineStatus("registrationDeadline"))
                    {
                        amountStatus.amount = (double)type.registrationCost;
                        amountStatus.inTime = true;
                        amountStatus.inTimeLateFee = false;

                    }
                    else if (getDeadlineStatus("lateRegistrationDeadline"))
                    {
                        amountStatus.amount = (double)type.registrationLateFee;
                        amountStatus.inTime = false;
                        amountStatus.inTimeLateFee = true;

                    }
                    else
                    {
                        amountStatus.inTime = false;
                        amountStatus.inTimeLateFee = false;

                    }

                    return amountStatus;
                }
            }
            catch (Exception ex)
            {
                Console.Write("PaymentManager.getAmountInDeadline error " + ex);
                return null;
            }
        }




        public List<PaymentQuery> getSponsorPayments(long id)
        {

            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var paymentInfo = (from s in context.paymentbills
                                       from sp in context.sponsor2
                                       where sp.userID == id && s.deleted == false && sp.paymentID == s.paymentID && s.completed == true
                                       select new PaymentQuery
                                       {
                                           paymentBillID = s.paymentBillID,
                                           date = s.date,
                                           transactionid = s.transactionid,
                                           AmountPaid = s.AmountPaid,
                                           methodOfPayment = s.methodOfPayment,
                                           firstName = s.firstName,
                                           lastName = s.lastName,
                                           email = s.email,
                                           tandemID = s.tandemID,
                                           batchID = s.batchID,
                                           userFirstName = sp.user.firstName,
                                           userLastName = sp.user.lastName,
                                           affiliationName = sp.user.affiliationName,
                                           type = sp.sponsortype1.name,
                                           description = "Sponsor Donation"

                                       }).ToList();



                    return paymentInfo;
                }

            }
            catch (Exception ex)
            {
                Console.Write("PaymetnManager.getPaymentSponsorsReceiptInfo error " + ex);
                return null;
            }


        }

        public PaymentQuery getPayment(long id)
        {

            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var paymentInfo = (from s in context.paymentbills
                                       from sp in context.sponsor2
                                       where s.paymentBillID == id && s.deleted == false && sp.paymentID == s.paymentID && s.completed == true
                                       select new PaymentQuery
                                       {
                                           paymentBillID = s.paymentBillID,
                                           date = s.date,
                                           affiliationName = sp.user.affiliationName,
                                           transactionid = s.transactionid,
                                           AmountPaid = s.AmountPaid,
                                           methodOfPayment = s.methodOfPayment,
                                           firstName = s.firstName,
                                           lastName = s.lastName,
                                           email = s.email,
                                           tandemID = s.tandemID,
                                           batchID = s.batchID,
                                           userFirstName = sp.user.firstName,
                                           userLastName = sp.user.lastName,
                                           type = sp.sponsortype1.name,
                                           description = "Sponsor Donation"

                                       }).FirstOrDefault();


                    if (paymentInfo == null)
                    {
                        paymentInfo = (from s in context.paymentbills
                                       from r in context.registrations
                                       where s.paymentBillID == id && s.deleted == false && r.paymentID == s.paymentID && s.completed == true
                                       select new PaymentQuery
                                       {
                                           paymentBillID = s.paymentBillID,
										   date = s.date,
                                           transactionid = s.transactionid,
                                           AmountPaid = s.AmountPaid,
                                           methodOfPayment = s.methodOfPayment,
                                           firstName = s.firstName,
                                           lastName = s.lastName,
                                           email = s.email,
                                           tandemID = s.tandemID,
                                           batchID = s.batchID,
                                           userFirstName = r.user.firstName,
                                           userLastName = r.user.lastName,
                                           affiliationName = r.user.affiliationName,
                                           type = r.user.usertype.userTypeName,
                                           description = "User Registration."
                                       }).FirstOrDefault();
                    }

                    if (paymentInfo == null)
                    {
                        PaymentQuery error = new PaymentQuery();
                        error.paymentBillID = -1;//notify not found
                        return error;
                    }
                    return paymentInfo;
                }

            }
            catch (Exception ex)
            {
                Console.Write("PaymetnManager.getPaymentReceiptInfo error " + ex);
                return null;
            }
        }

        public PaymentQuery getUserPayment(long id)
        {

            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var paymentInfo = (from s in context.paymentbills
                                       from sp in context.registrations
                                       where s.paymentID == sp.paymentID && s.deleted == false && sp.userID == id && s.completed == true
                                       select new PaymentQuery
                                       {
                                           paymentBillID = s.paymentBillID,
                                           date = s.date,
                                           affiliationName = sp.user.affiliationName,
                                           transactionid = s.transactionid,
                                           AmountPaid = s.AmountPaid,
                                           methodOfPayment = s.methodOfPayment,
                                           firstName = s.firstName,
                                           lastName = s.lastName,
                                           email = s.email,
                                           tandemID = s.tandemID,
                                           batchID = s.batchID,
                                           userFirstName = sp.user.firstName,
                                           userLastName = sp.user.lastName,
                                           type = sp.user.usertype.userTypeName,
                                           description = "User Registration"

                                       }).FirstOrDefault();

                    if (paymentInfo == null)
                    {

                        paymentInfo = (from r in context.paymentcomplementaries
                                       join p in context.complementarykeys on r.complementaryKeyID equals p.complementarykeyID
                                       join pay in context.payments on r.paymentID equals pay.paymentID
                                       join y in context.sponsor2 on p.sponsorID2 equals y.sponsorID
                                       join x in context.registrations on r.paymentID equals x.paymentID
                                       where id == x.userID && x.deleted == false && r.deleted == false && p.deleted == false && pay.deleted == false
                                       select new PaymentQuery
                                       {
                                           paymentBillID = r.paymentcomplementaryID,
                                           complementaryKey = p.key,
                                           date = (DateTime)pay.creationDate,
                                           affiliationName = x.user.affiliationName,
                                           transactionid = "N/A",
                                           AmountPaid = 0,
                                           methodOfPayment = "Complementary Key",
                                           userFirstName = x.user.firstName,
                                           userLastName = x.user.lastName,
                                           email = x.user.membership.email,
                                           tandemID = "N/A",
                                           batchID = "N/A",
                                           firstName = y.user.firstName,
                                           lastName = y.user.lastName,
                                           description = "User Registration",
                                           type = x.user.usertype.userTypeName,

                                       }).FirstOrDefault();

                    }
                    if (paymentInfo == null)
                    {
                        PaymentQuery error = new PaymentQuery();
                        error.paymentBillID = -1;///not found
                        return error;
                    }
                    else { return paymentInfo; }
                }

            }
            catch (Exception ex)
            {
                Console.Write("PaymetnManager.getPaymentReceiptInfo error " + ex);

                return null;
            }
        }


        // Method used for generate a string need by securesystem with payment Information
        public string creatXML(PaymentXML payment)
        {
            string xml = "<VERSION>" + version + "</VERSION>\n" +
                "<AUTHORIZATIONID>" + athorizationID + "</AUTHORIZATIONID>\n" +
                "<CLIENTFIRSTNAME>" + payment.firstName + "</CLIENTFIRSTNAME>\n" +
                "<CLIENTLASTNAME>" + payment.lastName + "</CLIENTLASTNAME>\n" +
                "<EMAIL>" + payment.email + "</EMAIL>\n" +
                "<ADDR1>" + payment.line1 + "</ADDR1>\n" +
                "<ADDR2>" + payment.line2 + "</ADDR2>\n" +
                "<CITY>" + payment.city + "</CITY>\n" +
                "<ZIPCODE>" + payment.zipCode + "</ZIPCODE>\n" +
                "<TELEPHONE>" + payment.phone + "</TELEPHONE>\n" +
                "<QUANTITY>" + payment.quantity + "</QUANTITY>\n" +
                "<IP>" + payment.IP + "</IP>\n" +
                "<PRODUCTID>" + payment.productID + "</PRODUCTID>\n";

            return xml;
        }


        public xmlTransacctionID MakeWebServiceCall(PaymentXML payment)
        {
            // this is what we are sending
            //string post_data = "foo=bar&baz=oof";
            string post_data = "xml=" + creatXML(payment);

            // this is where we will send it
            //http://secure2.uprm.edu/secure/inittrans.php
            string uri = "https://secure2.uprm.edu/secure/inttrans.php";

            // create a request
            HttpWebRequest request = (HttpWebRequest)
                WebRequest.Create(uri); request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";

            // turn our request string into a byte stream
            byte[] postBytes = Encoding.UTF8.GetBytes(post_data);

            // this is important - make sure you specify type this way
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postBytes.Length;
            Stream requestStream = request.GetRequestStream();

            // now send it
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            // grab te response and print it out to the console along with the status code
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //  if (((HttpWebResponse)response).StatusCode != HttpStatusCode.OK)


            StreamReader reader = new StreamReader(response.GetResponseStream());

            // Read the whole contents and return as a string  
            string responseStr = reader.ReadToEnd();

            xmlTransacctionID xmlTransaction = parseXMLTransacctionID(responseStr);


            reader.Close();
            response.Close();
            return xmlTransaction;



        }
        //This method is reponsible to create a paymentBill with the transaction ID and status completed == false
        public void createPaymentBill(PaymentInfo info, string transactionID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    double quantity = info.amount * 100;
                    paymentbill bill = new paymentbill();
                    bill.AmountPaid = info.amount;
                    bill.paymentID = info.paymentID;
                    bill.completed = false;
                    bill.transactionid = transactionID;
                    bill.quantity = (int)quantity;
                    bill.deleted = false;
                    bill.date = DateTime.Now;
                    bill.telephone = info.phone;
                    context.paymentbills.Add(bill);
                    context.SaveChanges();


                }
            }
            catch (Exception ex)
            {
                Console.Write("PaymentManager.makePayment error " + ex);

            }

        }
        //This method is call from reentry with the receipt information sent by bank
        public long storePaymentBill(XMLReceiptInfo receipt)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var bill = (from s in context.paymentbills
                                where s.transactionid == receipt.transactionID
                                select s).FirstOrDefault();
                    if (bill != null)
                    {
                        bill.tandemID = receipt.tandemID;
                        bill.methodOfPayment = receipt.transactionType;
                        bill.batchID = receipt.batchId;
                        bill.email = receipt.email;
                        bill.firstName = receipt.firstName;
                        bill.lastName = receipt.lastName;
                        bill.deleted = false;
                        bill.completed = true;
                        bill.date = DateTime.Now;
                        // bill.telephone = ;
                        context.SaveChanges();

                        var sponsor1 = bill.payment.sponsors2.FirstOrDefault();
                        if (sponsor1 != null)
                        {
                            List<paymentbill> bills = sponsor1.payment.paymentbills.ToList();
                            if (bills.Count() > 0)
                            {
                                double total = 0;
                                foreach (paymentbill b in bills)
                                {
                                    if (b.completed)
                                        total += b.AmountPaid;
                                }

                                sponsor1.active = true;
                                sponsor1.totalAmount = total;
                                //this loop update sposorType after paymentIs completed
                                var sponsorTypes = context.sponsortypes.ToArray();


                                if (sponsor1.totalAmount >= sponsorTypes[0].amount)
                                {
                                    sponsor1.sponsorType = 1;
                                }

                                else if (sponsorTypes[4].amount >= sponsor1.totalAmount && sponsor1.totalAmount <= sponsorTypes[3].amount - 1)
                                {
                                    sponsor1.sponsorType = 5;
                                }

                                else if (sponsor1.totalAmount >= sponsorTypes[3].amount && sponsor1.totalAmount <= sponsorTypes[4].amount - 1)
                                {
                                    sponsor1.sponsorType = 4;
                                }
                                else if (sponsor1.totalAmount >= sponsorTypes[2].amount && sponsor1.totalAmount <= sponsorTypes[3].amount - 1)
                                {
                                    sponsor1.sponsorType = 3;
                                }
                                else if (sponsor1.totalAmount >= sponsorTypes[1].amount && sponsor1.totalAmount <= sponsorTypes[2].amount - 1)
                                {
                                    sponsor1.sponsorType = 2;
                                }


                            }

                            else
                            {
                                sponsor1.active = true;
                                sponsor1.totalAmount = bill.AmountPaid;
                            }
                            context.SaveChanges();
                            return bill.paymentBillID;

                        }

                        else
                        {//user paymentRegistration
                            var registration = bill.payment.registrations.FirstOrDefault();
                            user saveUser = context.users.Where(u => u.userID == registration.userID).FirstOrDefault();
                            saveUser.registrationStatus = "Accepted";
                            context.SaveChanges();
                            return bill.paymentBillID;
                        };

                    }
                    else
                    {
                        return -1;
                    }
                }
            }

            catch (Exception ex)
            {
                Console.Write("paymentManger.StorePaymentBillInfo error " + ex);
                return -1;
            }

        }

        /*THIS METHOD extract values from string*/
        public PaymentXML parseXMLString(string xml)
        {
            PaymentXML xmlObj = new PaymentXML();


            xmlObj.firstName = getProperty(xml, "CLIENTFIRSTNAME");
            xmlObj.lastName = getProperty(xml, "CLIENTLASTNAME");
            xmlObj.email = getProperty(xml, "EMAIL");
            xmlObj.line1 = getProperty(xml, "ADDR1");
            xmlObj.line2 = getProperty(xml, "ADDR2");
            xmlObj.city = getProperty(xml, "CITY");
            xmlObj.zipCode = getProperty(xml, "ZIPCODE");
            xmlObj.phone = getProperty(xml, "TELEPHONE");
            xmlObj.quantity = getProperty(xml, "QUANTITY");
            xmlObj.IP = getProperty(xml, "IP");


            return xmlObj;
        }

        public xmlTransacctionID parseXMLTransacctionID(string xml)
        {
            xmlTransacctionID xmlObj = new xmlTransacctionID();

            xmlObj.error = getProperty(xml, "STATUSCODE");
            xmlObj.transactionID = getProperty(xml, "TRANSACTIONID");

            return xmlObj;
        }

        //This method parse the receipt information received from UPRMSecure
        public XMLReceiptInfo parseReceiptInfo(string xml)
        {
            XMLReceiptInfo xmlObj = new XMLReceiptInfo();


            xmlObj.transactionID = getProperty(xml, "TRANSACTIONID");
            xmlObj.merchantName = getProperty(xml, "MERCHANT_NAME");
            xmlObj.merchantURL = getProperty(xml, "MERCHANT_URL");
            xmlObj.firstName = getProperty(xml, "NAME");
            xmlObj.lastName = getProperty(xml, "LASTNAME");
            xmlObj.tandemID = getProperty(xml, "TANDEMID");
            xmlObj.batchId = getProperty(xml, "BATCHID");
            xmlObj.transactionType = getProperty(xml, "TRANSACTION_TYPE");
            xmlObj.email = getProperty(xml, "EMAIL");
            xmlObj.error = getProperty(xml, "ERROR");
            xmlObj.message = getProperty(xml, "MESSAGE");


            return xmlObj;
        }
        //Rejex code used to detect the tags on the request string 
        private string getProperty(string xml, string property)
        {
            //check
            Match m = Regex.Match(xml, @"<" + property + ">\\s*(.+?)\\s*</" + property + ">");
            if (m.Success)
            {
                return m.Groups[1].Value;
            }

            else
            {
                return null;
            }
        }

    }
    public class AmountSatusRegistration
    {
        public double amount { get; set; }
        public bool inTimeLateFee { get; set; }
        public bool inTime { get; set; }
    }
}