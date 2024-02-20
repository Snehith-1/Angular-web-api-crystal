using System;
using System.Collections.Generic;
using System.Drawing;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace ems.inventory.Models
{
    public class MdlImsTrnOpenDCSummary:result
    {
        public List<opndcsummary_list> opndcsummary_list { get; set; }
        public List<opendcadd_list> opendcadd_list { get; set; }
        public List<opendcaddsel_list> opendcaddsel_list { get; set; }
        public List<opendcaddselprod_list> opendcaddselprod_list { get; set; }
        public List<opendc_list> opendc_list { get; set; }
    }

    public class opndcsummary_list : result
    {
        public string directorder_gid { get; set; }
        public string directorder_refno { get; set; }
        public string directorder_date { get; set; }
        public string user_firstname { get; set; }
        public string customer_name { get; set; }
        public string customer_branchname { get; set; }
        public string customer_contactperson { get; set; }
        public string directorder_status { get; set; }
        public string delivery_status { get; set; }
        public string delivery_details { get; set; }
        public string contact { get; set; }
        public string created_by { get; set; }
    }

    public class opendcadd_list : result
    {
        public string salesorder_gid { get; set; }
        public string so_referenceno1 { get; set; }
        public string salesorder_date { get; set; }
        public string qty_quoted { get; set; }
        public string product_delivered { get; set; }
        public string customer_name { get; set; }
        public string customer_contact_person { get; set; }
        public string salesorder_status { get; set; }
        public string mobile { get; set; }
        public string despatch_status { get; set; }
        public string contact { get; set; }

    }
    public class opendcaddsel_list : result
    {
        public string salesorder_gid { get; set; }
        public string so_referenceno1 { get; set; }
        public string salesorder_date { get; set; }
        public string termsandconditions { get; set; }
        public string customer_gid { get; set; }
        public string customer_code { get; set; }
        public string customer_contact_person { get; set; }
        public string grandtotal { get; set; }
        public string mobile { get; set; }
        public string customer_address { get; set; }
        public string designation { get; set; }
        public string customercontact_name { get; set; }
        public string email { get; set; }
        public string currency_code { get; set; }
        public string shipping_to { get; set; }
        public string customer_mobile { get; set; }
        public string customer_email { get; set; }
        public string customer_address_so { get; set; }
        public string directorder_refno { get; set; }
        public string directorder_date { get; set; }

    }
    public class opendcaddselprod_list : result
    {
        public string salesorder_gid { get; set; }
        public string salesorderdtl_gid { get; set; }
        public string product_gid { get; set; }
        public string productgroup_gid { get; set; }
        public string productgroup_name { get; set; }
        public string product_name { get; set; }
        public string uom_gid { get; set; }
        public string uom_name { get; set; }
        public string qty_quoted { get; set; }
        public string display_field { get; set; }
        public string product_delivered { get; set; }
        public string product_price { get; set; }
        public string discount_percentage { get; set; }
        public string discount_amount { get; set; }
        public string tax_amount { get; set; }
        public string tax_amount2 { get; set; }
        public string tax_amount3 { get; set; }
        public string tax_name { get; set; }
        public string tax_name2 { get; set; }
        public string tax_name3 { get; set; }
        public string tax1_gid { get; set; }
        public string tax2_gid { get; set; }
        public string tax3_gid { get; set; }
        public string price { get; set; }
        public string stockable { get; set; }
        public string available_quantity { get; set; }
        public string serial_flag { get; set; }
        public string branch_gid { get; set; }
       
    }

    public class opendc_list : result
    {
        public string available_quantity { get; set; }
        public string despatch_quantity { get; set; }
        public string created_name { get; set; }
        public string customer_code { get; set; }
        public string customer_contact_person { get; set; }
        public string customer_mobile { get; set; }
        public string customer_address_so { get; set; }
        public string termsandconditions { get; set; }
        public string grandtotal { get; set; }
        public string shipping_to { get; set; }
        public string customer_email { get; set; }
        public string productgroup_name { get; set; }
        public string product_name { get; set; }
        public string uom_name { get; set; }
        public string qty_quoted { get; set; }
        public string display_field { get; set; }
        public string price { get; set; }
        public string discount_percentage { get; set; }
        public string discount_amount { get; set; }
        public string tax_name { get; set; }
        public string tax_name2 { get; set; }
        public string total_amount { get; set; }
        public string tax_name3 { get; set; }
        public string dc_no { get; set; }
        public string despatch_mode { get; set; }
        public string tracker_id { get; set; }
        public string tax_amount { get; set; }
        public string tax_amount2 { get; set; }
        public string tax_amount3 { get; set; }
        public string tax1_gid { get; set; }
        public string tax2_gid { get; set; }
        public string tax3_gid { get; set; }
    }
}