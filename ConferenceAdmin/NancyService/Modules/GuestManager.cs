using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyService.Modules
{
    public class GuestManager
    {

        //Get list of guests
        public List<GuestList> getListOfGuests()
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    List<GuestList> guestList = new List<GuestList>();
                    List<user> guests = context.users.Where(c => c.hasApplied == true && c.deleted != true).ToList();
                     long userID;
         String firstName;
         String lastName;
         String title;
         String affiliationName;
         String userTypeName;
         bool? authorizationStatus;
         bool isRegistered;
         String registrationStatus;
         String acceptanceStatus;
         String line1;
         String line2;
         String city;
         String state;
         String country;
         String zipcode;
         String email;
         String phoneNumber;
         String fax;
         bool? day1;
         bool? day2;
         bool? day3;
         String optionStatus;
         String companionFirstName;
         String companionLastName;
         long companionID;
                    /*var guests = context.users.Where(c => c.hasApplied == true && c.deleted != true).Select(i => new GuestList
                    {
                        userID = (int)i.userID,
                        firstName = i.firstName,
                        lastName = i.lastName,
                        title = i.title,
                        affiliationName = i.affiliationName,
                        userTypeName = i.usertype.userTypeName,
                        isRegistered = (i.registrationStatus == "Accepted" ? true : false),
                        registrationStatus = i.registrationStatus,
                        acceptanceStatus = i.acceptanceStatus,
                        optionStatus = "Accepted",
                        authorizationStatus = i.minors.FirstOrDefault() == null ? null : i.minors.FirstOrDefault().authorizationStatus,
                        line1 = i.address.line1,
                        line2 = i.address.line2,
                        city = i.address.city,
                        state = i.address.state,
                        country = i.address.country,
                        zipcode = i.address.zipcode,
                        email = i.membership.email,
                        phoneNumber = i.phone,
                        fax = i.userFax,
                        day1 = i.registrations.FirstOrDefault() == null ? null : i.registrations.FirstOrDefault().date1,
                        day2 = i.registrations.FirstOrDefault() == null ? null : i.registrations.FirstOrDefault().date2,
                        day3 = i.registrations.FirstOrDefault() == null ? null : i.registrations.FirstOrDefault().date3,
                        companionFirstName = i.companions.FirstOrDefault() == null ? null : i.companions.FirstOrDefault().user.firstName,
                        companionLastName = i.companions.FirstOrDefault() == null ? null : i.companions.FirstOrDefault().user.lastName,
                        companionID = i.companions.FirstOrDefault() == null ? -1 : (long)i.companions.FirstOrDefault().userID 
                    }).ToList();*/
                    foreach(var guest in guests){
                      userID = guest.userID;
          firstName = guest.firstName;
          lastName = guest.lastName;
          title = guest.title;
          affiliationName = guest.affiliationName;
          userTypeName = guest.usertype.userTypeName;
                        authorizationStatus = guest.minors.FirstOrDefault() == null ? false : guest.minors.FirstOrDefault().authorizationStatus;
          isRegistered = guest.registrationStatus == "Accepted" ? true : false;
          registrationStatus = guest.registrationStatus;
          acceptanceStatus = guest.acceptanceStatus;
          line1 = guest.address.line1;
          line2 = guest.address.line2;
          city = guest.address.city;
          state = guest.address.state;
          country = guest.address.country;
          zipcode = guest.address.zipcode;
          email = guest.membership.email;
          phoneNumber = guest.phone;
          fax = guest.userFax;
                        day1 = guest.registrations.FirstOrDefault() == null ? null : guest.registrations.FirstOrDefault().date1;
          day2 = guest.registrations.FirstOrDefault() == null ? null : guest.registrations.FirstOrDefault().date2;
          day3 = guest.registrations.FirstOrDefault() == null ? null : guest.registrations.FirstOrDefault().date3;
          optionStatus = "Accepted";
          companionFirstName = guest.companions.FirstOrDefault() == null ? null : guest.companions.FirstOrDefault().user.firstName;
          companionLastName = guest.companions.FirstOrDefault() == null ? null : guest.companions.FirstOrDefault().user.lastName;
          companionID = guest.companions.FirstOrDefault() == null ? -1 : (long)guest.companions.FirstOrDefault().userID;
          guestList.Add(new GuestList(userID, firstName, lastName, title, affiliationName, userTypeName,
authorizationStatus, isRegistered, registrationStatus, acceptanceStatus, line1, line2,
city, state, country, zipcode, email, phoneNumber, fax, day1,
day2, day3, optionStatus, companionFirstName, companionLastName, companionID));
                    }
                    return guestList;
                }
            }
            catch (Exception ex)
            {
                Console.Write("GuestManager.getListOfGuests error " + ex);
                return null;
            }
        }

        public bool updateAcceptanceStatus(int guestID, String acceptanceStatus)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var guest = context.users.Where(c => c.userID == guestID).FirstOrDefault();
                    guest.acceptanceStatus = acceptanceStatus;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("GuestManager.updateAcceptanceStatus error " + ex);
                return false;
            }
        }

        public List<MinorAuthorizations> getMinorAuthorizations(int id)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    List<MinorAuthorizations> authorizations = new List<MinorAuthorizations>();
                    authorizations = context.authorizationsubmitteds.Where(c => c.minor.userID == id && c.deleted == false).
                        Select(i => new MinorAuthorizations{
                            documentName = i.documentName,
                            documentFile = i.documentFile
                        }).ToList();                    
                   
                    return authorizations;
                }
            }
            catch (Exception ex)
            {
                Console.Write("GuestManager.getMinorAuthorizations error " + ex);
                return null;
            }
        }

        public bool rejectRegisteredGuest(int id)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var guest = context.users.Where(c => c.userID == id).FirstOrDefault();
                    guest.registrationStatus = "Rejected";
                    guest.acceptanceStatus = "Rejected";
                    guest.registrations.FirstOrDefault().deleted = true;
                    guest.registrations.FirstOrDefault().date1 = false;
                    guest.registrations.FirstOrDefault().date2 = false;
                    guest.registrations.FirstOrDefault().date3 = false;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("GuestManager.updateAcceptanceStatus error " + ex);
                return false;
            }
        }
    }

    public class MinorAuthorizations
    {
        public String documentName;
        public String documentFile;        

        public MinorAuthorizations()
        {
            
        }
    }

    public class GuestList
    {
        public long userID;
        public String firstName;
        public String lastName;
        public String title;
        public String affiliationName;
        public String userTypeName;
        public bool? authorizationStatus;
        public bool isRegistered;
        public String registrationStatus;
        public String acceptanceStatus;
        public String line1;
        public String line2;
        public String city;
        public String state;
        public String country;
        public String zipcode;
        public String email;
        public String phoneNumber;
        public String fax;
        public bool? day1;
        public bool? day2;
        public bool? day3;
        public String optionStatus;
        public String companionFirstName;
        public String companionLastName;
        public long companionID;

        public GuestList(long userID, String firstName, String lastName, String title, String affiliationName, String userTypeName,
            bool? authorizationStatus, bool isRegistered, String registrationStatus, String acceptanceStatus, String line1, String line2,
            String city, String state, String country, String zipcode, String email, String phoneNumber, String fax, bool? day1,
            bool? day2, bool? day3, String optionStatus, String companionFirstName, String companionLastName, long companionID)
        {
            this.userID = userID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.title = title;
            this.affiliationName = affiliationName;
            this.userTypeName = userTypeName;
            this.authorizationStatus = authorizationStatus;
            this.isRegistered = isRegistered;
            this.registrationStatus = registrationStatus;
            this.acceptanceStatus = acceptanceStatus;
            this.line1 = line1;
            this.line2 = line2;
            this.city = city;
            this.state = state;
            this.country = country;
            this.zipcode = zipcode;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.fax = fax;
            this.day1 = day1;
            this.day2 = day2;
            this.day3 = day3;
            this.optionStatus = optionStatus;
            this.companionFirstName = companionFirstName;
            this.companionLastName = companionLastName;
            this.companionID = companionID;
            
        }

        public GuestList()
        {
            // TODO: Complete member initialization
        }
    }
}