using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class user
    {
        public user()
        {
            this.claims = new List<claim>();
            this.companions = new List<companion>();
            this.evaluators = new List<evaluator>();
            this.minors = new List<minor>();
            this.registrations = new List<registration>();
            this.submissions = new List<submission>();
        }

        public long userID { get; set; }
        public long membershipID { get; set; }
        public int userTypeID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string title { get; set; }
        public string affiliationName { get; set; }
        public string phone { get; set; }
        public long addressID { get; set; }
        public string userFax { get; set; }
        public string registrationStatus { get; set; }
        public Nullable<bool> hasApplied { get; set; }
        public string acceptanceStatus { get; set; }
        public string isEvaluator { get; set; }
        public Nullable<short> deleted { get; set; }
        public virtual address address { get; set; }
        public virtual ICollection<claim> claims { get; set; }
        public virtual ICollection<companion> companions { get; set; }
        public virtual ICollection<evaluator> evaluators { get; set; }
        public virtual membership membership { get; set; }
        public virtual ICollection<minor> minors { get; set; }
        public virtual ICollection<registration> registrations { get; set; }
        public virtual ICollection<submission> submissions { get; set; }
        public virtual usertype usertype { get; set; }
    }
}
