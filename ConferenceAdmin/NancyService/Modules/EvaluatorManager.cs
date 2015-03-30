using NancyService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyService.Modules
{
    public class EvaluatorManager
    {
        public EvaluatorManager()
        {

        }

        public bool checkNewEvaluator(String email)
        {
            try
            {

                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var result = (from s in context.users
                                  where s.membership.email == email
                                  select s.userID).Count();

                    if (result > 0)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.Write("EvaluatorManager.checkNewEvaluator error " + ex);
                return false;
            }
        }

        public List<EvaluatorQuery> getEvaluatorList()
        {
            try
            {

                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var evaluators = context.users.Where(evaluator => evaluator.evaluatorStatus == "Accepted" || evaluator.evaluatorStatus == "Rejected").Select(evaluator => new EvaluatorQuery
                    {
                        userID = (long)evaluator.userID,
                        firstName = evaluator.firstName,
                        lastName = evaluator.lastName,
                        email = evaluator.membership.email,
                        acceptanceStatus = evaluator.evaluatorStatus

                    }).ToList();

                    return evaluators;
                }
            }
            catch (Exception ex)
            {
                Console.Write("EvaluatorManager.getEvaluatorList error " + ex);
                return null;
            }
        }

        public List<EvaluatorQuery> getPendingList()
        {
            try
            {

                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var evaluators = context.users.Where(evaluator => evaluator.evaluatorStatus == "Pending").Select(evaluator => new EvaluatorQuery
                    {
                        userID = (long)evaluator.userID,
                        firstName = evaluator.firstName,
                        lastName = evaluator.lastName,
                        email = evaluator.membership.email,
                        acceptanceStatus = evaluator.evaluatorStatus

                    }).ToList();

                    return evaluators;
                }
            }
            catch (Exception ex)
            {
                Console.Write("EvaluatorManager.getPendingList error " + ex);
                return null;
            }
        }

        public bool updateAcceptanceStatus(EvaluatorQuery e)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var updateUser = (from s in context.users
                                      where s.userID == e.userID
                                      select s).FirstOrDefault();

                    if (updateUser != null)
                    {
                        updateUser.evaluatorStatus = e.acceptanceStatus;

                        if (e.acceptanceStatus == "Rejected")
                        {
                            //Remove from claim table
                            var updateClaim = (from s in context.claims
                                               where s.userID == e.userID && s.privilegesID == 4
                                               select s).FirstOrDefault();

                            if (updateClaim != null)
                            {
                                updateClaim.deleted = true;
                            }

                            //Remove from evaluator table
                            var updateEvaluator = (from s in context.evaluators
                                                   where s.userID == e.userID
                                                   select s).FirstOrDefault();

                            if (updateEvaluator != null)
                            {
                                updateEvaluator.deleted = true;
                            }
                        }

                        else if (e.acceptanceStatus == "Accepted")
                        {
                            this.addEvaluator(updateUser.membership.email);
                        }

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
                Console.Write("EvaluatorManager.updateAcceptanceStatus error " + ex);
                return false;
            }
        }

        public EvaluatorQuery addEvaluator(String email)
        {
            try
            {
                using (conferenceadminContext context = new conferenceadminContext())
                {
                    var e = (from user in context.users
                             where user.membership.email == email
                             select user).FirstOrDefault();

                    if (e != null)
                    {
                        var check = (from s in context.evaluators
                                     where s.user.userID == e.userID
                                     select s).FirstOrDefault();

                        if (check != null)
                        {
                            //User is already in evaluator/claim table
                            if ((bool)check.deleted)
                            {
                                e.evaluatorStatus = "Accepted";
                                check.deleted = false;
                                var claims = (from s in context.claims
                                              where s.userID == e.userID && s.privilegesID == 4
                                              select s).FirstOrDefault();
                                if (claims != null)
                                {
                                    claims.deleted = false;
                                }
                                context.SaveChanges();
                            }

                            return null;
                        }

                        else
                        {
                            //Change status in table user
                            e.evaluatorStatus = "Accepted";

                            EvaluatorQuery newEvaluator = new EvaluatorQuery();
                            newEvaluator.userID = e.userID;
                            newEvaluator.firstName = e.firstName;
                            newEvaluator.lastName = e.lastName;
                            newEvaluator.email = email;
                            newEvaluator.acceptanceStatus = e.evaluatorStatus;

                            //Add claim 
                            claim newClaim = new claim();
                            newClaim.privilegesID = 4;
                            newClaim.deleted = false;
                            newClaim.userID = e.userID;
                            context.claims.Add(newClaim);

                            //Add evaluator
                            evaluator newEva = new evaluator();
                            newEva.userID = e.userID;
                            newEva.deleted = false;
                            context.evaluators.Add(newEva);

                            context.SaveChanges();
                            return newEvaluator;
                        }
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.Write("EvaluatorManager.addEvaluator error " + ex);
                return null;
            }
        }

    }

    public class EvaluatorQuery
    {
        public long userID;
        public String firstName;
        public String lastName;
        public String email;
        public String acceptanceStatus;
        public String optionStatus = "Accept";

        public EvaluatorQuery()
        {
        }
    }
}