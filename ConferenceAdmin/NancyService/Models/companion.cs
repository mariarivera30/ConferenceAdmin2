using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class companion
    {
        public companion()
        {
            this.companionminors = new List<companionminor>();
        }

        public long companionID { get; set; }
        public long userID { get; set; }
        public string companionKey { get; set; }
        public Nullable<bool> deleted { get; set; }
        public virtual ICollection<companionminor> companionminors { get; set; }
        public virtual user user { get; set; }
    }
}
