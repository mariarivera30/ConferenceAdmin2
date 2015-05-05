using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyService.Modules
{
    public class TopicManager
    {
        //By: Heidi Negron
        public TopicManager()
        {

        }


        //Get the list of conference Topics
        public List<topiccategory> getTopicList()
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //Select topics
                    var topicList = (from s in context.topiccategories
                                     where s.deleted != true
                                     select new { s.topiccategoryID, s.name }).ToList();

                    return topicList.Select(x => new topiccategory { topiccategoryID = x.topiccategoryID, name = x.name }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.getTopic error " + ex);
                return null;
            }
        }


        //Add a new topic: returns the new topicID
        public topiccategory addTopic(topiccategory s)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var checkTopic = (from t in context.topiccategories
                                      where t.name == s.name && t.deleted == true
                                      select t).FirstOrDefault();
                    //check if there has been topics with the specified name
                    if (checkTopic != null)
                    {
                        //Recover topic and set deleted attribute back to false
                        checkTopic.deleted = false;
                        s.topiccategoryID = checkTopic.topiccategoryID;
                    }
                    else
                    {
                        s.deleted = false;
                        //Add new topic
                        context.topiccategories.Add(s);
                    }

                    context.SaveChanges();
                    return s;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.addTopic error " + ex);
                return s;
            }

        }
        
        //Update topic to the specified name
        public bool updateTopic(topiccategory x)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //Get the topic to update
                    var topic = (from s in context.topiccategories
                                 where s.topiccategoryID == x.topiccategoryID
                                 select s).FirstOrDefault();

                    if (topic != null)
                    {
                        //Change name
                        topic.name = x.name;
                        context.SaveChanges();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.updateTopic error " + ex);
                return false;
            }
        }

        //Delete topic with the specified topicID
        public bool deleteTopic(int id)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    //Get topic to be removed
                    var topic = (from s in context.topiccategories
                                 where s.topiccategoryID == id
                                 select s).FirstOrDefault();

                    if (topic != null)
                    {
                        //Check the topic does not have submissions assigned
                        var submissions = (from s in context.submissions
                                           where s.topicID == id
                                           select s).Count();

                        if (submissions == 0)
                        {
                            //Topic is deleted if there are no submissions under the category.
                            topic.deleted = true;
                            context.SaveChanges();
                            return true;
                        }
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.deleteTopic error " + ex);
                return false;
            }
        }

    }

}