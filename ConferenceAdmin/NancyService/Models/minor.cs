using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class minor
    {
        public minor()
        {
            this.authorizationsubmitteds = new List<authorizationsubmitted>();
            this.companionminors = new List<companionminor>();
        }

        public long minorsID { get; set; }
        public long userID { get; set; }
        public Nullable<bool> authorizationStatus { get; set; }
        public Nullable<bool> deleted { get; set; }
        public virtual ICollection<authorizationsubmitted> authorizationsubmitteds { get; set; }
        public virtual ICollection<companionminor> companionminors { get; set; }
        public virtual user user { get; set; }
    }
}
