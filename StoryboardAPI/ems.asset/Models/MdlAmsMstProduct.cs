using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.asset.Models
{
    public class MdlAmsMstProduct
    {
        public List<product_list> product_list { get; set; }
        public List<GetProductsubgroup> GetProductsubgroup { get; set; }
        public List<GetAttribute> GetAttribute { get; set; }

    }
    public class GetProductsubgroup : result
    {
        public string productsubgroup_gid { get; set; }
        public string productsubgroup_name { get; set; }
        public string productsubgroup_name1 { get; set; }

    }
    public class GetAttribute : result
    {
        public string attribute_gid { get; set; }
        public string attribute_name { get; set; }
        public string attribute_code { get; set; }

    }

    public class product_list : result
    {
        public string productgroup_name { get; set; }
        public string productgroup { get; set; }

        public string product_gid { get; set; }
        public string product_code { get; set; }
      
        public string productgroup_gid { get; set; }
        public string productsubgroup_gid { get; set; }
        public string productsubgroup { get; set; }
        public string attribute_name { get; set; }
        public string productsubgroup_name { get; set; }
        public string product_name { get; set; }
        public string asset_images { get; set; }
        public string serial_flag { get; set; }





    }
}