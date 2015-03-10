using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class sponsor
    {
        
            public sponsor()
            {
                this.complementarykeys = new List<complementarykey>();
            }

            public long sponsorID { get; set; }
            public int sponsorType { get; set; }
            public Nullable<long> paymentID { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string title { get; set; }
            public string company { get; set; }
            public long addressID { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
            public string logo { get; set; }
            public Nullable<System.DateTime> creationDate { get; set; }
            public Nullable<System.DateTime> deleitionDate { get; set; }
            public virtual address address { get; set; }
            public virtual ICollection<complementarykey> complementarykeys { get; set; }
            public virtual payment payment { get; set; }
            public virtual sponsortype sponsortype1 { get; set; }
        }
    }

