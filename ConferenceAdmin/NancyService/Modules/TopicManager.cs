using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyService.Modules
{
    public class TopicManager
    {       
        public TopicManager(){
            
        }
        
        public topiccategory addTopic(topiccategory s)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    s.deleted = false;
                    context.topiccategories.Add(s);
                    context.SaveChanges();
                    return s;
                }
            }
            catch (Exception ex)
            {
                Console.Write("TopicManager.addTopic error " + ex);
                return s;
            }

        }

        public List<topiccategory> getTopicList()
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var topicList = (from s in context.topiccategories
                                     where s.deleted !=true
                                     select new { s.topiccategoryID, s.name }).ToList();

                    return topicList.Select(x => new topiccategory { topiccategoryID = x.topiccategoryID, name = x.name }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.Write("TopicManager.getTopic error " + ex);
                return null;
            }
        }

        public bool deleteTopic(int id)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var topic = (from s in context.topiccategories
                                 where s.topiccategoryID == id
                                 select s).First();
                    topic.deleted = true;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("TopicManager.deleteTopic error " + ex);
                return false;
            }
        }

        public bool updateTopic(topiccategory x)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var topic = (from s in context.topiccategories
                                 where s.topiccategoryID == x.topiccategoryID
                                 select s).First();
                    topic.name = x.name;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("TopicManager.updateTopic error " + ex);
                return false;
            }
        }  
     }
}

