using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyService.Modules
{
    public class ProfileInfoManager
    {
        public ProfileInfoManager()
        {

        }

        public UserInfo getProfileInfo(UserInfo user)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    UserInfo userInfo = context.users.Where(u => u.userID == user.userID).Select(u => new UserInfo
                    {
                        userID = u.userID,
                        userTypeID = u.userTypeID,
                        title = u.title,
                        firstName = u.firstName,
                        lastName = u.lastName,
                        affiliationName = u.affiliationName,
                        addressLine1 = u.address.line1,
                        addressLine2 = u.address.line2,
                        city = u.address.city,
                        state = u.address.state,
                        country = u.address.country,
                        zipcode = u.address.zipcode,
                        email = u.membership.email,
                        phone = u.phone,
                        userFax = u.userFax,
                        registrationStatus = u.registrationStatus,
                        acceptanceStatus = u.acceptanceStatus,
                        hasApplied = u.hasApplied,
                        key = context.companions.Where(c => c.userID == user.userID).FirstOrDefault().companionKey
                    }).FirstOrDefault();

                    RegisteredUser reg = context.registrations.Where(r => r.userID == user.userID).Select(r => new RegisteredUser
                    {
                        date1 = r.date1,
                        date2 = r.date2,
                        date3 = r.date3,
                        notes = r.note
                    }).FirstOrDefault();

                    if (reg != null)
                    {
                        userInfo.date1 = reg.date1;
                        userInfo.date2 = reg.date2;
                        userInfo.date3 = reg.date3;
                        userInfo.notes = reg.notes;
                    }

                    return userInfo;
                }
            }
            catch (Exception ex)
            {
                Console.Write("ProfileInfoManager.getProfileInfo error " + ex);
                return null;
            }
        }

        public bool updateProfileInfo(UserInfo user)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    user newUser = context.users.Where(u => u.userID == user.userID).FirstOrDefault();
                    newUser.userID = user.userID;
                    newUser.title = user.title;
                    newUser.firstName = user.firstName;
                    newUser.lastName = user.lastName;
                    newUser.affiliationName = user.affiliationName;
                    newUser.phone = user.phone;
                    newUser.userFax = user.userFax;

                    /*membership membership = context.memberships.Where(m => m.membershipID == newUser.membershipID).FirstOrDefault();
                    membership.email = user.email;*/

                    address address = context.addresses.Where(a => a.addressID == newUser.addressID).FirstOrDefault();
                    address.line1 = user.addressLine1;
                    address.line2 = user.addressLine2;
                    address.city = user.city;
                    address.state = user.state;
                    address.country = user.country;
                    address.zipcode = user.zipcode;
                    
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }


        public bool makePayment(UserInfo user)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    payment payment = new payment
                    {
                        paymentTypeID = 1,
                        deleted = false,
                        creationDate = DateTime.Now.Date
                    };

                    context.payments.Add(payment);
                    context.SaveChanges();

                    registration registration = new registration
                    {
                        userID = user.userID,
                        paymentID = payment.paymentID,
                        date1 = user.date1,
                        date2 = user.date2,
                        date3 = user.date3,
                        byAdmin = false,
                        deleted = false,
                        note = user.notes
                    };                    

                    user saveUser = context.users.Where(u => u.userID == user.userID).FirstOrDefault();
                    saveUser.registrationStatus = "Accepted";

                    paymentbill bill = new paymentbill
                    {
                        paymentID = payment.paymentID,
                        addressID = saveUser.addressID,
                        deleted = false,
                        AmountPaid = (double)saveUser.usertype.registrationCost,
                        // transaction fields
                        transactionid = "1",
                        methodOfPayment = "VISA",
                        //creditCardNumber = "123456789",
                       // cardExpirationDate = DateTime.Now.Date
                    };

                    context.registrations.Add(registration);
                    context.paymentbills.Add(bill);
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }



        public bool complementaryPayment(UserInfo user, string key)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    payment payment = new payment
                    {
                        paymentTypeID = 2,
                        deleted = false,
                        creationDate = DateTime.Now.Date
                    };

                    context.payments.Add(payment);
                    context.SaveChanges();

                    registration registration = new registration
                    {
                        userID = user.userID,
                        paymentID = payment.paymentID,
                        date1 = user.date1,
                        date2 = user.date2,
                        date3 = user.date3,
                        byAdmin = false,
                        deleted = false,
                        note = user.notes
                    };

                    user saveUser = context.users.Where(u => u.userID == user.userID).FirstOrDefault();
                    saveUser.registrationStatus = "Accepted";

                    complementarykey complementaryKey = context.complementarykeys.Where(k => k.key == key).FirstOrDefault();
                    complementaryKey.isUsed = true;

                    paymentcomplementary complementaryPay = new paymentcomplementary
                    {
                        paymentID = payment.paymentID,
                        deleted = false,
                        complementaryKeyID = complementaryKey.complementarykeyID
                    };

                    context.registrations.Add(registration);
                    context.paymentcomplementaries.Add(complementaryPay);
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public PaymentInfo userPayment(UserInfo user)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    PaymentInfo pay = new PaymentInfo();
                    payment payment = new payment();
                    var registrationExist = context.registrations.Where(x => x.userID == user.userID).FirstOrDefault();
                    if (registrationExist == null)
                    {
                        
                       payment.paymentTypeID = 1;
                       payment.deleted = false;
                       payment.creationDate = DateTime.Now.Date;
                      
                        context.payments.Add(payment);
                        context.SaveChanges();
                        //Check if exist a registration then take it and edit it 
                        registration registration = new registration
                        {
                            userID = user.userID,
                            paymentID = payment.paymentID,
                            date1 = user.date1,
                            date2 = user.date2,
                            date3 = user.date3,
                            byAdmin = false,
                            deleted = false,
                            note = user.notes,
                           
                        };
                        pay.paymentID = payment.paymentID;
                        context.registrations.Add(registration);
                        context.SaveChanges();

                    }

                    else
                    {
                        
                        registrationExist.date1 = user.date1;
                        registrationExist.date2 = user.date2;
                        registrationExist.date3 = user.date3;
                        registrationExist.byAdmin = false;
                        registrationExist.deleted = false;
                        registrationExist.note = user.notes;
                        context.SaveChanges();
                        pay.paymentID = registrationExist.paymentID;

                    }
                  
                    var userType =context.usertypes.Where(x => x.userTypeID == user.userTypeID).First();
                   
                    //add check if is lateregistration o regular registration*****************************************IMPORTANT
                    
                    pay.amount=(double)userType.registrationCost;

                    return pay;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public bool apply(UserInfo user)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    user newUser = context.users.Where(u => u.userID == user.userID).FirstOrDefault();
                    newUser.hasApplied = true;
                    if (newUser.userTypeID == 1)
                        context.minors.Where(m => m.userID == user.userID).FirstOrDefault().authorizationStatus = true;

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("ProfileAuthorizationManager.UserPayment error " + ex);
                return false;
            }
        }

        public bool checkComplementaryKey(string key)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    complementarykey complementaryKey = context.complementarykeys.Where(ck => ck.key == key && ck.isUsed == false && ck.deleted == false).FirstOrDefault();
                    
                    return complementaryKey != null;
                }
            }
            catch (Exception ex)
            {
                Console.Write("ProfileAuthorizationManager.checkComplementaryKey error " + ex);
                return false;
            }
        }

    }
}
public class PaymentInfo
{
    public long paymentID {get;set;}
    public double amount { get; set; }
    public string phone { get; set; }
    public bool isUser { get; set; }
}

public class UserInfo
{
    public long userID;
    public long userTypeID;
    public string title;
    public string firstName;
    public string lastName;
    public string affiliationName;
    public string addressLine1;
    public string addressLine2;
    public string city;
    public string state;
    public string country;
    public string zipcode;
    public string email;
    public string phone;
    public string userFax;
    public string registrationStatus;
    public string acceptanceStatus;
    public bool? hasApplied;
    public bool? date1;
    public bool? date2;
    public bool? date3;
    public string notes;
    public string key;
}