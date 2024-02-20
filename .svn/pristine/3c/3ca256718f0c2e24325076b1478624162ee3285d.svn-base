using StoryboardAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace ems.sales.Models
{
    public class MdlSmrMstProductGroup : result
    {
        public List<salesproductgroup_list> salesproductgroup_list { get; set; }
        public List<GetTaxDropdown> GetTaxDtl { get; set; }
        public List<GetTax2Dropdown> GetTax2Dtl { get; set; }
        public List<GetTax3Dropdown> GetTax3Dtl { get; set; }


    }

    public class salesproductgroup_list : result
    {
        public string productgroup_gid { get; set; }
        public string productgroup_code { get; set; }
        public string productgroup_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string productgroupedit_code { get; set; }
        public string productgroupedit_name { get; set; }
        public string tax1_gid { get; set; }
        public string tax2_gid { get; set; }
        public string tax3_gid { get; set; }

        public string tax1_name{ get; set; }
        public string tax2_name { get; set; }
        public string tax3_name { get; set; }



    }
    public class GetTaxDropdown : result
    {
        public string productgroup_gid { get; set; }
        public string productgroup_code { get; set; }
        public string productgroup_name { get; set; }
        public string tax1_gid { get; set; }
        public string tax1_name { get; set; }
    }

    public class GetTax2Dropdown : result
    {
        public string productgroup_gid { get; set; }
        public string productgroup_code { get; set; }
        public string productgroup_name { get; set; }
        public string tax2_gid { get; set; }
        public string tax2_name { get; set; }
    }

    public class GetTax3Dropdown : result
    {
        public string productgroup_gid { get; set; }
        public string productgroup_code { get; set; }
        public string productgroup_name { get; set; }
        public string tax3_gid { get; set; }
        public string tax3_name { get; set; }
    }


}