using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.asset.Models
{
    public class MdlAmsMstProductsubgroup
    {
        public List<breadcrumb_listproductsubgroup> breadcrumb_listproductsubgroup { get; set; }
        public List<productsubgroup_list> productsubgroup_list { get; set; }
        public List<GetProductgroup> GetProductgroup { get; set; }
        public List<GetAssetblock> GetAssetblock { get; set; }
        public List<GetNatureOfAsset> GetNatureOfAsset { get; set; }
    }
    public class GetAssetblock : result
    {
        public string assetblock_gid { get; set; }
        public string assetblock_name { get; set; }

    }
    public class GetNatureOfAsset : result
    {
        public string natureofasset_gid { get; set; }
        public string description { get; set; }

    }
    public class GetProductgroup : result
    {
        public string productgroup_gid { get; set; }
        public string productgroup_name { get; set; }
        public string productgroup_name1 { get; set; }

    }
    public class productsubgroup_list : result
    {
        public string productgroup_name { get; set; }
        public string description { get; set; }
        public string assetblock_gid { get; set; }
        public string assetblock_code { get; set; }
        public string natureofasset_code { get; set; }
        public string natureofasset_gid { get; set; }
        public string productgroup { get; set; }
        public string assetblock_name { get; set; }

        public string productsubgroup { get; set; }
        public string productsubgroup_name { get; set; }
        public string productsubgroup_code { get; set; }
        public string productsubgroup_gid { get; set; }
        public string productgroup_gid { get; set; }
        public string subgroup_warranty { get; set; }
        public string subgroup_additionalwarranty { get; set; }
        public string subgroup_oem { get; set; }
        public string subgroup_amc { get; set; }


    }
    public class breadcrumb_listproductsubgroup : result
    {
        public string module_name1 { get; set; }
        public string sref1 { get; set; }
        public string module_name2 { get; set; }
        public string sref2 { get; set; }
        public string module_name3 { get; set; }
        public string sref3 { get; set; }

    }
}