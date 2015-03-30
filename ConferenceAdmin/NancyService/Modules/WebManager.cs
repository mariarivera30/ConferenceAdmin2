using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyService.Modules
{
    public class WebManager
    {
        public WebManager()
        {

        }

        public ContentQuery getInterfaceElement(String element)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    ContentQuery webInterface = new ContentQuery();

                    webInterface = context.interfaceinformations.Where(inter => inter.attribute == element).Select(inter => new ContentQuery
                    {
                        interfaceID = (int)inter.interfaceID,
                        content = inter.content

                    }).FirstOrDefault();

                    return webInterface;
                }
            }
            catch (Exception ex)
            {
                Console.Write("WebManager.get" + element + " error " + ex);
                return null;
            }

        }

        public HomeQuery getHome()
        {
            try
            {
                HomeQuery home = new HomeQuery();
                home.conferenceName = this.getInterfaceElement("conferenceName").content;
                home.homeMainTitle = this.getInterfaceElement("homeMainTitle").content;
                home.homeTitle1 = this.getInterfaceElement("homeTitle1").content;
                home.homeParagraph1 = this.getInterfaceElement("homeParagraph1").content;
                home.homeTitle2 = this.getInterfaceElement("homeTitle2").content;
                home.homeParagraph2 = this.getInterfaceElement("homeParagraph2").content;

                using (conferenceadminContext context = new conferenceadminContext())
                {

                    var img = (from s in context.interfacedocuments
                                where s.attibuteName == "homeImage"
                                select s).FirstOrDefault();

                    if (img != null)
                    {
                        home.image = img.content;
                    }
                }

                return home;
            }
            catch (Exception ex)
            {
                Console.Write("WebManager.getHome error " + ex);
                return null;
            }
        }

        public bool saveHome(HomeQuery newHome)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var homeMainTitle = (from s in context.interfaceinformations
                                         where s.attribute == "homeMainTitle"
                                         select s).FirstOrDefault();
                    if (homeMainTitle != null)
                        homeMainTitle.content = newHome.homeMainTitle;

                    var homeTitle1 = (from s in context.interfaceinformations
                                      where s.attribute == "homeTitle1"
                                      select s).FirstOrDefault();

                    if (homeTitle1 != null)
                        homeTitle1.content = newHome.homeTitle1;

                    var homeParagraph1 = (from s in context.interfaceinformations
                                          where s.attribute == "homeParagraph1"
                                          select s).FirstOrDefault();

                    if (homeParagraph1 != null)
                        homeParagraph1.content = newHome.homeParagraph1;

                    var homeTitle2 = (from s in context.interfaceinformations
                                      where s.attribute == "homeTitle2"
                                      select s).FirstOrDefault();

                    if (homeTitle2 != null)
                        homeTitle2.content = newHome.homeTitle2;

                    var homeParagraph2 = (from s in context.interfaceinformations
                                          where s.attribute == "homeParagraph2"
                                          select s).FirstOrDefault();

                    if (homeParagraph2 != null)
                        homeParagraph2.content = newHome.homeParagraph2;

                    var img = (from s in context.interfacedocuments
                                where s.attibuteName == "homeImage"
                                select s).FirstOrDefault();

                    if (img != null)
                        img.content = newHome.image;

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("WebManager.saveHome error " + ex);
                return false;
            }
        }

        public bool removeImage(String src)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {   
                    var img = (from s in context.interfacedocuments
                               where s.attibuteName == src
                               select s).FirstOrDefault();

                    if (img != null)
                        img.content = "";

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("WebManager.removeImage error " + ex);
                return false;
            }
        }

        public VenueQuery getVenue()
        {
            try
            {
                VenueQuery venue = new VenueQuery();
                venue.venueTitle1 = this.getInterfaceElement("venueTitle1").content;
                venue.venueParagraph1 = this.getInterfaceElement("venueParagraph1").content;
                venue.venueTitle2 = this.getInterfaceElement("venueTitle2").content;
                venue.venueParagraph2 = this.getInterfaceElement("venueParagraph2").content;
                venue.venueTitleBox = this.getInterfaceElement("venueTitleBox").content;
                venue.venueParagraphContentBox = this.getInterfaceElement("venueParagraphContentBox").content;

                return venue;
            }
            catch (Exception ex)
            {
                Console.Write("WebManager.getVenue error " + ex);
                return null;
            }
        }

        public bool saveVenue(VenueQuery newVenue)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var venueTitle1 = (from s in context.interfaceinformations
                                       where s.attribute == "venueTitle1"
                                       select s).FirstOrDefault();
                    if (venueTitle1 != null)
                        venueTitle1.content = newVenue.venueTitle1;


                    var venueParagraph1 = (from s in context.interfaceinformations
                                           where s.attribute == "venueParagraph1"
                                           select s).FirstOrDefault();
                    if (venueParagraph1 != null)
                        venueParagraph1.content = newVenue.venueParagraph1;


                    var venueTitle2 = (from s in context.interfaceinformations
                                       where s.attribute == "venueTitle2"
                                       select s).FirstOrDefault();

                    if (venueTitle2 != null)
                        venueTitle2.content = newVenue.venueTitle2;


                    var venueParagraph2 = (from s in context.interfaceinformations
                                           where s.attribute == "venueParagraph2"
                                           select s).FirstOrDefault();

                    if (venueParagraph2 != null)
                        venueParagraph2.content = newVenue.venueParagraph2;


                    var venueTitleBox = (from s in context.interfaceinformations
                                         where s.attribute == "venueTitleBox"
                                         select s).FirstOrDefault();

                    if (venueTitleBox != null)
                        venueTitleBox.content = newVenue.venueTitleBox;


                    var venueParagraphContentBox = (from s in context.interfaceinformations
                                                    where s.attribute == "venueParagraphContentBox"
                                                    select s).FirstOrDefault();

                    if (venueParagraphContentBox != null)
                        venueParagraphContentBox.content = newVenue.venueParagraphContentBox;

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("WebManger.saveVenue error " + ex);
                return false;
            }
        }

        public ContactQuery getContact()
        {
            try
            {
                ContactQuery contact = new ContactQuery();
                contact.contactName = this.getInterfaceElement("contactName").content;
                contact.contactPhone = this.getInterfaceElement("contactPhone").content;
                contact.contactEmail = this.getInterfaceElement("contactEmail").content;
                contact.contactAdditionalInfo = this.getInterfaceElement("contactAdditionalInfo").content;

                return contact;
            }
            catch (Exception ex)
            {
                Console.Write("WebManager.getContact error " + ex);
                return null;
            }
        }

        public bool saveContact(ContactQuery newContact)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var contactName = (from s in context.interfaceinformations
                                       where s.attribute == "contactName"
                                       select s).FirstOrDefault();
                    if (contactName != null)
                        contactName.content = newContact.contactName;

                    var contactPhone = (from s in context.interfaceinformations
                                        where s.attribute == "contactPhone"
                                        select s).FirstOrDefault();
                    if (contactPhone != null)
                        contactPhone.content = newContact.contactPhone;

                    var contactEmail = (from s in context.interfaceinformations
                                        where s.attribute == "contactEmail"
                                        select s).FirstOrDefault();

                    if (contactEmail != null)
                        contactEmail.content = newContact.contactEmail;

                    var contactAdditionalInfo = (from s in context.interfaceinformations
                                                 where s.attribute == "contactAdditionalInfo"
                                                 select s).FirstOrDefault();

                    if (contactAdditionalInfo != null)
                        contactAdditionalInfo.content = newContact.contactAdditionalInfo;

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("WebManger.saveContact error " + ex);
                return false;
            }
        }

        public ParticipationQuery getParticipation()
        {
            try
            {
                ParticipationQuery participation = new ParticipationQuery();
                participation.participationTitle1 = this.getInterfaceElement("participationTitle1").content;
                participation.participationParagraph1 = this.getInterfaceElement("participationParagraph1").content;
                participation.participationTitle2 = this.getInterfaceElement("participationTitle2").content;
                participation.participationParagraph2 = this.getInterfaceElement("participationParagraph2").content;
                participation.participationTitle3 = this.getInterfaceElement("participationTitle3").content;
                participation.participationParagraph3 = this.getInterfaceElement("participationParagraph3").content;
                participation.participationTitle4 = this.getInterfaceElement("participationTitle4").content;
                participation.participationParagraph4 = this.getInterfaceElement("participationParagraph4").content;
                participation.participationTitle5 = this.getInterfaceElement("participationTitle5").content;
                participation.participationParagraph5 = this.getInterfaceElement("participationParagraph5").content;

                return participation;
            }
            catch (Exception ex)
            {
                Console.Write("WebManager.getParticipation error " + ex);
                return null;
            }
        }

        public bool saveParticipation(ParticipationQuery newParticipation)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var participationTitle1 = (from s in context.interfaceinformations
                                               where s.attribute == "participationTitle1"
                                               select s).FirstOrDefault();
                    if (participationTitle1 != null)
                        participationTitle1.content = newParticipation.participationTitle1;

                    var participationTitle2 = (from s in context.interfaceinformations
                                               where s.attribute == "participationTitle2"
                                               select s).FirstOrDefault();
                    if (participationTitle2 != null)
                        participationTitle2.content = newParticipation.participationTitle2;

                    var participationTitle3 = (from s in context.interfaceinformations
                                               where s.attribute == "participationTitle3"
                                               select s).FirstOrDefault();
                    if (participationTitle3 != null)
                        participationTitle3.content = newParticipation.participationTitle3;

                    var participationTitle4 = (from s in context.interfaceinformations
                                               where s.attribute == "participationTitle4"
                                               select s).FirstOrDefault();
                    if (participationTitle4 != null)
                        participationTitle4.content = newParticipation.participationTitle4;

                    var participationTitle5 = (from s in context.interfaceinformations
                                               where s.attribute == "participationTitle5"
                                               select s).FirstOrDefault();
                    if (participationTitle5 != null)
                        participationTitle5.content = newParticipation.participationTitle5;

                    var participationParagraph1 = (from s in context.interfaceinformations
                                                   where s.attribute == "participationParagraph1"
                                                   select s).FirstOrDefault();
                    if (participationParagraph1 != null)
                        participationParagraph1.content = newParticipation.participationParagraph1;

                    var participationParagraph2 = (from s in context.interfaceinformations
                                                   where s.attribute == "participationParagraph2"
                                                   select s).FirstOrDefault();
                    if (participationParagraph2 != null)
                        participationParagraph2.content = newParticipation.participationParagraph2;

                    var participationParagraph3 = (from s in context.interfaceinformations
                                                   where s.attribute == "participationParagraph3"
                                                   select s).FirstOrDefault();
                    if (participationParagraph3 != null)
                        participationParagraph3.content = newParticipation.participationParagraph3;

                    var participationParagraph4 = (from s in context.interfaceinformations
                                                   where s.attribute == "participationParagraph4"
                                                   select s).FirstOrDefault();
                    if (participationParagraph4 != null)
                        participationParagraph4.content = newParticipation.participationParagraph4;

                    var participationParagraph5 = (from s in context.interfaceinformations
                                                   where s.attribute == "participationParagraph5"
                                                   select s).FirstOrDefault();
                    if (participationParagraph5 != null)
                        participationParagraph5.content = newParticipation.participationParagraph5;

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("WebManger.saveParticipation error " + ex);
                return false;
            }
        }

        public RegistrationQuery getRegistrationInfo()
        {
            try
            {
                RegistrationQuery registration = new RegistrationQuery();
                registration.registrationTitle1 = this.getInterfaceElement("registrationTitle1").content;
                registration.registrationParagraph1 = this.getInterfaceElement("registrationParagraph1").content;
                registration.registrationTitle2 = this.getInterfaceElement("registrationTitle2").content;
                registration.registrationParagraph2 = this.getInterfaceElement("registrationParagraph2").content;
                registration.registrationNotes = this.getInterfaceElement("registrationNotes").content;

                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var undergraduateStudent = (from usertype in context.usertypes
                                                where usertype.userTypeName == "Undergraduate Student"
                                                select usertype).FirstOrDefault();

                    if (undergraduateStudent != null)
                    {
                        registration.undergraduateStudentFee = (double)undergraduateStudent.registrationCost;
                        registration.undergraduateStudentLateFee = (double)undergraduateStudent.registrationLateFee;
                    }

                    var graduateStudent = (from usertype in context.usertypes
                                           where usertype.userTypeName == "Graduate Student"
                                           select usertype).FirstOrDefault();

                    if (graduateStudent != null)
                    {
                        registration.graduateStudentFee = (double)graduateStudent.registrationCost;
                        registration.graduateStudentLateFee = (double)graduateStudent.registrationLateFee;
                    }


                    var highSchoolStudent = (from usertype in context.usertypes
                                             where usertype.userTypeName == "High School Student"
                                             select usertype).FirstOrDefault();

                    if (highSchoolStudent != null)
                    {
                        registration.highSchoolStudentFee = (double)highSchoolStudent.registrationCost;
                        registration.highSchoolStudentLateFee = (double)highSchoolStudent.registrationLateFee;
                    }

                    var companionStudent = (from usertype in context.usertypes
                                            where usertype.userTypeName == "Companion"
                                            select usertype).FirstOrDefault();

                    if (companionStudent != null)
                    {
                        registration.companionStudentFee = (double)companionStudent.registrationCost;
                        registration.companionStudentLateFee = (double)companionStudent.registrationLateFee;
                    }

                    var professionalAcademia = (from usertype in context.usertypes
                                                where usertype.userTypeName == "Professional Academia"
                                                select usertype).FirstOrDefault();

                    if (professionalAcademia != null)
                    {
                        registration.professionalAcademyFee = (double)professionalAcademia.registrationCost;
                        registration.professionalAcademyLateFee = (double)professionalAcademia.registrationLateFee;
                    }

                    var professionalIndustry = (from usertype in context.usertypes
                                                where usertype.userTypeName == "Professional Industry"
                                                select usertype).FirstOrDefault();
                    if (professionalIndustry != null)
                    {
                        registration.professionalIndustryFee = (double)professionalIndustry.registrationCost;
                        registration.professionalIndustryLateFee = (double)professionalIndustry.registrationLateFee;
                    }
                }

                return registration;
            }
            catch (Exception ex)
            {
                Console.Write("WebManager.getRegistrationInfo error " + ex);
                return null;
            }
        }

        public bool saveRegistrationInfo(RegistrationQuery newRegistration)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var registrationTitle1 = (from s in context.interfaceinformations
                                              where s.attribute == "registrationTitle1"
                                              select s).FirstOrDefault();
                    if (registrationTitle1 != null)
                        registrationTitle1.content = newRegistration.registrationTitle1;

                    var registrationParagraph1 = (from s in context.interfaceinformations
                                                  where s.attribute == "registrationParagraph1"
                                                  select s).FirstOrDefault();
                    if (registrationParagraph1 != null)
                        registrationParagraph1.content = newRegistration.registrationParagraph1;

                    var registrationTitle2 = (from s in context.interfaceinformations
                                              where s.attribute == "registrationTitle2"
                                              select s).FirstOrDefault();

                    if (registrationTitle2 != null)
                        registrationTitle2.content = newRegistration.registrationTitle2;

                    var registrationParagraph2 = (from s in context.interfaceinformations
                                                  where s.attribute == "registrationParagraph2"
                                                  select s).FirstOrDefault();

                    if (registrationParagraph2 != null)
                        registrationParagraph2.content = newRegistration.registrationParagraph2;

                    var registrationNotes = (from s in context.interfaceinformations
                                             where s.attribute == "registrationNotes"
                                             select s).FirstOrDefault();

                    if (registrationNotes != null)
                        registrationNotes.content = newRegistration.registrationNotes;

                    var undergraduateStudentFee = (from s in context.usertypes
                                                   where s.userTypeName == "Undergraduate Student"
                                                   select s).FirstOrDefault();

                    if (undergraduateStudentFee != null)
                    {
                        undergraduateStudentFee.registrationCost = newRegistration.undergraduateStudentFee;
                        undergraduateStudentFee.registrationLateFee = newRegistration.undergraduateStudentLateFee;
                    }

                    var graduateStudentFee = (from s in context.usertypes
                                              where s.userTypeName == "Graduate Student"
                                              select s).FirstOrDefault();

                    if (graduateStudentFee != null)
                    {
                        graduateStudentFee.registrationCost = newRegistration.graduateStudentFee;
                        graduateStudentFee.registrationLateFee = newRegistration.graduateStudentLateFee;
                    }

                    var highSchoolStudentFee = (from s in context.usertypes
                                                where s.userTypeName == "High School Student"
                                                select s).FirstOrDefault();

                    if (highSchoolStudentFee != null)
                    {
                        highSchoolStudentFee.registrationCost = newRegistration.highSchoolStudentFee;
                        highSchoolStudentFee.registrationLateFee = newRegistration.highSchoolStudentLateFee;
                    }

                    var companionStudentFee = (from s in context.usertypes
                                               where s.userTypeName == "Companion"
                                               select s).FirstOrDefault();

                    if (companionStudentFee != null)
                    {
                        companionStudentFee.registrationCost = newRegistration.companionStudentFee;
                        companionStudentFee.registrationLateFee = newRegistration.companionStudentLateFee;
                    }

                    var professionalIndustryFee = (from s in context.usertypes
                                                   where s.userTypeName == "Professional Industry"
                                                   select s).FirstOrDefault();

                    if (professionalIndustryFee != null)
                    {
                        professionalIndustryFee.registrationCost = newRegistration.professionalIndustryFee;
                        professionalIndustryFee.registrationLateFee = newRegistration.professionalIndustryLateFee;
                    }

                    var professionalAcademyFee = (from s in context.usertypes
                                                  where s.userTypeName == "Professional Academia"
                                                  select s).FirstOrDefault();

                    if (professionalAcademyFee != null)
                    {
                        professionalAcademyFee.registrationCost = newRegistration.professionalAcademyFee;
                        professionalAcademyFee.registrationLateFee = newRegistration.professionalIndustryLateFee;
                    }

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("WebManger.saveRegistrationInfo error " + ex);
                return false;
            }
        }

        public DeadlinesQuery getDeadlines()
        {
            try
            {
                DeadlinesQuery deadline = new DeadlinesQuery();
                deadline.deadline1 = this.getInterfaceElement("deadline1").content;
                deadline.deadlineDate1 = this.getInterfaceElement("deadlineDate1").content;
                deadline.deadline2 = this.getInterfaceElement("deadline2").content;
                deadline.deadlineDate2 = this.getInterfaceElement("deadlineDate2").content;
                deadline.deadline3 = this.getInterfaceElement("deadline3").content;
                deadline.deadlineDate3 = this.getInterfaceElement("deadlineDate3").content;
                deadline.deadline4 = this.getInterfaceElement("deadline4").content;
                deadline.deadlineDate4 = this.getInterfaceElement("deadlineDate4").content;
                deadline.deadline5 = this.getInterfaceElement("deadline5").content;
                deadline.deadlineDate5 = this.getInterfaceElement("deadlineDate5").content;

                return deadline;
            }
            catch (Exception ex)
            {
                Console.Write("WebManager.getDeadline error " + ex);
                return null;
            }
        }

        public bool saveDeadlines(DeadlinesQuery newDeadline)
        {
            try
            {
                if (newDeadline.deadlineDate1 == "Invalid Date")
                {
                    newDeadline.deadlineDate1 = "";
                }

                if (newDeadline.deadlineDate2 == "Invalid Date")
                {
                    newDeadline.deadlineDate2 = "";
                }

                if (newDeadline.deadlineDate3 == "Invalid Date")
                {
                    newDeadline.deadlineDate3 = "";
                }

                if (newDeadline.deadlineDate4 == "Invalid Date")
                {
                    newDeadline.deadlineDate4 = "";
                }

                if (newDeadline.deadlineDate5 == "Invalid Date")
                {
                    newDeadline.deadlineDate5 = "";
                }

                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var deadline1 = (from s in context.interfaceinformations
                                     where s.attribute == "deadline1"
                                     select s).FirstOrDefault();
                    if (deadline1 != null)
                        deadline1.content = newDeadline.deadline1;

                    var deadline2 = (from s in context.interfaceinformations
                                     where s.attribute == "deadline2"
                                     select s).FirstOrDefault();
                    if (deadline2 != null)
                        deadline2.content = newDeadline.deadline2;

                    var deadline3 = (from s in context.interfaceinformations
                                     where s.attribute == "deadline3"
                                     select s).FirstOrDefault();

                    if (deadline3 != null)
                        deadline3.content = newDeadline.deadline3;

                    var deadline4 = (from s in context.interfaceinformations
                                     where s.attribute == "deadline4"
                                     select s).FirstOrDefault();

                    if (deadline4 != null)
                        deadline4.content = newDeadline.deadline4;

                    var deadline5 = (from s in context.interfaceinformations
                                     where s.attribute == "deadline5"
                                     select s).FirstOrDefault();

                    if (deadline5 != null)
                        deadline5.content = newDeadline.deadline5;

                    var deadlineDate1 = (from s in context.interfaceinformations
                                         where s.attribute == "deadlineDate1"
                                         select s).FirstOrDefault();

                    if (deadlineDate1 != null)
                        deadlineDate1.content = newDeadline.deadlineDate1;

                    var deadlineDate2 = (from s in context.interfaceinformations
                                         where s.attribute == "deadlineDate2"
                                         select s).FirstOrDefault();

                    if (deadlineDate2 != null)
                        deadlineDate2.content = newDeadline.deadlineDate2;

                    var deadlineDate3 = (from s in context.interfaceinformations
                                         where s.attribute == "deadlineDate3"
                                         select s).FirstOrDefault();

                    if (deadlineDate3 != null)
                        deadlineDate3.content = newDeadline.deadlineDate3;

                    var deadlineDate4 = (from s in context.interfaceinformations
                                         where s.attribute == "deadlineDate4"
                                         select s).FirstOrDefault();

                    if (deadlineDate4 != null)
                        deadlineDate4.content = newDeadline.deadlineDate4;

                    var deadlineDate5 = (from s in context.interfaceinformations
                                         where s.attribute == "deadlineDate5"
                                         select s).FirstOrDefault();

                    if (deadlineDate5 != null)
                        deadlineDate5.content = newDeadline.deadlineDate5;

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("WebManger.saveDeadlines error " + ex);
                return false;
            }
        }

        public List<PlanningCommitteeQuery> getPlanningCommittee()
        {
            try
            {

                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var committee = context.committeeinterfaces.Select(s => new PlanningCommitteeQuery
                    {
                        firstName = s.firstName,
                        lastName = s.lastName,
                        affiliation = s.affiliation,
                        description = s.description,
                        committeeID = s.committeID

                    }).ToList();

                    return committee;
                }
            }
            catch (Exception ex)
            {
                Console.Write("WebManager.getPlanningCommitteeList error " + ex);
                return null;
            }
        }

        public PlanningCommitteeQuery addCommittee(PlanningCommitteeQuery committee)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    committeeinterface s = new committeeinterface();
                    s.firstName = committee.firstName;
                    s.lastName = committee.lastName;
                    s.affiliation = committee.affiliation;
                    s.description = committee.description;
                    context.committeeinterfaces.Add(s);
                    context.SaveChanges();
                    committee.committeeID = s.committeID;
                    return committee;
                }
            }
            catch (Exception ex)
            {
                Console.Write("WebManager.addCommittee error " + ex);
                return committee;
            }
        }

        public bool editCommittee(PlanningCommitteeQuery committee)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var edit = (from s in context.committeeinterfaces
                                where s.committeID == committee.committeeID
                                select s).FirstOrDefault();

                    if (edit != null)
                    {
                        edit.description = committee.description;
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("WebManager.editCommittee error " + ex);
                return false;
            }
        }

        public bool deleteCommittee(PlanningCommitteeQuery committee)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var delete = (from s in context.committeeinterfaces
                                  where s.committeID == committee.committeeID
                                  select s).FirstOrDefault();

                    if (delete != null)
                    {
                        context.committeeinterfaces.Remove(delete);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("WebManager.deleteCommittee error " + ex);
                return false;
            }
        }

        public CommitteeInterfaceQuery getCommitteeInterface()
        {
            try
            {
                var committee = new CommitteeInterfaceQuery();

                using (conferenceadminContext context = new conferenceadminContext())
                {
                    committee.conferenceChairList = context.committeeinterfaces.Where(s => s.description == "Conference Chair").Select(s => new PlanningCommitteeQuery
                    {
                        firstName = s.firstName,
                        lastName = s.lastName,
                        affiliation = s.affiliation,
                        description = s.description,
                        committeeID = s.committeID

                    }).ToList();

                    committee.conferenceCoChairList = context.committeeinterfaces.Where(s => s.description == "Conference Co-Chair").Select(s => new PlanningCommitteeQuery
                    {
                        firstName = s.firstName,
                        lastName = s.lastName,
                        affiliation = s.affiliation,
                        description = s.description,
                        committeeID = s.committeID

                    }).ToList();

                    committee.conferenceCoordinatorList = context.committeeinterfaces.Where(s => s.description == "Conference Coordinator").Select(s => new PlanningCommitteeQuery
                    {
                        firstName = s.firstName,
                        lastName = s.lastName,
                        affiliation = s.affiliation,
                        description = s.description,
                        committeeID = s.committeID

                    }).ToList();

                    committee.conferenceTreasurerList = context.committeeinterfaces.Where(s => s.description == "Treasurer").Select(s => new PlanningCommitteeQuery
                    {
                        firstName = s.firstName,
                        lastName = s.lastName,
                        affiliation = s.affiliation,
                        description = s.description,
                        committeeID = s.committeID

                    }).ToList();

                    committee.conferenceAssistantList = context.committeeinterfaces.Where(s => s.description == "Conference Assistant").Select(s => new PlanningCommitteeQuery
                    {
                        firstName = s.firstName,
                        lastName = s.lastName,
                        affiliation = s.affiliation,
                        description = s.description,
                        committeeID = s.committeID

                    }).ToList();

                    committee.conferenceAccountantList = context.committeeinterfaces.Where(s => s.description == "Conference Accountant").Select(s => new PlanningCommitteeQuery
                    {
                        firstName = s.firstName,
                        lastName = s.lastName,
                        affiliation = s.affiliation,
                        description = s.description,
                        committeeID = s.committeID

                    }).ToList();

                }

                return committee;
            }
            catch (Exception ex)
            {
                Console.Write("WebManager.getCommitteeInterface error " + ex);
                return null;
            }
        }

        public SponsorInterfaceBenefits getAdminSponsorBenefits(String name)
        {
            try
            {
                var sponsors = new SponsorInterfaceBenefits();

                using (conferenceadminContext context = new conferenceadminContext())
                {
                    if (name == "Platinum")
                    {
                        var platinum = (from s in context.sponsortypes
                                        where s.name == "Platinium"
                                        select s).FirstOrDefault();
                        if (platinum != null)
                        {
                            sponsors.platinumAmount = platinum.amount;
                            SponsorBenefitsQuery benefits = new SponsorBenefitsQuery();
                            benefits.benefit1 = platinum.benefit1;
                            benefits.benefit2 = platinum.benefit2;
                            benefits.benefit3 = platinum.benefit3;
                            benefits.benefit4 = platinum.benefit4;
                            benefits.benefit5 = platinum.benefit5;
                            benefits.benefit6 = platinum.benefit6;
                            benefits.benefit7 = platinum.benefit7;
                            benefits.benefit8 = platinum.benefit8;
                            benefits.benefit9 = platinum.benefit9;
                            benefits.benefit10 = platinum.benefit10;
                            sponsors.platinumBenefits = benefits;

                        }
                    }

                    else if (name == "Gold")
                    {
                        var gold = (from s in context.sponsortypes
                                    where s.name == "Gold"
                                    select s).FirstOrDefault();
                        if (gold != null)
                        {
                            sponsors.goldAmount = gold.amount;
                            SponsorBenefitsQuery benefits = new SponsorBenefitsQuery();
                            benefits.benefit1 = gold.benefit1;
                            benefits.benefit2 = gold.benefit2;
                            benefits.benefit3 = gold.benefit3;
                            benefits.benefit4 = gold.benefit4;
                            benefits.benefit5 = gold.benefit5;
                            benefits.benefit6 = gold.benefit6;
                            benefits.benefit7 = gold.benefit7;
                            benefits.benefit8 = gold.benefit8;
                            benefits.benefit9 = gold.benefit9;
                            benefits.benefit10 = gold.benefit10;
                            sponsors.goldBenefits = benefits;

                        }
                    }

                    else if (name == "Silver")
                    {
                        var silver = (from s in context.sponsortypes
                                      where s.name == "Silver"
                                      select s).FirstOrDefault();
                        if (silver != null)
                        {
                            sponsors.silverAmount = silver.amount;
                            SponsorBenefitsQuery benefits = new SponsorBenefitsQuery();
                            benefits.benefit1 = silver.benefit1;
                            benefits.benefit2 = silver.benefit2;
                            benefits.benefit3 = silver.benefit3;
                            benefits.benefit4 = silver.benefit4;
                            benefits.benefit5 = silver.benefit5;
                            benefits.benefit6 = silver.benefit6;
                            benefits.benefit7 = silver.benefit7;
                            benefits.benefit8 = silver.benefit8;
                            benefits.benefit9 = silver.benefit9;
                            benefits.benefit10 = silver.benefit10;
                            sponsors.silverBenefits = benefits;
                        }
                    }

                    else if (name == "Bronze")
                    {
                        var bronze = (from s in context.sponsortypes
                                      where s.name == "Bronze"
                                      select s).FirstOrDefault();
                        if (bronze != null)
                        {
                            sponsors.bronzeAmount = bronze.amount;
                            SponsorBenefitsQuery benefits = new SponsorBenefitsQuery();
                            benefits.benefit1 = bronze.benefit1;
                            benefits.benefit2 = bronze.benefit2;
                            benefits.benefit3 = bronze.benefit3;
                            benefits.benefit4 = bronze.benefit4;
                            benefits.benefit5 = bronze.benefit5;
                            benefits.benefit6 = bronze.benefit6;
                            benefits.benefit7 = bronze.benefit7;
                            benefits.benefit8 = bronze.benefit8;
                            benefits.benefit9 = bronze.benefit9;
                            benefits.benefit10 = bronze.benefit10;
                            sponsors.bronzeBenefits = benefits;
                        }
                    }
                }

                return sponsors;
            }
            catch (Exception ex)
            {
                Console.Write("WebManager.getAdminSponsorBenefits error " + ex);
                return null;
            }
        }

        public bool saveSponsorBenefits(SaveSponsorQuery sponsorBenefits)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var sponsor = new sponsortype();

                    if (sponsorBenefits.name == "Platinum")
                    {
                        sponsor = (from s in context.sponsortypes
                                   where s.name == "Platinium"
                                   select s).FirstOrDefault();
                    }
                    else if (sponsorBenefits.name == "Gold")
                    {
                        sponsor = (from s in context.sponsortypes
                                   where s.name == "Gold"
                                   select s).FirstOrDefault();
                    }
                    else if (sponsorBenefits.name == "Silver")
                    {
                        sponsor = (from s in context.sponsortypes
                                   where s.name == "Silver"
                                   select s).FirstOrDefault();
                    }

                    else if (sponsorBenefits.name == "Bronze")
                    {
                        sponsor = (from s in context.sponsortypes
                                   where s.name == "Bronze"
                                   select s).FirstOrDefault();
                    }

                    if (sponsor != null)
                    {
                        sponsor.amount = sponsorBenefits.amount;
                        sponsor.benefit1 = sponsorBenefits.benefits.benefit1;
                        sponsor.benefit2 = sponsorBenefits.benefits.benefit2;
                        sponsor.benefit3 = sponsorBenefits.benefits.benefit3;
                        sponsor.benefit4 = sponsorBenefits.benefits.benefit4;
                        sponsor.benefit5 = sponsorBenefits.benefits.benefit5;
                        sponsor.benefit6 = sponsorBenefits.benefits.benefit6;
                        sponsor.benefit7 = sponsorBenefits.benefits.benefit7;
                        sponsor.benefit8 = sponsorBenefits.benefits.benefit8;
                        sponsor.benefit9 = sponsorBenefits.benefits.benefit9;
                        sponsor.benefit10 = sponsorBenefits.benefits.benefit10;
                        context.SaveChanges();
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Write("WebManger.saveSponsorBenefits error " + ex);
                return false;
            }
        }

        public bool saveInstructions(String instructions)
        {

            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var i = (from s in context.interfaceinformations
                             where s.attribute == "sponsorInstructions"
                             select s).FirstOrDefault();

                    if (i != null)
                        i.content = instructions;

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("WebManger.saveInstructions error " + ex);
                return false;
            }
        }

        public String getInstructions()
        {
            try
            {
                var instructions = this.getInterfaceElement("sponsorInstructions").content;
                return instructions;
            }
            catch (Exception ex)
            {
                Console.Write("WebManager.getInstructions error " + ex);
                return null;
            }
        }

        public SponsorInterfaceBenefits getAllSponsorBenefits()
        {
            try
            {
                var sponsors = new SponsorInterfaceBenefits();

                using (conferenceadminContext context = new conferenceadminContext())
                {

                    var platinum = (from s in context.sponsortypes
                                    where s.name == "Platinium"
                                    select s).FirstOrDefault();
                    if (platinum != null)
                    {
                        sponsors.platinumAmount = platinum.amount;
                        SponsorBenefitsQuery benefits = new SponsorBenefitsQuery();
                        benefits.benefit1 = platinum.benefit1;
                        benefits.benefit2 = platinum.benefit2;
                        benefits.benefit3 = platinum.benefit3;
                        benefits.benefit4 = platinum.benefit4;
                        benefits.benefit5 = platinum.benefit5;
                        benefits.benefit6 = platinum.benefit6;
                        benefits.benefit7 = platinum.benefit7;
                        benefits.benefit8 = platinum.benefit8;
                        benefits.benefit9 = platinum.benefit9;
                        benefits.benefit10 = platinum.benefit10;
                        sponsors.platinumBenefits = benefits;

                    }

                    var gold = (from s in context.sponsortypes
                                where s.name == "Gold"
                                select s).FirstOrDefault();
                    if (gold != null)
                    {
                        sponsors.goldAmount = gold.amount;
                        SponsorBenefitsQuery benefits = new SponsorBenefitsQuery();
                        benefits.benefit1 = gold.benefit1;
                        benefits.benefit2 = gold.benefit2;
                        benefits.benefit3 = gold.benefit3;
                        benefits.benefit4 = gold.benefit4;
                        benefits.benefit5 = gold.benefit5;
                        benefits.benefit6 = gold.benefit6;
                        benefits.benefit7 = gold.benefit7;
                        benefits.benefit8 = gold.benefit8;
                        benefits.benefit9 = gold.benefit9;
                        benefits.benefit10 = gold.benefit10;
                        sponsors.goldBenefits = benefits;

                    }

                    var silver = (from s in context.sponsortypes
                                  where s.name == "Silver"
                                  select s).FirstOrDefault();
                    if (silver != null)
                    {
                        sponsors.silverAmount = silver.amount;
                        SponsorBenefitsQuery benefits = new SponsorBenefitsQuery();
                        benefits.benefit1 = silver.benefit1;
                        benefits.benefit2 = silver.benefit2;
                        benefits.benefit3 = silver.benefit3;
                        benefits.benefit4 = silver.benefit4;
                        benefits.benefit5 = silver.benefit5;
                        benefits.benefit6 = silver.benefit6;
                        benefits.benefit7 = silver.benefit7;
                        benefits.benefit8 = silver.benefit8;
                        benefits.benefit9 = silver.benefit9;
                        benefits.benefit10 = silver.benefit10;
                        sponsors.silverBenefits = benefits;
                    }

                    var bronze = (from s in context.sponsortypes
                                  where s.name == "Bronze"
                                  select s).FirstOrDefault();
                    if (bronze != null)
                    {
                        sponsors.bronzeAmount = bronze.amount;
                        SponsorBenefitsQuery benefits = new SponsorBenefitsQuery();
                        benefits.benefit1 = bronze.benefit1;
                        benefits.benefit2 = bronze.benefit2;
                        benefits.benefit3 = bronze.benefit3;
                        benefits.benefit4 = bronze.benefit4;
                        benefits.benefit5 = bronze.benefit5;
                        benefits.benefit6 = bronze.benefit6;
                        benefits.benefit7 = bronze.benefit7;
                        benefits.benefit8 = bronze.benefit8;
                        benefits.benefit9 = bronze.benefit9;
                        benefits.benefit10 = bronze.benefit10;
                        sponsors.bronzeBenefits = benefits;
                    }
                }

                return sponsors;
            }
            catch (Exception ex)
            {
                Console.Write("WebManager.getAllSponsorBenefits error " + ex);
                return null;
            }
        }

        public GeneralInfoQuery getGeneralInfo()
        {
            try
            {
                GeneralInfoQuery info = new GeneralInfoQuery();
                info.conferenceName = this.getInterfaceElement("conferenceName").content;
                info.dateFrom = this.getInterfaceElement("conferenceDay1").content;
                info.dateTo = this.getInterfaceElement("conferenceDay3").content;

                if (info.dateTo == "")
                {
                    info.dateTo = this.getInterfaceElement("conferenceDay2").content;
                }

                using (conferenceadminContext context = new conferenceadminContext())
                {

                    var logo = (from s in context.interfacedocuments
                                where s.attibuteName == "logo"
                                select s).FirstOrDefault();

                    if (logo != null)
                    {
                        info.logo = logo.content;
                    }
                }

                return info;
            }
            catch (Exception ex)
            {
                Console.Write("WebManager.getGeneralInfo error " + ex);
                return null;
            }
        }

        public bool saveGeneralInfo(GeneralInfoQuery info)
        {
            try
            {
                String conferenceDay1 = "";
                String conferenceDay2 = "";
                String conferenceDay3 = "";

                if (info.dateFrom == "" && info.dateTo != "")
                {
                    return false;
                }

                else if (info.dateFrom != "" && info.dateTo == "")
                {
                    conferenceDay1 = info.dateFrom;
                }

                else if (info.dateFrom != "" && info.dateTo != "")
                {
                    //check distance between dates
                    DateTime dateFrom = Convert.ToDateTime(info.dateFrom);
                    DateTime dateTo = Convert.ToDateTime(info.dateTo);

                    TimeSpan ts = dateTo - dateFrom;
                    int differenceDays = ts.Days;

                    if (differenceDays < 0 || differenceDays >= 3)
                    {
                        return false;
                    }

                    else if (differenceDays == 0)
                    {
                        conferenceDay1 = info.dateFrom;
                    }

                    else if (differenceDays == 1)
                    {
                        conferenceDay1 = info.dateFrom;
                        conferenceDay2 = info.dateTo;
                    }
                    else if (differenceDays == 2)
                    {
                        conferenceDay1 = info.dateFrom;
                        conferenceDay2 = dateFrom.AddDays(1).ToShortDateString();
                        conferenceDay3 = dateFrom.AddDays(2).ToShortDateString();

                    }

                }

                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var conferenceName = (from s in context.interfaceinformations
                                          where s.attribute == "conferenceName"
                                          select s).FirstOrDefault();
                    if (conferenceName != null)
                        conferenceName.content = info.conferenceName;

                    var d1 = (from s in context.interfaceinformations
                              where s.attribute == "conferenceDay1"
                              select s).FirstOrDefault();

                    if (d1 != null)
                        d1.content = conferenceDay1;

                    var d2 = (from s in context.interfaceinformations
                              where s.attribute == "conferenceDay2"
                              select s).FirstOrDefault();

                    if (d2 != null)
                        d2.content = conferenceDay2;

                    var d3 = (from s in context.interfaceinformations
                              where s.attribute == "conferenceDay3"
                              select s).FirstOrDefault();

                    if (d3 != null)
                        d3.content = conferenceDay3;

                    var logo = (from s in context.interfacedocuments
                              where s.attibuteName == "logo"
                              select s).FirstOrDefault();

                    if (logo != null)
                        logo.content = info.logo; 

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("WebManger.saveGeneralInfo error " + ex);
                return false;
            }
        }
    }

    public class ContentQuery
    {
        public int interfaceID;
        public String content;

        public ContentQuery()
        {

        }
    }

    public class HomeQuery
    {
        public String conferenceName;
        public String homeMainTitle;
        public String homeTitle1;
        public String homeParagraph1;
        public String homeTitle2;
        public String homeParagraph2;
        public String image;

        public HomeQuery()
        {

        }
    }

    public class GeneralInfoQuery
    {
        public String conferenceName;
        public String dateFrom;
        public String dateTo;
        public String logo;
        public GeneralInfoQuery()
        {

        }
    }

    public class VenueQuery
    {
        public String venueTitle1;
        public String venueParagraph1;
        public String venueTitle2;
        public String venueParagraph2;
        public String venueTitleBox;
        public String venueParagraphContentBox;

        public VenueQuery()
        {

        }
    }

    public class ContactQuery
    {
        public String contactName;
        public String contactPhone;
        public String contactEmail;
        public String contactAdditionalInfo;

        public ContactQuery()
        {

        }
    }

    public class ParticipationQuery
    {
        public String participationTitle1;
        public String participationParagraph1;
        public String participationTitle2;
        public String participationParagraph2;
        public String participationTitle3;
        public String participationParagraph3;
        public String participationTitle4;
        public String participationParagraph4;
        public String participationTitle5;
        public String participationParagraph5;

        public ParticipationQuery()
        {

        }
    }

    public class RegistrationQuery
    {
        public String registrationTitle1;
        public String registrationParagraph1;
        public String registrationTitle2;
        public String registrationParagraph2;
        public String registrationNotes;
        public double undergraduateStudentFee;
        public double graduateStudentFee;
        public double highSchoolStudentFee;
        public double companionStudentFee;
        public double professionalAcademyFee;
        public double professionalIndustryFee;
        public double undergraduateStudentLateFee;
        public double graduateStudentLateFee;
        public double highSchoolStudentLateFee;
        public double companionStudentLateFee;
        public double professionalAcademyLateFee;
        public double professionalIndustryLateFee;

        public RegistrationQuery()
        {

        }
    }

    public class DeadlinesQuery
    {
        public String deadline1;
        public String deadlineDate1;
        public String deadline2;
        public String deadlineDate2;
        public String deadline3;
        public String deadlineDate3;
        public String deadline4;
        public String deadlineDate4;
        public String deadline5;
        public String deadlineDate5;
    }

    public class PlanningCommitteeQuery
    {
        public String firstName;
        public String lastName;
        public String affiliation;
        public String description;
        public int committeeID;

        public PlanningCommitteeQuery()
        {

        }
    }

    public class CommitteeInterfaceQuery
    {
        public List<PlanningCommitteeQuery> conferenceChairList = new List<PlanningCommitteeQuery>();
        public List<PlanningCommitteeQuery> conferenceCoChairList = new List<PlanningCommitteeQuery>();
        public List<PlanningCommitteeQuery> conferenceCoordinatorList = new List<PlanningCommitteeQuery>();
        public List<PlanningCommitteeQuery> conferenceTreasurerList = new List<PlanningCommitteeQuery>();
        public List<PlanningCommitteeQuery> conferenceAssistantList = new List<PlanningCommitteeQuery>();
        public List<PlanningCommitteeQuery> conferenceAccountantList = new List<PlanningCommitteeQuery>();

        public CommitteeInterfaceQuery()
        {

        }


    }

    public class SponsorBenefitsQuery
    {
        public String benefit1;
        public String benefit2;
        public String benefit3;
        public String benefit4;
        public String benefit5;
        public String benefit6;
        public String benefit7;
        public String benefit8;
        public String benefit9;
        public String benefit10;

        public SponsorBenefitsQuery()
        {

        }
    }

    public class SponsorInterfaceBenefits
    {
        public double platinumAmount;
        public double goldAmount;
        public double silverAmount;
        public double bronzeAmount;
        public SponsorBenefitsQuery platinumBenefits;
        public SponsorBenefitsQuery goldBenefits;
        public SponsorBenefitsQuery silverBenefits;
        public SponsorBenefitsQuery bronzeBenefits;

        public SponsorInterfaceBenefits()
        {

        }
    }

    public class SaveSponsorQuery
    {
        public String name;
        public double amount;
        public SponsorBenefitsQuery benefits;

        public SaveSponsorQuery()
        {

        }
    }
}