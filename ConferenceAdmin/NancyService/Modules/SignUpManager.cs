
﻿using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;


namespace NancyService.Modules
{
     public static class Security
   {
       /// <summary>
       /// take any string and encrypt it using SHA1 then
       /// return the encrypted data
       /// </summary>
       /// <param name="data">input text you will enterd to encrypt it</param>
       /// <returns>return the encrypted text as hexadecimal string</returns>
       public static string GetSHA1HashData(string data)
       {
           //create new instance of md5
           SHA1 sha1 = SHA1.Create();

           //convert the input text to array of bytes
           byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));

           //create new instance of StringBuilder to save hashed data
           StringBuilder returnValue = new StringBuilder();

           //loop for each byte and add it to StringBuilder
           for (int i = 0; i < hashData.Length; i++)
           {
               returnValue.Append(hashData[i].ToString());
           }

           // return hexadecimal string
           return returnValue.ToString();
       }

       /// <summary>
       /// encrypt input text using SHA1 and compare it with
       /// the stored encrypted text
       /// </summary>
       /// <param name="inputData">input text you will enterd to encrypt it</param>
       /// <param name="storedHashData">the encrypted
       ///           text stored on file or database ... etc</param>
       /// <returns>true or false depending on input validation</returns>

       public static bool ValidateSHA1HashData(string inputData, string storedHashData)
       {
           //hash input text and save it string variable
           string getHashInputData = GetSHA1HashData(inputData);

           if (string.Compare(getHashInputData, storedHashData) == 0)
           {
               return true;
           }
           else
           {
               return false;
           }
       }
   }
    public class SignUpManager
    {
        public class UserCreation
        {
            public string email { get; set; }
            public string password { get; set; }
            public int userTypeID { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string title { get; set; }
            public string affiliationName { get; set; }
            public string phone { get; set; }
            public long addressID { get; set; }
            public string userFax { get; set; }
            public string line1 { get; set; }
            public string line2 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public string zipcode { get; set; }
            public long membershipID { get; set; }
            public string newPass { get; set; }
            public bool emailConfirmation { get; set; }


        }
        //ccwicEmail
        string ccwicEmail = "ccwictest@gmail.com";
        string ccwicEmailPass = "ccwic123456789";
       
        public bool createUser(user user, membership member, address address)
        {
            try
            {
                string key = generateEmailConfirmationKey();
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //code for password encryption
                  
                    var encrpPass = Security.GetSHA1HashData(member.password);
                    member.password = encrpPass;
                   
                    //end password encryption
                    member.deleted = false;
                    member.emailConfirmation = false;
                    member.deleted = false;
                    member.confirmationKey = key;
                    context.memberships.Add(member);
                    context.SaveChanges();
                    context.addresses.Add(address);
                    context.SaveChanges();

                    user.addressID = address.addressID;
                    user.membershipID = member.membershipID;
                    user.acceptanceStatus = "Pending";
                    user.deleted = false;
                    user.hasApplied = false;
                    user.registrationStatus = "Pending";
                    user.evaluatorStatus = user.evaluatorStatus;
                    
                    context.users.Add(user);
                    context.SaveChanges();

                    if (user.userTypeID == 1)
                    {
                        minor minor = new minor();
                        minor.authorizationStatus = false;
                        minor.deleted = false;
                        minor.userID = user.userID;
                        context.minors.Add(minor);
                        context.SaveChanges();

                    }
                    else if (user.userTypeID == 7)
                    {
                        sponsor2 sponsor = new sponsor2();
                       
                        sponsor.deleted = false;
                        sponsor.userID = user.userID;
                        sponsor.active = false;
                        sponsor.sponsorType = 5;
                        sponsor.byAdmin = false;
                        sponsor.active = false;
                        sponsor.totalAmount = 0;
                        
                        payment payment2 = new payment();
                        payment2.paymentTypeID = 1;
                        payment2.deleted = false;
                        payment2.creationDate = DateTime.Now;
                        context.payments.Add(payment2);
                      
                        context.SaveChanges();
                        sponsor.paymentID = payment2.paymentID;
                        context.sponsor2.Add(sponsor);
                        context.SaveChanges();

                    }
                    else if (user.userTypeID == 6)
                    {
                        companion companion = new companion();
                        companion.deleted = false;
                        companion.userID = user.userID;
                        companion.companionKey = "Companion "+ user.userID + generateEmailConfirmationKey().Substring(0, 9);
                        context.companions.Add(companion);
                        context.SaveChanges();

                    }

                    try { sendEmailConfirmation(member.email, member.confirmationKey); }

                    catch (Exception ex)
                    {
                        Console.Write("SignUpManager.NewConfirmationEmail error " + ex);
                        return false;
                    }

                    return true;


                }

            }
            catch (Exception ex)
            {
                Console.Write("SignUpManager.creatingUser error " + ex);
                return false;
            }

        }

        private string generateEmailConfirmationKey()
        {
            return "CCWIC" + Guid.NewGuid().ToString();
        }

        private void sendEmailConfirmation(string email, string key)
        {
            MailAddress ccwic = new MailAddress(ccwicEmail);
            MailAddress user = new MailAddress(email);
            MailMessage mail = new System.Net.Mail.MailMessage(ccwic, user);


            mail.Subject = "Caribbean Celebration of Women in Computing Account Confirmation!";
            mail.Body = "Please click the link to confirm your account. \n\n " + "http://136.145.116.238/#/validate/"+ key;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            smtp.Credentials = new NetworkCredential(
                ccwicEmail,ccwicEmailPass);
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }

        private void sendTemporaryPassword(string email, string pass)
        {
            MailAddress ccwic = new MailAddress(ccwicEmail);
            MailAddress user = new MailAddress(email);
            MailMessage mail = new System.Net.Mail.MailMessage(ccwic, user);


            mail.Subject = "Caribbean Celebration of Women Temporary Password!";
            mail.Body = "Login using this password " + pass + ".\n Change your password as soon as possible.\n Visit us: http://136.145.116.238/#/ChangePassword";

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            smtp.Credentials = new NetworkCredential(
                ccwicEmail, ccwicEmailPass);
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }
        public String confirmAccount(string key)
        {


            using (conferenceadminContext context = new conferenceadminContext())
            {

                try
                {
                    UserCreation user = (from m in context.memberships
                                         from u in context.users
                                         where (m.confirmationKey.Equals(key) && m.membershipID == u.membershipID && m.deleted == false)
                                         select new UserCreation
                                         {
                                             firstName = u.firstName,
                                             lastName = u.lastName,
                                             email = m.email,
                                             membershipID = m.membershipID,
                                             emailConfirmation = (bool) m.emailConfirmation,
                                         }).FirstOrDefault();


                    if (user != null)
                    {
                        if (user.emailConfirmation)
                        {
                            return "wasValidated";
                        }
                        else
                        {
                            context.memberships
                                .Where(s => s.membershipID == user.membershipID && s.deleted == false)
                                .ToList().ForEach(s => { s.emailConfirmation = true; });
                            context.SaveChanges();
                            return "validated";
                        }
                       
                    }
                    else{
                         return "";
                    }
                   
                }
                catch (Exception ex)
                {
                    Console.Write("SponsorManager.getSponsor error " + ex);
                    return null;
                }
            }

        }


        public String requestPass(string email)
        {
            using (conferenceadminContext context = new conferenceadminContext())
            {

                try
                {
                    string tempPass = generateEmailConfirmationKey().Substring(0, 9);
                    tempPass = tempPass.Replace("-", "");
                    //encryption code
                    
                    var encrpTempPass = Security.GetSHA1HashData(tempPass);
                  
                    //end encryption code
                    var member = (from m in context.memberships
                                  where (m.email.Equals(email) && m.deleted == false)
                                  select m).FirstOrDefault();
                    if (member != null)
                    {
                        //member.password = tempPass;  //before encryption, maria code
                        member.password = encrpTempPass;//encrypting
                       
                        UserCreation u = new UserCreation();
                        u.email = member.email;
                        u.membershipID = member.membershipID;
                        context.SaveChanges();
                        try { sendTemporaryPassword(u.email, tempPass); }
                        catch (Exception ex){
                            Console.Write("SignUP.requestPass Send Email error " + ex);
                            return null;
                        }
                        
                        return "changed";
                    }

                    else
                    {
                        return "";
                    }


                }
                catch (Exception ex)
                {
                    Console.Write("checkEmail error " + ex);
                    return null;
                }
            }

        }

        public string checkEmail(string email)
        {
            using (conferenceadminContext context = new conferenceadminContext())
            {
                try
                {
                    var user = (from m in context.memberships
                                where (m.email.Equals(email)  && m.deleted == false)
                                select m).FirstOrDefault();
                    if (user != null)
                    {
                        if (user.emailConfirmation == true)
                        {
                            return "confirmed";
                        }
                        else 
                        {
                            try
                            {
                                sendEmailConfirmation(user.email, user.confirmationKey);
                                return "notconfirmed";
                            }
                            catch (Exception ex)
                            {
                                Console.Write("SignUP.Resend Email Confirmation error " + ex);
                                return null;
                            }
                        
                            
                        }

                    }

                    else { return ""; }

                }
                catch (Exception ex)
                {
                    Console.Write("SignUpRequestPassword error " + ex);
                    return null;
                }
            }

        }

        public UserCreation changePassword(UserCreation u)
        {
            using (conferenceadminContext context = new conferenceadminContext())
            {
                try
                {
                    /*var member = (from m in context.memberships
                                  where (m.email.Equals(u.email) && m.password== u.password && m.deleted == false)
                                  select m).FirstOrDefault(); */
                    //same as in login, had to remove password check from query
                    var member = (from m in context.memberships
                                  where (m.email.Equals(u.email) && m.deleted == false)
                                  select m).FirstOrDefault();
                    
                    if (Security.ValidateSHA1HashData(u.password, member.password)) { }//if password is the same member stays the same
                    else member = null;

                    if (member != null)
                    {
                        //encryption
                        var newEncrpPass = Security.GetSHA1HashData(u.newPass);
                      
                        //end encryption
                        //member.password = u.newPass;//without encryption, old
                        member.password = newEncrpPass;
                       
                        u.membershipID = member.membershipID;
                        context.SaveChanges();
                        return u;
                    }

                    else
                    {
                        return null;
                    }

                }
                catch (Exception ex)
                {
                    Console.Write("checkEmail error " + ex);
                    return null;
                }
            }

        }


    }

}