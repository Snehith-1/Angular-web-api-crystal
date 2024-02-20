using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.inventory.Models
{
    public class MdlImsTrnDeliveryAcknowledgement:result
    {
        public List<deliverysummary_list> deliverysummary_list { get; set; }
        public List<deliveryadd_list> deliveryadd_list { get; set; }
        public List<deliverycus_list> deliverycus_list { get; set; }
        public List<deliverycusprod_list> deliverycusprod_list { get; set; }
        public List<postdelivery_list> postdelivery_list { get; set; }

    }
    public class deliverysummary_list :result
    {
        public string directorder_gid { get; set; }
        public string directorder_refno { get; set; }
        public string directorder_date { get; set; }
        public string customer_name { get; set; }
        public string customer_branchname { get; set; }
        public string customer_contactperson { get; set; }
        public string directorder_status { get; set; }
        public string delivery_status { get; set; }
        public string delivery_details { get; set; }
        public string contact { get; set; }
        public string delivered_by { get; set; }
        public string delivered_remarks { get; set; }

    } 
    public class deliveryadd_list : result
    {
        public string directorder_gid { get; set; }
        public string directorder_refno { get; set; }
        public string directorder_date { get; set; }
        public string customer_name { get; set; }
        public string customer_branchname { get; set; }
        public string customer_contactperson { get; set; }
        public string directorder_status { get; set; }
        public string delivery_status { get; set; }
        public string delivery_details { get; set; }
        public string contact { get; set; }
        public string delivered_by { get; set; }
        public string delivered_remarks { get; set; }

    }
    public class deliverycus_list :result
    {
        public string directorder_gid { get; set; }
        public string directorder_refno { get; set; }
        public string directorder_date { get; set; }
        public string customer_name { get; set; }
        public string customer_branchname { get; set; }
        public string customer_contactperson { get; set; }
        public string customer_contactnumber { get; set; }
        public string customer_address { get; set; }
        public string directorder_remarks { get; set; }
        public string terms_condition { get; set; }
        public string Landline_no { get; set; }
        public string customer_department { get; set; }
        public string grandtotal_amount { get; set; }
        public string addon_amount { get; set; }
        public string mobile { get; set; }
        public string customer_emailid { get; set; }
        public string designation { get; set; }


    } public class deliverycusprod_list : result
    {
        public string productgroup_name { get; set; }
        public string product_code { get; set; }
        public string product_name { get; set; }
        public string product_description { get; set; }
        public string productuom_name { get; set; }
        public string product_qty { get; set; }
        public string product_qtydelivered { get; set; }
        public string discount_amount { get; set; }
        public string tax_amount { get; set; }
        public string product_total { get; set; }
        


    }
    public class postdelivery_list : result
    {
        public string directorder_gid { get; set; }
        public string delivery_to { get; set; }
        public string delivery_date { get; set; }
        public string delivery_by { get; set; }
        public string remarks { get; set; }
    }
}