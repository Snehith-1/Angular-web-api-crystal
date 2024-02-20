﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.inventory.Models
{
    public class MdlImsRptStockreport:result
    {
        public List<stockreport_list> stockreport_list { get; set; }
        public List<branch_list> branch_list { get; set; }
        public string branch_name { get; set; }
    }



    public class stockreport_list : result
    {
        public string bin_number { get; set; }
        public string branch_name { get; set; }
        public string location_name { get; set; }
        public string product_code { get; set; }
        public string product_name { get; set; }
        public string productuom_name { get; set; } 
        public string stock_balance { get; set; }
        public string stock_value { get; set; }
        public string product_price { get; set; }
        public string display_field { get; set; }
    
        public string branch_gid { get; set; }
   




    } 
    public class branch_list : result
    {
 
        public string branch_gid { get; set; }
        public string branch_name { get; set; }




    }
}