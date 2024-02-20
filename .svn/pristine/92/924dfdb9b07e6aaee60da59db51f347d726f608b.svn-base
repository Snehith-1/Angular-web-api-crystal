using ems.asset.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ems.asset.Models
{
    public class MdlAmsMstProductGroup
    {
        public List<productgroup_list> productgrouplist { get; set; }
        public List<amsmstproductgroupdtl> amsmstproductgroupdtl { get; set; }
        public List<breadcrumb_list3> breadcrumb_list { get; set; }


    }

    public class productgroup_list : result
    {
        public string productgroup_gid { get; set; }
        public string productgroup_code { get; set; }
        public string productgroup_prefix { get; set; }
        public string productgroup_name { get; set; }
        public string product_type { get; set; }
        public string productgroupedit_prefix { get; set; }
        public string productgroupedit_name { get; set; }
      

    }
    public class amsmstproductgroupdtl : result
    {
        public string productgroup_gid { get; set; }
        public string productgroup_code { get; set; }
        public string productgroup_name { get; set; }
        public string productgroup_prefix { get; set; }
        public string product_type { get; set; }
        public bool status { get; set; }
        public string message { get; set; }

    }
    public class breadcrumb_list3 : result
    {

        public string module_name1 { get; set; }
        public string sref1 { get; set; }
        public string module_name2 { get; set; }
        public string sref2 { get; set; }
        public string module_name3 { get; set; }
        public string sref3 { get; set; }


    }
}