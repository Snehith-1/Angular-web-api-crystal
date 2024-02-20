using StoryboardAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.sales.Models
{



    public class MdlSmrTrnDeliveryorderSummary :result
    {
        public List<adddeliveryorder_list> adddeliveryorder_list { get; set; }
        public List<deliveryorder_list> deliveryorder_list { get; set; }

    }
    public class deliveryorder_list : result
    {
        public string directorder_date { get; set; }
        public string directorder_refno { get; set; }
        public string so_referenceno1 { get; set; }
        public string customer_name { get; set; }
        public string contact { get; set; }
        public string branch_name { get; set; }
        public string salesorder_status { get; set; }


    }

    public class adddeliveryorder_list : result
    {
        public string salesorder_date { get; set; }
        public string salesorder_gid { get; set; }
        public string so_referenceno1 { get; set; }
        public string customer_name { get; set; }
        public string contact { get; set; }
        public string branch_name { get; set; }
        public string salesorder_status { get; set; }


    }
}