using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.einvoice.Models
{
    public class MdlLandingPage:result
    {
       
            public List<LandingpageSummary> LandingpageSummary { get; set; }

    }
    public class LandingpageSummary : result
    {
        public string total_raised_invoice { get; set; }
        public string irn_generated { get; set; }
        public string irn_Pending { get; set; }
    }
}