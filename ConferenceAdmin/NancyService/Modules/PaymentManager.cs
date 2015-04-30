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
    }
    public class XMLReceiptInfo
    {
       
        public string transactionID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string merchantName { get; set; }
        public string merchantURL{ get; set; }
        public string tandemID{ get; set; }
        public string batchId { get; set; }
        public string transactionType { get; set; }
        public string error { get; set; }
        public string message { get; set; }
        public string methodOfPayment { get; set; }
    }

   public class xmlTransacctionID{
        public string error { get; set; }
        public string transactionID { get; set; }
   }

   public class PaymentXML
    {
        
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string line1 { get; set; }
        public string line2{ get; set; }
        public string city { get; set; }
        public string zipCode { get; set; }
        public string phone { get; set; }
        public string quantity { get; set; }
        public string IP { get; set; }
  
       

    }
    public class PaymentManager
    {   //RECA0185 Sponsor
        private string productID = "RECA0186";//registro
        private string athorizationID = "DMKRZ72";
        private string version = "1.1.0";
        public  PaymentManager()
        {}


        public List<PaymentQuery> getSponsorPayments(long id)
        {

            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var paymentInfo = (from s in context.paymentbills
                                       from sp in context.sponsor2
                                       where sp.userID == id && s.deleted == false && sp.paymentID == s.paymentID && s.completed==true
                                       select new PaymentQuery
                                       {
                                           paymentBillID = s.paymentBillID,
                                           date = (DateTime)s.payment.creationDate,
                                           transactionid = s.transactionid,
                                           AmountPaid = s.AmountPaid,
                                           authorizationID = s.authorizationID,
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


                    //if (paymentInfo == null)
                    //{
                    //    paymentInfo = (from s in context.paymentbills
                    //                   from r in context.registrations
                    //                   where s.paymentBillID == id && s.deleted == false && r.paymentID == s.paymentID
                    //                   select new PaymentQuery
                    //                   {
                    //                       paymentBillID = s.paymentBillID,
                    //                       date = (DateTime)s.payment.creationDate,
                    //                       transactionid = s.transactionid,
                    //                       AmountPaid = s.AmountPaid,
                    //                       authorizationID = s.authorizationID,
                    //                       methodOfPayment = s.methodOfPayment,
                    //                       firstName = s.firstName,
                    //                       lastName = s.lastName,
                    //                       email = s.email,
                    //                       tandemID = s.tandemID,
                    //                       batchID = s.batchID,
                    //                       userFirstName = r.user.firstName,
                    //                       userLastName = r.user.lastName,
                    //                       affiliationName = r.user.affiliationName,
                    //                       type = r.user.usertype.userTypeName,
                    //                       description = "User Registration."
                    //                   }).FirstOrDefault();
                    //}

                    return paymentInfo;
                }

            }
            catch (Exception ex)
            {
                Console.Write("PaymetnManager.getPaymentSponsorsReceiptInfo error " + ex);
                return null;
            }


        }
       //public PaymentQuery getPayment(long id)
       // {
          
       //         try
       //     {
       //         using (conferenceadminContext context = new conferenceadminContext())
       //         {
       //             var paymentInfo = (from s in context.paymentbills
       //                                from sp in context.sponsor2
       //                                where s.paymentBillID ==id && s.deleted==false && sp.paymentID ==s.paymentID 
       //                          select new PaymentQuery
       //                          {
       //                               paymentBillID = s.paymentBillID,
       //                               date =(DateTime) s.payment.creationDate,
       //                               transactionid=s.transactionid,
       //                               AmountPaid =s.AmountPaid,
       //                               authorizationID =s.authorizationID,
       //                               methodOfPayment = s.methodOfPayment,
       //                               firstName = s.firstName,
       //                               lastName =s.lastName,
       //                               email = s.email,
       //                               tandemID =s.tandemID,
       //                               batchID =s.batchID,
       //                               userFirstName= sp.firstName,
       //                               userLastName= sp.lastName,
       //                               affiliationName = sp.company,
       //                               type = sp.sponsortype1.name,
       //                               description = "Sponsor Donation"

       //                          }).FirstOrDefault();


       //             if (paymentInfo == null)
       //             {
       //                 paymentInfo = (from s in context.paymentbills
       //                                    from r in context.registrations
       //                                    where s.paymentBillID == id && s.deleted == false && r.paymentID == s.paymentID
       //                                    select new PaymentQuery
       //                                    {
       //                                        paymentBillID = s.paymentBillID,
       //                                        date = (DateTime)s.payment.creationDate,
       //                                        transactionid = s.transactionid,
       //                                        AmountPaid = s.AmountPaid,
       //                                        authorizationID = s.authorizationID,
       //                                        methodOfPayment = s.methodOfPayment,
       //                                        firstName = s.firstName,
       //                                        lastName = s.lastName,
       //                                        email = s.email,
       //                                        tandemID = s.tandemID,
       //                                        batchID = s.batchID,
       //                                        userFirstName = r.user.firstName,
       //                                        userLastName = r.user.lastName,                                               
       //                                        affiliationName = r.user.affiliationName,
       //                                        type = r.user.usertype.userTypeName,
       //                                        description = "User Registration."
       //                                    }).FirstOrDefault();
       //             }

       //             return paymentInfo;
       //         }

       //     }
       //     catch (Exception ex)
       //     {
       //         Console.Write("PaymetnManager.getPaymentReceiptInfo error " + ex);
       //         return null;
       //     }

      
       // }
        public string creatXML(PaymentXML payment)
        {   
          string xml  = "<VERSION>" + version +"</VERSION>\n" +
           "<AUTHORIZATIONID>" + athorizationID + "</AUTHORIZATIONID>\n" +
		   "<CLIENTFIRSTNAME>"+payment.firstName+"</CLIENTFIRSTNAME>\n"+
		   "<CLIENTLASTNAME>"+payment.lastName+"</CLIENTLASTNAME>\n"+
		   "<EMAIL>"+payment.email+"</EMAIL>\n"+
		   "<ADDR1>"+payment.line1+"</ADDR1>\n"+
		   "<ADDR2>"+payment.line2+"</ADDR2>\n"+
		   "<CITY>"+payment.city+"</CITY>\n"+
		   "<ZIPCODE>"+payment.zipCode+"</ZIPCODE>\n"+
		   "<TELEPHONE>"+payment.phone+"</TELEPHONE>\n"+
		   "<QUANTITY>"+payment.quantity+"</QUANTITY>\n"+
           "<IP>"+payment.IP+"</IP>\n" +
           "<PRODUCTID>" + productID + "</PRODUCTID>\n";
            
            return xml;
        }
        public long storePaymentBill(XMLReceiptInfo receipt)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var bill = (from s in context.paymentbills
                                  where s.transactionid == receipt.transactionID && s.deleted == false
                                  select s).FirstOrDefault();
                    if (bill != null)
                    {
                        bill.tandemID = receipt.tandemID;
                        bill.methodOfPayment = receipt.methodOfPayment;
                        bill.batchID = receipt.batchId;
                        bill.email = receipt.email;
                        bill.firstName = receipt.firstName;
                        bill.lastName = receipt.lastName;
                        bill.deleted = false;
                        context.SaveChanges();
                        return bill.paymentBillID;

                    }
                    else return 0;
                  
                        
                }


            }
            catch (Exception ex)
            {
                Console.Write("paymentManger.StorePaymentBillInfo error " + ex);
                return -1;
            }

        }

        public xmlTransacctionID MakeWebServiceCall(PaymentXML payment)
        {
                // this is what we are sending
                //string post_data = "foo=bar&baz=oof";
                 string post_data = "xml=" + creatXML(payment) ;
 
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
              
                xmlTransacctionID xmlTransaction =parseXMLTransacctionID(responseStr);
                //Save transactionID on paymentBill
                //catch error 
                reader.Close();
                response.Close();
                return xmlTransaction; 
        
        }
        public void createPaymentBill(long paymentID, double amount, string transactionID)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    double quantity = amount * 100;
                    var sponsor = (from p in context.sponsor2
                                    where p.paymentID == paymentID
                                    select p).FirstOrDefault();
                    if (sponsor != null) { 
                    paymentbill bill = new paymentbill();
                    bill.AmountPaid = amount;
                    bill.paymentID = (long)sponsor.paymentID;
                    bill.completed = false;
                    bill.transactionid = transactionID;
                    bill.quantity = (int)quantity;
                    bill.deleted = false;
                    bill.date = DateTime.Now;
                    context.paymentbills.Add(bill);
                    context.SaveChanges();
                    }
                 
                }
            }
            catch (Exception ex)
            {
                Console.Write("PaymentManager.makePayment error " + ex);
               
            }
         
        }

        /*THIS METHOD extract values from string*/
        public PaymentXML parseXMLString(string xml)
        {
            PaymentXML xmlObj =new PaymentXML();

    
            xmlObj.firstName= getProperty(xml, "CLIENTFIRSTNAME");
            xmlObj.lastName= getProperty(xml, "CLIENTLASTNAME");
            xmlObj.email= getProperty(xml, "EMAIL");
            xmlObj.line1= getProperty(xml, "ADDR1");
            xmlObj.line2= getProperty(xml, "ADDR2");
            xmlObj.city= getProperty(xml, "CITY");
            xmlObj.zipCode= getProperty(xml, "ZIPCODE");
            xmlObj.phone= getProperty(xml, "TELEPHONE");
            xmlObj.quantity= getProperty(xml, "QUANTITY");
            xmlObj.IP= getProperty(xml, "IP");
         

           return xmlObj;
        }

        public xmlTransacctionID parseXMLTransacctionID(string xml)
        {
            xmlTransacctionID xmlObj =new xmlTransacctionID();
                       
            xmlObj.error= getProperty(xml, "STATUSCODE");
            xmlObj.transactionID= getProperty(xml, "TRANSACTIONID");
            
           return xmlObj;
        }
          

          public XMLReceiptInfo parseReceiptInfo(string xml)
        {
            XMLReceiptInfo   xmlObj =new XMLReceiptInfo();
      
          
            xmlObj.transactionID= getProperty(xml, "TRANSACTIONID");
            xmlObj.merchantName= getProperty(xml, "MERCHANT_NAME");
            xmlObj.merchantURL= getProperty(xml, "MERCHANT_URL");
            xmlObj.firstName= getProperty(xml, "NAME");
            xmlObj.lastName= getProperty(xml, "LASTNAME");
            xmlObj.tandemID= getProperty(xml, "TANDEMID");
            xmlObj.batchId= getProperty(xml, "BATCHID");
            xmlObj.transactionType= getProperty(xml, "TRANSACTION_TYPE");
            xmlObj.email= getProperty(xml, "EMAIL");
            xmlObj.error= getProperty(xml, "ERROR");
            xmlObj.message= getProperty(xml, "MESSAGE");
            

           return xmlObj;
        }

        private string getProperty(string xml, string property){
            //check
            Match m = Regex.Match(xml, @"<"+property+">\\s*(.+?)\\s*</"+property+">");
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



}