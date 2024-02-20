using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.sales.Models
{
    public class MdlSmrRptSalesOrderDetailedReport:result
    {
       public  List<salesorderdetail_list>salesorderdetail_list {get;set;}
    }
    public class salesorderdetail_list :result
    {
        public string contact { get; set; }
        public string salesorder_gid { get; set; }
        public string salesorder_date { get; set; }
        public string so_referenceno1 { get; set; }
        public string customer_name { get; set; }
        public string branch_name { get; set; }
        public string so_type { get; set; }
        public string Grandtotal { get; set; }
        public string user_firstname { get; set; }
        public string salesorder_status { get; set; }
        public string approved_date { get; set; }
        public string delivered_date { get; set; }
        public string approved_by { get; set; }
        public string customer_contact_person { get; set; }
        public string currency_code { get; set; }
        public string delivered_by { get; set; }
    }
}