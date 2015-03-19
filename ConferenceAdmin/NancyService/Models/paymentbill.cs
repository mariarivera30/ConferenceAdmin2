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
        public string creditCardNumber { get; set; }
        public Nullable<bool> deleted { get; set; }
        public virtual address address { get; set; }
        public virtual payment payment { get; set; }
    }
}
