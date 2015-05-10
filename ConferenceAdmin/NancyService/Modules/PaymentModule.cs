using Nancy;
using Nancy.Authentication.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using Nancy.ModelBinding;
using System.Web;

/*Created by: Maria Rivera
 * This module contains the routes used to make a payment
 * 
 *  Get["/billerror"] = parameters => error page used by UPRMSecure to redirect the use in case of error
 *  Post["/reentry"]  Used by UPRMSecure to complete a payment base on the transaction ID
 *  Put["/SponsorPayment"]  Used for Initialize a sponsor Paymentprocess
 *  Put["/userPayment"]  Used for Initialize  a User Payment
 *  Get["/getUserPriceInDeadline/{id:int}"] Used for get the correct amount for a registration depending on type of user and registration deadlines
 *  Get["/getUserPayment/{id:long}"] 
 *  Get["/getsponsorpayments/{id:long}"] 
 *  Get["/getUserPayment/{id:long}"]---> General Receipt used by UPRMSecure to redirect the user
 * 
 */
namespace NancyService.Modules
{
    public class PaymentModule : NancyModule
    {
        PaymentManager paymentManager = new PaymentManager();
        SponsorManager sponsorManager = new SponsorManager();
        AdminManager adminManager = new AdminManager();
        ProfileInfoManager profileInfoManager = new ProfileInfoManager();
        public PaymentModule(ITokenizer tokenizer)
            : base("/payment")
        {
         
            
            Get["/billerror"] = parameters =>
            {
                return Response.AsRedirect("http://136.145.116.238/#/PaymentError");
            };


           Post["/reentry"] = parameters =>
            {
                var xml = this.Request.Form["xml"];
                
                /*store on db the payment information*/
                XMLReceiptInfo receipt = paymentManager.parseReceiptInfo(xml);

                if (receipt.error == "0")
                {
                    long paymentID = paymentManager.storePaymentBill(receipt);
                    if (paymentID == 0 || paymentID ==-1)
                    {
                        //error storing Payment
                        string link = "http://136.145.116.238/#/PaymentError";
                        return "<URL>"+link+"</URL>";
                    }
                  
                    else{
                        string link = "http://136.145.116.238/#/PaymentBill/" + paymentID;
                         return "<URL> " + link + "</URL>";
                 

                    }
                       
                    
                }
                else { 
                    string link = "http://136.145.116.238/#/PaymentError";
                    return "<URL>"+link+"</URL>";
                }
           
            };
      //
            Put["/SponsorPayment"] = parameters =>
                {   string sponsorProductID = "RECA0185";//sponsor
       
                    var sponsor = this.Bind<NancyService.Modules.SponsorManager.SponsorQuery>();
                    var temp = this.Bind<PaymentXML>();
                    temp.productID = sponsorProductID;
                    temp.IP =this.Request.UserHostAddress;
                  
                    PaymentInfo payInfo = new PaymentInfo();
                    payInfo.phone=sponsor.phone;
                    payInfo.amount = sponsor.newAmount;
                    payInfo.paymentID =sponsorManager.getPaymentID(sponsor.sponsorID);
                    payInfo.isUser = false;
                    xmlTransacctionID action = paymentManager.MakeWebServiceCall(temp);
                    if (action.error == "000")
                        {
                          
                           if (payInfo.paymentID != 0) { //dont exist a sponsor 
                                paymentManager.createPaymentBill(payInfo,action.transactionID);
                                string secureLink = "https://secure2.uprm.edu/payment/index.php?id=" + action.transactionID;
                                return Response.AsJson(secureLink);
                           }
                            else
                           {
                                return HttpStatusCode.Conflict;
                            }
                        }
                     else
                        {
                            string errorLink = null;
                            return Response.AsJson(errorLink); 
                        }   
                 
                };


            Put["/userPayment"] = parameters =>
            {
                string userProductID = "RECA0186";//registro
                var user = this.Bind<UserInfo>();
                var temp = this.Bind<PaymentXML>();
                temp.line1 = user.addressLine1;
                temp.line2 = user.addressLine2;
                temp.phone=user.phone;
                temp.productID = userProductID;
                temp.IP = this.Request.UserHostAddress;
                if (user.amount == 0)
                {
                    if( profileInfoManager.makePaymentFree(user))
                        return Response.AsJson("billCreated");
                    else
                    {
                        return HttpStatusCode.Conflict;
                    }
              

                }
                PaymentInfo payInfo = profileInfoManager.userPayment(user);
                payInfo.phone = user.phone;
                payInfo.isUser = true;
                if (payInfo != null)
                {
                    temp.quantity = (payInfo.amount * 100).ToString();
                    xmlTransacctionID action = paymentManager.MakeWebServiceCall(temp);

                    if (action.error == "000")
                    {
                        paymentManager.createPaymentBill(payInfo, action.transactionID);
                        string secureLink = "https://secure2.uprm.edu/payment/index.php?id=" + action.transactionID;
                        return Response.AsJson(secureLink);
                    }
                    else
                    {
                        string errorLink = null;
                        return Response.AsJson(errorLink);
                    }
                }
                else
                {
                    return HttpStatusCode.Conflict;
                }

            };
            
                Get["/getUserPriceInDeadline/{id:int}"] = parameters =>
            {
                int id = parameters.id;

                AmountSatusRegistration result = paymentManager.getUserPriceInDeadline(id);

                if(result!=null){
                        return Response.AsJson(result);
                }
                
                else
                {
                    return HttpStatusCode.Conflict;
                }


            };
            Get["/getUserPayment/{id:long}"] = parameters =>
            {
                long id = parameters.id;

                PaymentQuery result = paymentManager.getUserPayment(id);

                if (result != null)
                {
                    if(result.paymentBillID!=-1){
                        return Response.AsJson(result);
                    }
                    else
                    {
                        return Response.AsJson("");
                    }
                   
                }

                else
                {
                    return HttpStatusCode.Conflict;
                }


            };
            
            Get["/GetPayment/{id:long}"] = parameters =>
            {
                long id = parameters.id;

                PaymentQuery result = paymentManager.getPayment(id);

                if (result != null)
                {
                    return Response.AsJson(result);
                }

                else
                {
                    return HttpStatusCode.Conflict;
                }


            };

            Get["/getsponsorpayments/{id:long}"] = parameters =>
            {
                long id = parameters.id;

                List<PaymentQuery> result = paymentManager.getSponsorPayments(id);

                if (result != null)
                {
                    return Response.AsJson(result);
                }

                else
                {
                    return HttpStatusCode.Conflict;
                }


            };
        

        }
    }
}