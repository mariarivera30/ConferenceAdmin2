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
         
            
            Get["/BillError"] = parameters =>
            {
                return Response.AsRedirect("http://localhost:12036/#/PaymentError");
            };


           Put["/Reentry"] = parameters =>
            {
              
                var xml = this.Bind<String>();
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
      
            Put["/SponsorPayment"] = parameters =>
                {
                    var sponsor = this.Bind<NancyService.Modules.SponsorManager.SponsorQuery>();
                    var temp = this.Bind<PaymentXML>();
                    temp.quantity = (sponsor.amount * 100).ToString();
                    temp.IP =HttpContext.Current.Request.UserHostAddress;
                    SponsorManager.SponsorQuery added = sponsorManager.addSponsorPaid(sponsor,temp);
                    if (added != null)
                    {
                        xmlTransacctionID action = paymentManager.MakeWebServiceCall(temp);
                        if (action.error == "000")
                        {
                            string secureLink = "https://secure2.uprm.edu/payment/index.php?id=" + action.transactionID;
                            return Response.AsJson(secureLink);
                        }
                        else
                        {
                            string errorLink = "http://localhost:12036/#/PaymentError";
                            return Response.AsJson(errorLink); 
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