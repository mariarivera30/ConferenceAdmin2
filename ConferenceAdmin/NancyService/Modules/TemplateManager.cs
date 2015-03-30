using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyService.Modules
{
    public class TemplateManager
    {
        public class templateQuery
        {
            public long templateID { get; set; }
            public string name { get; set; }
            public string document { get; set; }
            public string topic { get; set; }
            public string description { get; set; }
        };

        public List<templateQuery> getTemplates()
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var tempList = (from t in context.templates
                                    where t.deleted == false
                                    select new templateQuery
                                    {
                                        templateID = t.templateID,
                                        name = t.name,
                                        document = t.document,
                                        topic = t.topic,
                                        description = t.description
                                    }).ToList();

                    return tempList;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.getTemplates error " + ex);
                return null;
            }


        }

        public templateQuery addTemplate(templateQuery t)
        {
            template template = new template();
            template.templateID = t.templateID;
            template.description = t.description;
            template.topic = t.topic;
            template.document = t.document;
            template.name = t.name;
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    template.deleted = false;
                    context.templates.Add(template);
                    context.SaveChanges();
                    t.templateID = template.templateID;
                    return t;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.addTemplate error " + ex);
                return null;
            }

        }

        public bool updateTemplate(template newTemp)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var template = (from temp in context.templates
                                    where temp.templateID == newTemp.templateID
                                    select temp).FirstOrDefault();
                    template.description = newTemp.description;
                    template.name = newTemp.name;
                    template.document = newTemp.document;
                    template.topic = newTemp.topic;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.addTemplate error " + ex);
                return false;
            }

        }

        public bool deleteTemplate(long id)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {

                    //verificar que se quiere se de disable a todos los evaluation q los tiene o que simplemente no se pueda escoger desde la pantalla 
                    //donde se asignan los evaluation pero lo q ya lo tienen  se queden con las evaluaciones.

                    var template = (from temp in context.templates
                                    where temp.templateID == id
                                    select temp).FirstOrDefault();

                    template.deleted = true;

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Write("AdminManager.deleteTemplate error " + ex);
                return false;
            }

        }

    }
}