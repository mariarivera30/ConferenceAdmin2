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
        AdminManager adminManager = new AdminManager();
        ProfileInfoManager profileInfoManager = new ProfileInfoManager();
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
                {   string sponsorProductID = "RECA0185";//registro
       
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