﻿using StoryboardAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.sales.Models
{
    public class MdlSmrMstTaxSummary:result
    {

        public List<smrtax_list> smrtax_list { get; set; }

    }
    
    public class smrtax_list : result

    { 
    public string tax_gid { get; set; }
    public string tax_name { get; set; }
    public string percentage { get; set; }
    public string created_by { get; set; }
    public string created_date { get; set; }
    public string taxedit_name { get; set; }
    public string editpercentage { get; set; }
}
}