using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Nancy;
using System.Drawing;


namespace NancyService.Modules
{
	public class SponsorManager
	{
		public class addComplementary
		{
			public long sponsorID { get; set; }
			public string company { get; set; }
			public int quantity { get; set; }

		}
		public class SponsorQuery
		{
			public long sponsorID { get; set; }
			public int sponsorType { get; set; }
			public long addressID { get; set; }
			public double amount { get; set; }
			public string firstName { get; set; }
			public string lastName { get; set; }
			public string title { get; set; }
			public string company { get; set; }
			public string phone { get; set; }
			public string email { get; set; }
			public string logo { get; set; }
			public string logoName { get; set; }
			public string city { get; set; }
			public string line1 { get; set; }
			public string line2 { get; set; }
			public string state { get; set; }
			public string zipcode { get; set; }
			public string country { get; set; }
			public string transactionID { get; set; }
			public string method { get; set; }
			public string typeName { get; set; }
			public long paymentID { get; set; }
			public bool byAdmin { get; set; }
			public bool active { get; set; }
			public double newAmount { get; set; }
			public string userFax { get; set; }


		}
		public class SponsorTypeQuery
		{
			public int sponsortypeID { get; set; }
			public string name { get; set; }
			public double amount { get; set; }
			public string benefit1 { get; set; }
			public string benefit2 { get; set; }
			public string benefit3 { get; set; }
			public string benefit4 { get; set; }
			public string benefit5 { get; set; }
			public string benefit6 { get; set; }
			public string benefit7 { get; set; }
			public string benefit8 { get; set; }
			public string benefit9 { get; set; }
			public string benefit10 { get; set; }
		}

		public class ComplementaryQuery
		{
			public long complementarykeyID { get; set; }
			public long userID { get; set; }
			public string key { get; set; }
			public bool isUsed { get; set; }
			public long sponsorID { get; set; }
			public int quantity { get; set; }
			public string company { get; set; }
			public string name { get; set; }
		}

		public class SponsorPagingQuery
		{
			public int indexPage;
			public int maxIndex;
			public int rowCount;
			public List<SponsorQuery> results;

			public SponsorPagingQuery()
			{
				results = new List<SponsorQuery>();
			}
		}

		public class ComplimentaryPagingQuery
		{
			public int indexPage { get; set; }
			public int maxIndex { get; set; }
			public int rowCount { get; set; }
			public long sponsorID { get; set; }
			public long userID { get; set; }
			public int index { get; set; }
			public List<ComplementaryQuery> results { get; set; }

			public ComplimentaryPagingQuery()
			{
				results = new List<ComplementaryQuery>();
			}
		}


		public bool getSponsorDeadline()
		{
			try
			{
				using (conferenceadminContext context = new conferenceadminContext())
				{

					WebManager webManager = new WebManager();
					string deadline = webManager.getInterfaceElement("sponsorDeadline").content;

					var Day = Convert.ToInt32(deadline.Split('/')[1]);
					var Month = Convert.ToInt32(deadline.Split('/')[0]);
					var Year = Convert.ToInt32(deadline.Split('/')[2]);

					DateTime submissionDeadline = new DateTime(Year, Month, Day);


					return (DateTime.Compare(submissionDeadline, DateTime.Now.Date) >= 0);
				}
			}
			catch (Exception ex)
			{
				Console.Write("SubmissionManager.getSubmissionDeadline error " + ex);
				return false;
			}
		}

		public SponsorQuery addSponsor(SponsorQuery x)
		{
			try
			{
				using (conferenceadminContext context = new conferenceadminContext())
				{
					address address = new address();
					address.city = x.city;
					address.country = x.country;
					address.state = x.state;
					address.zipcode = x.zipcode;
					address.line1 = x.line1;
					address.line2 = x.line2;
					context.addresses.Add(address);
					context.SaveChanges();

					user user = new user();
					user.membershipID = 1;
					user.firstName = x.firstName;
					user.lastName = x.lastName;
					user.userFax = x.userFax;
					user.phone = x.phone;
					user.addressID = address.addressID;
					user.affiliationName = x.company;
					user.userTypeID = 7;
					user.deleted = false;
					context.users.Add(user);
					context.SaveChanges();

					payment payment2 = new payment();
					payment2.paymentTypeID = 1;
					payment2.deleted = false;
					payment2.creationDate = DateTime.Now;
					context.payments.Add(payment2);
					context.SaveChanges();


					paymentbill bill = new paymentbill();
					bill.AmountPaid = (double)x.amount;
					bill.paymentID = payment2.paymentID;
					bill.methodOfPayment = x.method;
					bill.transactionid = x.transactionID;
					bill.completed = true;
					bill.quantity = (int)(x.amount * 100);
					bill.deleted = false;
					bill.date = DateTime.Now;
					context.paymentbills.Add(bill);
					context.SaveChanges();

					sponsor2 sponsor = new sponsor2();
					sponsor.userID = user.userID;
					sponsor.emailInfo = x.email;
					sponsor.logo = x.logo;
					sponsor.sponsorType = x.sponsorType;
					sponsor.totalAmount = x.amount;
					sponsor.deleted = false;
					sponsor.byAdmin = true;
					sponsor.active = true;
					sponsor.paymentID = payment2.paymentID;

					context.sponsor2.Add(sponsor);
					context.SaveChanges();
					x.sponsorID = sponsor.sponsorID;
					x.addressID = address.addressID;
					x.byAdmin = true;
					return x;
				}

			}
			catch (Exception ex)
			{
				Console.Write("AdminManager.addSponsor error " + ex);
				return null;
			}

		}

		public long getPaymentID(long sponsorID)
		{
			try
			{
				using (conferenceadminContext context = new conferenceadminContext())
				{
					var sponsor = (from s in context.sponsor2
						where s.sponsorID == sponsorID
						select s).FirstOrDefault();
					if (sponsor != null)
						return (long)sponsor.paymentID;
					else
					{
						return 0;
					}

				}
			}

			catch (Exception ex)
			{
				Console.Write("SponsorManger.getUserID error + ex");
				return -1;
			}
		}

		public SponsorPagingQuery getSponsorList(int index)
		{
			SponsorPagingQuery page = new SponsorPagingQuery();

			try
			{
				using (conferenceadminContext context = new conferenceadminContext())
				{
					int pageSize = 10;
					var sponsor = (from s in context.sponsor2
						join p in context.paymentbills on s.paymentID equals p.paymentID into sponsorsQuery
						where s.byAdmin == true && s.deleted == false && s.active == true 
						from x in sponsorsQuery.DefaultIfEmpty()

						select new SponsorQuery
						{
							sponsorID = s.sponsorID,
							firstName = s.user.firstName,
							lastName = s.user.lastName,
							company = s.user.affiliationName,
							title = s.user.title,
							email = s.emailInfo == null ? s.user.membership.email : s.emailInfo,
							logo = s.logo,
							phone = s.user.phone,
							addressID = s.user.addressID,
							city = s.user.address.city,
							line1 = s.user.address.line1,
							line2 = s.user.address.line2,
							state = s.user.address.state,
							zipcode = s.user.address.zipcode,
							country = s.user.address.country,
							sponsorType = (int)s.sponsorType,
							amount = s.totalAmount,
							method = s.byAdmin == true ? x.methodOfPayment : null,
							transactionID = s.byAdmin == true ? x.transactionid : null,
							userFax = s.user.userFax,
							byAdmin = s.byAdmin,
							typeName = s.sponsortype1.name,
							active = (bool)s.active,


						}).Union((from s in context.sponsor2
							join p in context.paymentbills on s.paymentID equals p.paymentID into sponsorsQuery
							where s.byAdmin == false && s.deleted == false
							from x in sponsorsQuery.DefaultIfEmpty()

							select new SponsorQuery
							{
								sponsorID = s.sponsorID,
								firstName = s.user.firstName,
								lastName = s.user.lastName,
								company = s.user.affiliationName,
								title = s.user.title,
								email = s.emailInfo == null ? s.user.membership.email : s.emailInfo,
								logo = s.logo,
								phone = s.user.phone,
								addressID = s.user.addressID,
								city = s.user.address.city,
								line1 = s.user.address.line1,
								line2 = s.user.address.line2,
								state = s.user.address.state,
								zipcode = s.user.address.zipcode,
								country = s.user.address.country,
								sponsorType = (int)s.sponsorType,
								amount = s.totalAmount,
								method = "",
								transactionID = "",
								userFax = s.user.userFax,
								byAdmin = s.byAdmin,
								typeName = s.sponsortype1.name,
								active = (bool)s.active,


							})).OrderBy(x => x.sponsorID);
					page.rowCount = sponsor.Count();
					if (page.rowCount > 0)
					{
						page.maxIndex = (int)Math.Ceiling(page.rowCount / (double)pageSize);
						var sponsorList = sponsor.Skip(pageSize * index).Take(pageSize).ToList(); //Skip past rows and take new elements
						page.results = sponsorList;
					}

					return page;
				}


			}
			catch (Exception ex)
			{
				Console.Write("SponsorManager.getSponsor(index) error " + ex);
				return null;
			}

		}

		public SponsorQuery getSponsorbyID(long x)
		{
			try
			{
				using (conferenceadminContext context = new conferenceadminContext())
				{

					var sponsor = (from s in context.sponsor2
						join p in context.paymentbills on s.paymentID equals p.paymentID into sponsorsQuery
						where s.byAdmin == true && s.deleted == false && x == s.userID
						from y in sponsorsQuery.DefaultIfEmpty()

						select new SponsorQuery
						{
							sponsorID = s.sponsorID,
							firstName = s.user.firstName,
							lastName = s.user.lastName,
							company = s.user.affiliationName,
							title = s.user.title,
							email = s.emailInfo == null ? s.user.membership.email : s.emailInfo,
							logo = s.logo,
							phone = s.user.phone,
							addressID = s.user.addressID,
							city = s.user.address.city,
							line1 = s.user.address.line1,
							line2 = s.user.address.line2,
							state = s.user.address.state,
							zipcode = s.user.address.zipcode,
							country = s.user.address.country,
							sponsorType = (int)s.sponsorType,
							amount = s.totalAmount,
							method = s.byAdmin == true ? y.methodOfPayment : null,
							transactionID = s.byAdmin == true ? y.transactionid : null,
							userFax = s.user.userFax,
							byAdmin = s.byAdmin,
							typeName = s.sponsortype1.name,
							active = (bool)s.active,


						}).FirstOrDefault();

					if (sponsor == null)
					{

						sponsor = (from s in context.sponsor2
							join p in context.paymentbills on s.paymentID equals p.paymentID into sponsorsQuery
							where s.byAdmin == false && s.deleted == false && x == s.userID
							from y in sponsorsQuery.DefaultIfEmpty()

							select new SponsorQuery
							{
								sponsorID = s.sponsorID,
								firstName = s.user.firstName,
								lastName = s.user.lastName,
								company = s.user.affiliationName,
								title = s.user.title,
								email = s.emailInfo == null ? s.user.membership.email : s.emailInfo,
								logo = s.logo,
								phone = s.user.phone,
								addressID = s.user.addressID,
								city = s.user.address.city,
								line1 = s.user.address.line1,
								line2 = s.user.address.line2,
								state = s.user.address.state,
								zipcode = s.user.address.zipcode,
								country = s.user.address.country,
								sponsorType = (int)s.sponsorType,
								amount = s.totalAmount,
								method = "",
								transactionID = "",
								userFax = s.user.userFax,
								byAdmin = s.byAdmin,
								typeName = s.sponsortype1.name,
								active = (bool)s.active,


							}).FirstOrDefault();

					}


					return sponsor;
				}


			}
			catch (Exception ex)
			{
				Console.Write("SponsorManager.getSponsor error " + ex);
				return null;
			}

		}

		public List<SponsorTypeQuery> getSponsorTypesList()
		{
			try
			{
				using (conferenceadminContext context = new conferenceadminContext())
				{
					var types = (from s in context.sponsortypes
						select new SponsorTypeQuery
						{
							sponsortypeID = s.sponsortypeID,
							name = s.name,
							amount = s.amount,
							benefit1 = s.benefit1,
							benefit2 = s.benefit2,
							benefit3 = s.benefit3,
							benefit4 = s.benefit4,
							benefit5 = s.benefit5,
							benefit6 = s.benefit6,
							benefit7 = s.benefit7,
							benefit8 = s.benefit8,
							benefit9 = s.benefit9,
							benefit10 = s.benefit10,

						}).ToList();
					return types;
				}

			}
			catch (Exception ex)
			{
				Console.Write("SponsorManager.getSponsorType error " + ex);
				return null;
			}

		}

		public SponsorQuery updateSponsor(SponsorQuery x)
		{
			try
			{
				using (conferenceadminContext context = new conferenceadminContext())
				{   var sponsor = new sponsor2();
					if (x.byAdmin){
						sponsor = (from s in context.sponsor2
							from p in context.paymentbills
							where s.sponsorID == x.sponsorID && s.paymentID == p.paymentID
							select s).FirstOrDefault();
					}

					else{
						sponsor = (from s in context.sponsor2
							where s.sponsorID == x.sponsorID 
							select s).FirstOrDefault();
					}
					if (sponsor != null )
					{
						sponsor.user.firstName = x.firstName;
						sponsor.user.lastName = x.lastName;
						sponsor.logo = x.logo;
						sponsor.user.phone = x.phone;
						sponsor.sponsorType = x.sponsorType;
						sponsor.user.userFax = x.userFax;
						sponsor.user.address.city = x.city;
						sponsor.user.address.country = x.country;
						sponsor.user.address.state = x.state;
						sponsor.user.address.zipcode = x.zipcode;
						sponsor.user.address.line1 = x.line1;
						sponsor.user.address.line2 = x.line2;
						sponsor.totalAmount = x.amount;

						context.SaveChanges();
						if (x.byAdmin)
						{
							var payment = (from p in context.paymentbills
								where sponsor.paymentID == p.paymentID
								select p).FirstOrDefault();
							payment.AmountPaid = x.amount;
							payment.methodOfPayment = x.method;
							payment.quantity = (int)(x.amount * 100);
							payment.date = DateTime.Now;
							context.SaveChanges();

						}
					}

					return x;
				}
			}
			catch (Exception ex)
			{
				Console.Write("SponsorManager.updateSponsor error " + ex);
				return null;
			}
		}

		public bool deleteSponsor(long x)
		{
			try
			{
				using (conferenceadminContext context = new conferenceadminContext())
				{

					var sponsor = (from s in context.sponsor2
						where s.sponsorID == x
						select s).FirstOrDefault();
					if (sponsor != null)
					{
						sponsor.user.membership.deleted = true;
						sponsor.user.deleted = true;
						sponsor.deleted = true;
						context.SaveChanges();
					}
					return true;
				}
			}
			catch (Exception ex)
			{
				Console.Write("SponsorManager.deleteSponsor error " + ex);
				return false;
			}
		}

		public List<ComplementaryQuery> getComplementaryList()
		{
			try
			{
				using (conferenceadminContext context = new conferenceadminContext())
				{

					var keysUsed = (from s in context.complementarykeys
						from pay in context.paymentcomplementaries
						from r in context.registrations
						from u in context.users
						where s.isUsed == true && s.deleted == false &&
						s.complementarykeyID == pay.complementaryKeyID && r.paymentID == pay.paymentID &&
						r.userID == u.userID
						select new ComplementaryQuery
						{
							complementarykeyID = s.complementarykeyID,
							key = s.key,
							isUsed = (bool)s.isUsed,
							sponsorID = s.sponsorID2,
							userID = r.userID,
							name = u.firstName + " " + u.lastName,



						}).ToList();

					var keysUnused = (from s in context.complementarykeys
						where s.isUsed == false && s.deleted == false
						select new ComplementaryQuery
						{
							complementarykeyID = s.complementarykeyID,
							key = s.key,
							isUsed = (bool)s.isUsed,
							userID = 0,
							name = "",
						}).ToList();

					List<ComplementaryQuery> allKeys = (List<ComplementaryQuery>)keysUnused.Concat(keysUnused);

					return allKeys;
				}

			}
			catch (Exception ex)
			{
				Console.Write("SponsorManager.getSponsorType error " + ex);
				return null;
			}

		}

		public List<ComplementaryQuery> getSponsorComplementaryList(long id)
		{
			try
			{
				using (conferenceadminContext context = new conferenceadminContext())
				{

					var keysUsed = (from s in context.complementarykeys
						from pay in context.paymentcomplementaries
						from r in context.registrations
						from u in context.users
						where s.sponsorID2 == id && s.isUsed == true && s.deleted == false &&
						s.complementarykeyID == pay.complementaryKeyID && r.paymentID == pay.paymentID &&
						r.userID == u.userID
						select new ComplementaryQuery
						{
							complementarykeyID = s.complementarykeyID,
							key = s.key,
							isUsed = (bool)s.isUsed,
							sponsorID = s.sponsorID2,
							userID = r.userID,
							name = u.firstName + " " + u.lastName,

						}).ToList();

					var keysUnused = (from s in context.complementarykeys
						where s.sponsorID2 == id && s.isUsed == false && s.deleted == false
						select new ComplementaryQuery
						{
							complementarykeyID = s.complementarykeyID,
							key = s.key,
							isUsed = (bool)s.isUsed,
							userID = 0,
							name = "",
						}).ToList();

					var list = keysUnused.Concat(keysUsed).ToList();
					return list;

				}

			}
			catch (Exception ex)
			{
				Console.Write("SponsorManager.getSponsorComplentaryList error " + ex);
				return null;
			}

		}

		public ComplimentaryPagingQuery getSponsorComplementaryList(ComplimentaryPagingQuery page)
		{
			try
			{
				int pageSize = 10;
				using (conferenceadminContext context = new conferenceadminContext())
				{

					var keys = (from s in context.complementarykeys
						from pay in context.paymentcomplementaries
						from r in context.registrations
						from u in context.users
						where s.sponsorID2 == page.sponsorID && s.isUsed == true && s.deleted == false &&
						s.complementarykeyID == pay.complementaryKeyID && r.paymentID == pay.paymentID &&
						r.userID == u.userID
						select new ComplementaryQuery
						{
							complementarykeyID = s.complementarykeyID,
							key = s.key,
							isUsed = (bool)s.isUsed,
							sponsorID = s.sponsorID2,
							userID = r.userID,
							name = u.firstName + " " + u.lastName,
						}).Concat((from s in context.complementarykeys
							where s.sponsorID2 == page.sponsorID && s.isUsed == false && s.deleted == false
							select new ComplementaryQuery
							{
								complementarykeyID = s.complementarykeyID,
								key = s.key,
								isUsed = (bool)s.isUsed,
								sponsorID = s.sponsorID2,
								userID = 0,
								name = "",
							})).OrderBy(x => x.complementarykeyID);

					page.rowCount = keys.Count();
					if (page.rowCount > 0)
					{
						page.maxIndex = (int)Math.Ceiling(page.rowCount / (double)pageSize);
						var keyList = keys.Skip(pageSize * page.index).Take(pageSize).ToList(); //Skip past rows and take new elements
						page.results = keyList;
					}

					return page;
				}

			}
			catch (Exception ex)
			{
				Console.Write("SponsorManager.getSponsorComplentaryList(index) error " + ex);
				return null;
			}

		}

		public bool deleteComplementary(long x)
		{
			try
			{
				using (conferenceadminContext context = new conferenceadminContext())
				{
					//delete si nadie ha pagado con el hasta este momento. 
					var key = (from s in context.complementarykeys
						where s.complementarykeyID == x && s.isUsed == false
						select s).FirstOrDefault();

					key.deleted = true;
					context.SaveChanges();
					return true;

				}
			}
			catch (Exception ex)
			{
				Console.Write("SponsorManager.Deletekey error " + ex);
				return false;
			}


		}

		public SponsorPagingQuery searchSponsors(int index, String criteria)
		{
			SponsorPagingQuery page = new SponsorPagingQuery();

			try
			{
				using (conferenceadminContext context = new conferenceadminContext())
				{

					int pageSize = 10;
					var sponsor = (from s in context.sponsor2
						where ((s.byAdmin == true && s.active == true && s.deleted == false) && ((s.user.firstName.ToLower() + " " + s.user.lastName.ToLower()).Contains(criteria.ToLower()) || s.user.affiliationName.Contains(criteria.ToLower()) || s.user.membership.email.ToLower().Contains(criteria.ToLower())))
						join p in context.paymentbills on s.paymentID equals p.paymentID into sponsorsQuery
						from x in sponsorsQuery.DefaultIfEmpty()

						select new SponsorQuery
						{
							sponsorID = s.sponsorID,
							firstName = s.user.firstName,
							lastName = s.user.lastName,
							company = s.user.affiliationName,
							title = s.user.title,
							email = s.emailInfo == null ? s.user.membership.email : s.emailInfo,
							logo = s.logo,
							phone = s.user.phone,
							addressID = s.user.addressID,
							city = s.user.address.city,
							line1 = s.user.address.line1,
							line2 = s.user.address.line2,
							state = s.user.address.state,
							userFax =s.user.userFax,
							zipcode = s.user.address.zipcode,
							country = s.user.address.country,
							sponsorType = (int)s.sponsorType,
							amount = s.totalAmount,
							method = s.byAdmin == true ? x.methodOfPayment : null,
							transactionID = s.byAdmin == true ? x.transactionid : null,
							byAdmin = s.byAdmin,
							typeName = s.sponsortype1.name,
							active = (bool)s.active,


						}).Concat((from s in context.sponsor2
							join p in context.paymentbills on s.paymentID equals p.paymentID into sponsorsQuery
							where ((s.byAdmin == false && s.active == true && s.deleted == false) && ((s.user.firstName.ToLower() + " " + s.user.lastName.ToLower()).Contains(criteria.ToLower()) || s.user.affiliationName.Contains(criteria.ToLower()) || s.user.membership.email.ToLower().Contains(criteria.ToLower())))
							from x in sponsorsQuery.DefaultIfEmpty()

							select new SponsorQuery
							{
								sponsorID = s.sponsorID,
								firstName = s.user.firstName,
								lastName = s.user.lastName,
								company = s.user.affiliationName,
								title = s.user.title,
								email = s.emailInfo == null ? s.user.membership.email : s.emailInfo,
								logo = s.logo,
								phone = s.user.phone,
								addressID = s.user.addressID,
								city = s.user.address.city,
								line1 = s.user.address.line1,
								line2 = s.user.address.line2,
								state = s.user.address.state,
								userFax = s.user.userFax,
								zipcode = s.user.address.zipcode,
								country = s.user.address.country,
								sponsorType = (int)s.sponsorType,
								amount = s.totalAmount,
								method = "",
								transactionID = "",
								byAdmin = s.byAdmin,
								typeName = s.sponsortype1.name,
								active = (bool)s.active,


							})).OrderBy(x => x.sponsorID);
					page.rowCount = sponsor.Count();
					if (page.rowCount > 0)
					{
						page.maxIndex = (int)Math.Ceiling(page.rowCount / (double)pageSize);
						var sponsorList = sponsor.Skip(pageSize * index).Take(pageSize).ToList(); //Skip past rows and take new elements
						page.results = sponsorList;
					}

					return page;
				}


			}
			catch (Exception ex)
			{
				Console.Write("SponsorManager.searchSponsor(index) error " + ex);
				return null;
			}

		}

		public ComplimentaryPagingQuery searchKeyCodes(ComplimentaryPagingQuery page, String criteria)
		{
			ComplimentaryPagingQuery e = new ComplimentaryPagingQuery();

			try
			{
				int pageSize = 10;
				using (conferenceadminContext context = new conferenceadminContext())
				{

					var keys = (from s in context.complementarykeys
						from pay in context.paymentcomplementaries
						from k in context.payments
						from r in context.registrations
						from u in context.users
						where (s.sponsorID2 == page.sponsorID && s.isUsed == true && s.deleted == false && pay.paymentID == k.paymentID &&
							s.complementarykeyID == pay.complementaryKeyID && r.paymentID == k.paymentID &&
							r.userID == u.userID) && ((u.firstName.ToLower() + " " + u.lastName.ToLower()).Contains(criteria.ToLower()) || s.key.ToLower().Contains(criteria.ToLower()))
						select new ComplementaryQuery
						{
							complementarykeyID = s.complementarykeyID,
							key = s.key,
							isUsed = (bool)s.isUsed,
							sponsorID = s.sponsorID2,
							userID = r.userID,
							name = u.firstName + " " + u.lastName,
						}).Concat((from s in context.complementarykeys
							where (s.sponsorID2 == page.sponsorID && s.isUsed == false && s.deleted == false) && (s.key.ToLower().Contains(criteria.ToLower()))
							select new ComplementaryQuery
							{
								complementarykeyID = s.complementarykeyID,
								key = s.key,
								isUsed = (bool)s.isUsed,
								sponsorID = s.sponsorID2,
								userID = 0,
								name = "",
							})).OrderBy(x => x.complementarykeyID);

					page.rowCount = keys.Count();
					if (page.rowCount > 0)
					{
						page.maxIndex = (int)Math.Ceiling(page.rowCount / (double)pageSize);
						var keyList = keys.Skip(pageSize * page.index).Take(pageSize).ToList(); //Skip past rows and take new elements
						page.results = keyList;
					}

					return page;
				}
			}
			catch (Exception ex)
			{
				Console.Write("SponsorManager.searchKeyCodes error " + ex);
				return null;
			}
		}

		public ComplimentaryPagingQuery deleteComplementarySponsor(long x)
		{
			try
			{
				using (conferenceadminContext context = new conferenceadminContext())
				{
					//delete si nadie ha pagado con el hasta este momento. 
					context.complementarykeys
						.Where(s => s.sponsorID2 == x && s.isUsed == false)
						.ToList().ForEach(s => { s.deleted = true; });

					context.SaveChanges();

					ComplimentaryPagingQuery page = new ComplimentaryPagingQuery();
					page.index = 0;
					page.sponsorID = x;
					return getSponsorComplementaryList(page);
				}
			}
			catch (Exception ex)
			{
				Console.Write("SponsorManager.Deletekey error " + ex);
				return null;
			}


		}

		public ComplimentaryPagingQuery addKeysTo(addComplementary obj)
		{
			try
			{
				using (conferenceadminContext context = new conferenceadminContext())
				{


					for (int i = 0; i < obj.quantity; i++)
					{
						complementarykey c = new complementarykey();
						c.sponsorID2 = obj.sponsorID;
						c.isUsed = false;
						c.deleted = false;
						c.key = "CCWIC-" + obj.company + "-" + GenerateComplementary(30);
						context.complementarykeys.Add(c);
					}

					context.SaveChanges();

					ComplimentaryPagingQuery page = new ComplimentaryPagingQuery();
					page.index = 0;
					page.sponsorID = obj.sponsorID;
					//return getSponsorComplentaryList(obj.sponsorID);
					return getSponsorComplementaryList(page);
				}
			}
			catch (Exception ex)
			{
				Console.Write("SponsorManager.CoplementarykeyGenerator error " + ex);
				return null;
			}


		}

		private string GenerateComplementary(int length)
		{
			return Guid.NewGuid().ToString().Substring(0, 9);
		}
	}

}
