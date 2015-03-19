using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class payment
    {
        public payment()
        {
            this.sponsors = new List<sponsor>();
            this.paymentcomplementaries = new List<paymentcomplementary>();
            this.paymentbills = new List<paymentbill>();
            this.registrations = new List<registration>();
        }

        public long paymentID { get; set; }
        public int paymentTypeID { get; set; }
        public Nullable<System.DateTime> creationDate { get; set; }
        public Nullable<bool> deleted { get; set; }
        public virtual ICollection<sponsor> sponsors { get; set; }
        public virtual ICollection<paymentcomplementary> paymentcomplementaries { get; set; }
        public virtual ICollection<paymentbill> paymentbills { get; set; }
        public virtual ICollection<registration> registrations { get; set; }
        public virtual paymenttype paymenttype { get; set; }
    }
}
