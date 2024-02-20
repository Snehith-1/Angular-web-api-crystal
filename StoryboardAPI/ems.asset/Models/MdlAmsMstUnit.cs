using ems.asset.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.asset.Models
{
    public class MdlAmsMstUnit
    {
        public List<unit_list> unitlist { get; set; }
        public List<breadcrumb_list1> breadcrumb_list { get; set; }
    }
    public class unit_list : result
    {
        public string locationunit_gid { get; set; }
        public string locationunit_name { get; set; }
        public string locationunit_code { get; set; }
        public string locationunit_address { get; set; }
        public string unit_prefix { get; set; }
        public string unitedit_prefix { get; set; }
        public string locationunitedit_name { get; set; }
        public string locationunitedit_address { get; set; }

    }
    public class unitllist
    {
        public List<unitdtl> unitdtl { get; set; }
    }

    public class unitdtl : result
    {
        public string locationunit_gid { get; set; }
        public string locationunit_code { get; set; }
        public string unit_prefix { get; set; }
        public string locationunit_name { get; set; }
        public string locationunit_address { get; set; }
    }

    public class result
    {
        public string message { get; set; }
        public bool status { get; set; }
    }
    public class breadcrumb_list1 : result
    {

        public string module_name1 { get; set; }
        public string sref1 { get; set; }
        public string module_name2 { get; set; }
        public string sref2 { get; set; }
        public string module_name3 { get; set; }
        public string sref3 { get; set; }


    }
}
