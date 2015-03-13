using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class paymentbill
    {
        public long paymentBillID { get; set; }
        public long paymentID { get; set; }
        public string transactionid { get; set; }
        public int AmountPaid { get; set; }
        public string methodOfPayment { get; set; }
        public string creditCardNumber { get; set; }
        public virtual payment payment { get; set; }
    }
}
