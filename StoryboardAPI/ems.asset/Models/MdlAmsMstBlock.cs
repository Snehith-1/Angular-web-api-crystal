using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.asset.Models
{
    public class MdlAmsMstBlock
    {
        public List<block_list> block_list { get; set; }

    }
    public class breadcrumb_listblock : result
    {
        public string module_name1 { get; set; }
        public string sref1 { get; set; }
        public string module_name2 { get; set; }
        public string sref2 { get; set; }
        public string module_name3 { get; set; }
        public string sref3 { get; set; }

    }

    public class block_list : result
    {
        public string locationunit_gid { get; set; }
        public string locationunit_name { get; set; }

        public string locationblock_gid { get; set; }
        public string locationblock_name { get; set; }
        public string locationblock_code { get; set; }
        public string block_prefix { get; set; }

    }
    public class blocklist
    {
        public List<blockdtl> blockdtl { get; set; }
        public List<breadcrumb_listblock> breadcrumb_listblock { get; set; }
    }

    public class blockdtl : result
    {
        public string locationblock_gid { get; set; }
        public string locationunit_gid { get; set; }
        public string locationblock_code { get; set; }
        public string block_prefix { get; set; }
        public string locationblock_name { get; set; }
        public string locationunit_name { get; set; }

    }
    public class unit_llist
    {
        public List<unit_llist> unit_bllist { get; set; }
        public string locationunit_name { get; set; }
        public string locationunit_gid { get; set; }
        public string locationblock_gid { get; set; }

        public string locationblock_code { get; set; }
        public string block_prefix { get; set; }
        public string locationblock_name { get; set; }

        public string message { get; set; }
        public bool status { get; set; }

    }
}