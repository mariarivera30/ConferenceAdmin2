using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyService.Modules
{
    public class AdminManager
    {   
        
       
        
        public AdminManager(){
            
        }
        
        public bool addSponsor (sponsor s)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    context.sponsors.Add(s);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.addSponsor error " + ex);
                return false;
            }           
               
         }    
        
        public List<string> getSponsorList () 
        {
            try{
             
                 using(conferenceadminContext context = new conferenceadminContext())
                    {
                        var guests = from g in context.users
                                     join ad in context.minors on g.userID equals ad.userID
                                   //  join mem in context.memberships on g.membershipID equals mem.membershipID
                                     where g.hasApplied == true
                                     select g.firstName ;

                        return guests.ToList();
                        /*var list = from s in context.sponsors
                                   select s.firstName;
                        return list.ToList();*/

                     }
                
              
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.getSponsor error " + ex);
                return null;
            }           
               
         }      

         
            
                

           
       
        }
    }

