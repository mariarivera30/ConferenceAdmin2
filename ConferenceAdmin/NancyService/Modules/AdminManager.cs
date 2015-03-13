using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyService.Modules
{
    public class AdminManager
    {   
        
        private conferenceadminContext context;
        
        public AdminManager(){
            context = new conferenceadminContext();
        }
        
        public bool addSponsor (sponsor s)
        {
            try
            {
                context.sponsors.Add(s);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.addSponsor error " + ex);
                return false;
            }           
               
         }    
        
        public List<sponsor> getSponsorList () 
        {
            try
            {
                return context.sponsors.ToList();
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.getSponsor error " + ex);
                return null;
            }           
               
         }      

         
            
                

           
       
        }
    }

