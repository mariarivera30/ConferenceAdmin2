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
                    var guests = context.users.Where(c => c.hasApplied == true && c.deleted != true).Select(i => new GuestList
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
                        authorizationStatus = i.minors.FirstOrDefault().authorizationStatus,
                        line1 = i.address.line1,
                        line2 = i.address.line2,
                        city = i.address.city,
                        state = i.address.state,
                        country = i.address.country,
                        zipcode = i.address.zipcode,
                        email = i.membership.email,
                        phoneNumber = i.phone,
                        fax = i.userFax,
                        day1 = i.registrations.FirstOrDefault().date1,
                        day2 = i.registrations.FirstOrDefault().date2,
                        day3 = i.registrations.FirstOrDefault().date3,
                        companionFirstName = i.companions.FirstOrDefault().user.firstName,
                        companionLastName = i.companions.FirstOrDefault().user.lastName,
                        companionID = ((int)i.companions.FirstOrDefault().userID != null ? (int)i.companions.FirstOrDefault().userID : -1)
                    }).ToList();

                    //var t = context.users.FirstOrDefault();

                    return guests;
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
        public int userID;
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
        public int companionID;

        public GuestList()
        {
            // TODO: Complete member initialization
        }
    }
}