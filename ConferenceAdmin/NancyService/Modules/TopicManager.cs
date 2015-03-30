using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyService.Modules
{
    public class TopicManager
    {
        public TopicManager()
        {

        }

        public List<topiccategory> getTopicList()
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {

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

        public topiccategory addTopic(topiccategory s)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var checkTopic = (from t in context.topiccategories
                                      where t.name == s.name && t.deleted == true
                                      select t).FirstOrDefault();

                    if (checkTopic != null)
                    {
                        checkTopic.deleted = false;
                        s.topiccategoryID = checkTopic.topiccategoryID;
                    }
                    else
                    {
                        s.deleted = false;
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

        public bool updateTopic(topiccategory x)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var topic = (from s in context.topiccategories
                                 where s.topiccategoryID == x.topiccategoryID
                                 select s).FirstOrDefault();

                    if (topic != null)
                    {
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

        public bool deleteTopic(int id)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var topic = (from s in context.topiccategories
                                 where s.topiccategoryID == id
                                 select s).FirstOrDefault();

                    if (topic != null)
                    {
                        var submissions = (from s in context.submissions
                                           where s.topicID == id
                                           select s).Count();

                        if (submissions == 0)
                        {
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