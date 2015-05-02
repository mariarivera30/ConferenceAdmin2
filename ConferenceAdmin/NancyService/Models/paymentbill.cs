using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class paymentbill
    {
        public long paymentBillID { get; set; }
        public long paymentID { get; set; }
        public Nullable<long> addressID { get; set; }
        public string transactionid { get; set; }
        public double AmountPaid { get; set; }
        public string methodOfPayment { get; set; }
        public bool deleted { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public string ip { get; set; }
        public string tandemID { get; set; }
        public string batchID { get; set; }
        public int quantity { get; set; }
        public System.DateTime date { get; set; }
        public bool completed { get; set; }
        public virtual address address { get; set; }
        public virtual payment payment { get; set; }
    }
}
