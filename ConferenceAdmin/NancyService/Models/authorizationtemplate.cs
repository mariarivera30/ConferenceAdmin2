using System;
using System.Collections.Generic;

namespace NancyService.Models
{
    public partial class authorizationtemplate
    {
        public int authorizationID { get; set; }
        public string authorizationName { get; set; }
        public string authorizationDocument { get; set; }
        public Nullable<bool> deleted { get; set; }
    }
}
