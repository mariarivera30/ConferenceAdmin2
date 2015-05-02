using Nancy;
using Nancy.Authentication.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using Nancy.ModelBinding;
using System.Web;

namespace NancyService.Modules
{
    public class PaymentModule : NancyModule
    {
        PaymentManager paymentManager = new PaymentManager();
        SponsorManager sponsorManager = new SponsorManager();
        public PaymentModule(ITokenizer tokenizer)
            : base("/payment")
        {
         
            
            Get["/billerror"] = parameters =>
            {
                return Response.AsRedirect("http://localhost:12036/#/PaymentError");
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
                        return "http://localhost:12036/#/PaymentError";
                    }
                  
                    else
                        return "http://localhost:12036/#/PaymentBill/" + paymentID ;
                    
                }
                else { return "http://localhost:12036/#/PaymentError"; }
           
            };
      //
            Put["/SponsorPayment"] = parameters =>
                {
                    var sponsor = this.Bind<NancyService.Modules.SponsorManager.SponsorQuery>();
                    var temp = this.Bind<PaymentXML>();
                    temp.quantity = (sponsor.newAmount * 100).ToString();
                    temp.IP =HttpContext.Current.Request.UserHostAddress;
                   
                    xmlTransacctionID action = paymentManager.MakeWebServiceCall(temp);
                    if (action.error == "000")
                        {
                           long paymentId= sponsorManager.getPaymentID(sponsor.sponsorID);
                           if (paymentId != 0) { //dont exist a sponsor 
                                paymentManager.createPaymentBill(paymentId,sponsor.newAmount,action.transactionID);
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
                            string errorLink = "http://localhost:12036/#/PaymentError";
                            return Response.AsJson(errorLink); 
                        }   
                 
                };

            Put["/UserPayment"] = parameters =>
            {
              var sponsor = this.Bind<NancyService.Modules.SponsorManager.SponsorQuery>();
                var temp = this.Bind<PaymentXML>();
                temp.quantity = (sponsor.newAmount * 100).ToString();
                temp.IP = HttpContext.Current.Request.UserHostAddress;

                xmlTransacctionID action = paymentManager.MakeWebServiceCall(temp);
                if (action.error == "000")
                {
                    //   long userId= sponsorManager.getUserID(sponsor.sponsorID);
                    //   if (userId != 0) { //dont exist a user 
                    paymentManager.createPaymentBill(sponsor.sponsorID,sponsor.newAmount, action.transactionID);
                    string secureLink = "https://secure2.uprm.edu/payment/index.php?id=" + action.transactionID;
                    return Response.AsJson(secureLink);
                    // }
                    //else
                    //{
                    //    return HttpStatusCode.Conflict;
                    //}
                }
                else
                {
                    string errorLink = "http://localhost:12036/#/PaymentError";
                    return Response.AsJson(errorLink);
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