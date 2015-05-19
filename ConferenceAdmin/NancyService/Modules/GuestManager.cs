using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace NancyService.Modules
{
	public class GuestManager
	{
		//ccwicEmail
		string ccwicEmail = "ccwictest@gmail.com";
		string ccwicEmailPass = "ccwic123456789";

		//Get list of guests
		public GuestsPagingQuery getListOfGuests(int index)
		{
			GuestsPagingQuery page = new GuestsPagingQuery();
			try
			{
				using (conferenceadminContext context = new conferenceadminContext())
				{
					int pageSize = 10;
					var guestsThatApplied = context.users.Where(c => c.hasApplied == true && c.deleted != true).ToList();
					List<GuestList> guests = new List<GuestList>();
					foreach (var i in guestsThatApplied)
					{
						long userID = (int)i.userID;
						String firstName = i.firstName;
						String lastName = i.lastName;
						String title = i.title;
						String affiliationName = i.affiliationName;
						String userTypeName = i.usertype.userTypeName;
						bool isRegistered = (i.registrationStatus == "Accepted" ? true : false);
						String registrationStatus = i.registrationStatus;
						String acceptanceStatus = i.acceptanceStatus;
						String optionStatus = "Accepted";
						bool? authorizationStatus = i.minors.FirstOrDefault() == null ? null : i.minors.FirstOrDefault().authorizationStatus;
						String line1 = i.address.line1;
						String line2 = i.address.line2;
						String city = i.address.city;
						String state = i.address.state;
						String country = i.address.country;
						String zipcode = i.address.zipcode;
						String email = i.membership.email;
						String phoneNumber = i.phone;
						String fax = i.userFax;
						bool? day1 = i.registrations.FirstOrDefault() == null ? null : i.registrations.FirstOrDefault().date1;
						bool? day2 = i.registrations.FirstOrDefault() == null ? null : i.registrations.FirstOrDefault().date2;
						bool? day3 = i.registrations.FirstOrDefault() == null ? null : i.registrations.FirstOrDefault().date3;
						String companionFirstName = i.companions.FirstOrDefault() == null ? null : i.companions.FirstOrDefault().user.firstName;
						String companionLastName = i.companions.FirstOrDefault() == null ? null : i.companions.FirstOrDefault().user.lastName;
						long companionID = i.companions.FirstOrDefault() == null ? -1 : (long)i.companions.FirstOrDefault().userID;
						//check if user has accepted subs
						bool hasAcceptedSub = false;
						List<usersubmission> allSubs = i.usersubmissions.ToList();
						foreach (usersubmission sub in allSubs)
						{
							//check if initial subs accepted
							if (sub.submission1 != null)
							{
								if (sub.submission1.status == "Accepted")
								{
									hasAcceptedSub = true;
								}
							}
							//check if final subs accepted
							if (sub.submission != null)
							{
								if (sub.submission.status == "Accepted")
								{
									hasAcceptedSub = true;
								}
							}
						}
						var guest = new GuestList(userID, firstName, lastName, title, affiliationName, userTypeName,
							authorizationStatus, isRegistered, registrationStatus, acceptanceStatus, line1, line2,
							city, state, country, zipcode, email, phoneNumber, fax, day1,
							day2, day3, optionStatus, companionFirstName, companionLastName, companionID, hasAcceptedSub);
						guests.Add(guest);
					}
					guests = guests.OrderBy(f => f.firstName).ToList();

					page.rowCount = guests.Count();
					if (page.rowCount > 0)
					{
						page.maxIndex = (int)Math.Ceiling(page.rowCount / (double)pageSize);
						List<GuestList> guestPage = guests.Skip(pageSize * index).Take(pageSize).ToList(); //Skip past rows and take new elements
						page.results = guestPage;
					}

					return page;
				}
			}
			catch (Exception ex)
			{
				Console.Write("GuestManager.getListOfGuests error " + ex);
				return null;
			}
		}

		public bool hasAcceptedSub(long userID)
		{
			try
			{
				using (conferenceadminContext context = new conferenceadminContext())
				{
					usersubmission userSub = context.usersubmission.Where(c => c.userID == userID).FirstOrDefault();
					if (userSub == null) return false;
					else
					{
						if (userSub.submission == null) return false;
						else 
						{ 
							if (userSub.submission.status == "Accepted") return true; 
							else if (userSub.submission.status != "Accepted") return false; 
						}
						if (userSub.submission1 == null) return false;
						else 
						{ 
							if (userSub.submission1.status == "Accepted") return true; 
							else if (userSub.submission1.status != "Accepted")return false; 
						}
						return false;
					}

				}
			}
			catch (Exception ex)
			{
				Console.Write("GuestManager.hasAcceptedSub error " + ex);
				return false;
			}            
		}


		/* [Randy] Search within the list with a certain criteria */
		public GuestsPagingQuery searchGuest(int index, string criteria)
		{
			GuestsPagingQuery page = new GuestsPagingQuery();
			try
			{
				using (conferenceadminContext context = new conferenceadminContext())
				{
					int pageSize = 10;
					var guestsThatApplied = context.users.Where(c => ((c.firstName + " " + c.lastName).ToLower().Contains(criteria.ToLower()) || c.usertype.userTypeName.ToLower().Contains(criteria.ToLower()) || c.acceptanceStatus.ToLower().Contains(criteria.ToLower())) && c.hasApplied == true && c.deleted != true).ToList();
					List<GuestList> guests = new List<GuestList>();
					foreach (var i in guestsThatApplied)
					{
						long userID = (int)i.userID;
						String firstName = i.firstName;
						String lastName = i.lastName;
						String title = i.title;
						String affiliationName = i.affiliationName;
						String userTypeName = i.usertype.userTypeName;
						bool isRegistered = (i.registrationStatus == "Accepted" ? true : false);
						String registrationStatus = i.registrationStatus;
						String acceptanceStatus = i.acceptanceStatus;
						String optionStatus = "Accepted";
						bool? authorizationStatus = i.minors.FirstOrDefault() == null ? null : i.minors.FirstOrDefault().authorizationStatus;
						String line1 = i.address.line1;
						String line2 = i.address.line2;
						String city = i.address.city;
						String state = i.address.state;
						String country = i.address.country;
						String zipcode = i.address.zipcode;
						String email = i.membership.email;
						String phoneNumber = i.phone;
						String fax = i.userFax;
						bool? day1 = i.registrations.FirstOrDefault() == null ? null : i.registrations.FirstOrDefault().date1;
						bool? day2 = i.registrations.FirstOrDefault() == null ? null : i.registrations.FirstOrDefault().date2;
						bool? day3 = i.registrations.FirstOrDefault() == null ? null : i.registrations.FirstOrDefault().date3;
						String companionFirstName = i.companions.FirstOrDefault() == null ? null : i.companions.FirstOrDefault().user.firstName;
						String companionLastName = i.companions.FirstOrDefault() == null ? null : i.companions.FirstOrDefault().user.lastName;
						long companionID = i.companions.FirstOrDefault() == null ? -1 : (long)i.companions.FirstOrDefault().userID;
						//check if user has accepted subs
						bool hasAcceptedSub = false;                        
						List<usersubmission> allSubs = i.usersubmissions.ToList();
						foreach (usersubmission sub in allSubs)
						{
							//check if initial subs accepted
							if(sub.submission1 != null){
								if(sub.submission1.status == "Accepted")
								{
									hasAcceptedSub = true;
								}
							}
							//check if final subs accepted
							if (sub.submission != null) {
								if (sub.submission.status == "Accepted")
								{
									hasAcceptedSub = true;
								}
							}
						}
						var guest = new GuestList(userID, firstName, lastName, title, affiliationName, userTypeName,
							authorizationStatus, isRegistered, registrationStatus, acceptanceStatus, line1, line2,
							city, state, country, zipcode, email, phoneNumber, fax, day1,
							day2, day3, optionStatus, companionFirstName, companionLastName, companionID, hasAcceptedSub);
						guests.Add(guest);
					}
					guests = guests.OrderBy(f => f.firstName).ToList();

					page.rowCount = guests.Count();
					if (page.rowCount > 0)
					{
						page.maxIndex = (int)Math.Ceiling(page.rowCount / (double)pageSize);
						List<GuestList> guestPage = guests.Skip(pageSize * index).Take(pageSize).ToList(); //Skip past rows and take new elements
						page.results = guestPage;
					}

					return page;
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
					//send email to notify of acceptance status
					String email = context.users.Where(c => c.userID == guestID).FirstOrDefault().membership.email;
					try { sendAcceptanceStatusUpdate(email, acceptanceStatus); }

					catch (Exception ex)
					{
						Console.Write("GuestManager.sendAcceptanceStatusUpdate error " + ex);
						return false;
					}

					return true;
				}
			}
			catch (Exception ex)
			{
				Console.Write("GuestManager.updateAcceptanceStatus error " + ex);
				return false;
			}
		}

		//Send email when submission status has been changed
		private void sendAcceptanceStatusUpdate(string email, String acceptanceStatus)
		{
			MailAddress ccwic = new MailAddress(ccwicEmail);
			MailAddress user = new MailAddress(email);
			MailMessage mail = new System.Net.Mail.MailMessage(ccwic, user);
			if (acceptanceStatus != "rejectedRegistration")
			{
				mail.Subject = "Caribbean Celebration of Women in Computing";
				mail.Body = "Greetings, \n\n " +
					"The status to assist to the conference was changed to: " + acceptanceStatus + ". This change can also be seen in your profile by logging in through the following link: \n\n" +
					"http://136.145.116.238/#/Login/Log" + ".";
			}
			else {
				mail.Subject = "Caribbean Celebration of Women in Computing";
				mail.Body = "Greetings, \n\n " +
					"We regret to inform you that your registration to the Caribbean Celebration of Women in Computing has been rejected.";
			}

			SmtpClient smtp = new SmtpClient();
			smtp.Host = "smtp.gmail.com";
			smtp.Port = 587;

			smtp.Credentials = new NetworkCredential(
				ccwicEmail, ccwicEmailPass);
			smtp.EnableSsl = true;

			smtp.Send(mail);
		}

		public List<MinorAuthorizations> getMinorAuthorizations(int id)
		{
			try
			{
				using (conferenceadminContext context = new conferenceadminContext())
				{
					List<MinorAuthorizations> authorizations = new List<MinorAuthorizations>();
					authorizations = context.authorizationsubmitteds.Where(c => c.minor.userID == id && c.deleted == false).
						Select(i => new MinorAuthorizations
							{
								authorizationSubmittedID = i.authorizationSubmittedID,
								documentName = i.documentName,
								//documentFile = i.documentFile
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
					//reject user's submission
					if (guest.usersubmissions.FirstOrDefault() != null)//if user has submissions
					{
						foreach (var userSub in guest.usersubmissions.ToList())
						{
							if (userSub.submission != null) 
								userSub.submission.status = "Rejected";
							if (userSub.submission1 != null) 
								userSub.submission1.status = "Rejected";
							context.SaveChanges();
						}
					}
					//send email to reject
					String email = context.users.Where(c => c.userID == id).FirstOrDefault().membership.email;
					try { sendAcceptanceStatusUpdate(email, "rejectedRegistration"); }

					catch (Exception ex)
					{
						Console.Write("GuestManager.sendAcceptanceStatusUpdate error " + ex);
						return false;
					}
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

	public class GuestsPagingQuery
	{
		public int indexPage;
		public int maxIndex;
		public int rowCount;
		public List<GuestList> results;
	}

	public class MinorAuthorizations
	{
		public int authorizationSubmittedID;
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
		public bool hasAcceptedSub;

		public GuestList(long userID, String firstName, String lastName, String title, String affiliationName, String userTypeName,
			bool? authorizationStatus, bool isRegistered, String registrationStatus, String acceptanceStatus, String line1, String line2,
			String city, String state, String country, String zipcode, String email, String phoneNumber, String fax, bool? day1,
			bool? day2, bool? day3, String optionStatus, String companionFirstName, String companionLastName, long companionID, bool hasAcceptedSub)
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
			this.hasAcceptedSub = hasAcceptedSub;
		}

		public GuestList()
		{
			// TODO: Complete member initialization
		}
	}
}