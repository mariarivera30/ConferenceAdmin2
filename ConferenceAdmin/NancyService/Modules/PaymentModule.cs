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
                //string xml ="xml=%3CVERSION%3E1.1.0%3C/VERSION%3E%0A%3CTRANSACTIONID%3E9999999999999999999%3C/TRANSACTIONID%3E%0A%3CMERCHANT_NAME%3EUPRM%3C/MERCHANT_NAME%3E%0A%3CMERCHANT_URL%3Ehttp%3A//secure.uprm.edu%3C/MERCHANT_URL%3E%0A%3CNAME%3ENombre%3C/NAME%3E%0A%3CLASTNAME%3EApellido%3C/LASTNAME%3E%0A%3CTANDEMID%3E000000%3C/TANDEMID%3E%0A%3CBATCHID%3E000000%3C/BATCHID%3E%0A%3CTRANSACTION_TYPE%3EATH%20-%20Purchase%3C/TRANSACTION_TYPE%3E%0A%3CEMAIL%3Eemail%40upr.edu%3C/EMAIL%3E%0A%3CERROR%3E0%3C/ERROR%3E%0A%3CMESSAGE%3C/MESSAGE%3E;
                var xml = this.Request.Form["xml"];
                
                /*store on db the payment information*/
                XMLReceiptInfo receipt = paymentManager.parseReceiptInfo(xml);

                if (receipt.error == "000")
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
                           if (paymentId != 0) { //dont exist a user 
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

          /*  Get["/GetPayment/{id:long}"] = parameters =>
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


            };*/

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