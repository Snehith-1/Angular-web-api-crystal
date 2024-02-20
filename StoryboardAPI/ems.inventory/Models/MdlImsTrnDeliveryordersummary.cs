﻿using CrystalDecisions.CrystalReports.ViewerObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.inventory.Models
{
    public class MdlImsTrnDeliveryordersummary:result
    {
        public List<adddeliveryorder_list> adddeliveryorder_list { get; set; }
        public List<deliveryorder_list> deliveryorder_list { get; set; }
        public List<raisedelivery_list> raisedelivery_list { get; set; }
        public List<OutstandingQty_list> OutstandingQty_list { get; set; }
        public List<IssuedQty_list> IssuedQty_list { get; set; } 
        public List<deliveryorderview_list> deliveryorderview_list { get; set; }
        public List<deliveryorderview_list1> deliveryorderview_list1 { get; set; }
        public string product_gid { get; set; }
        public string uom_gid { get; set; }
        public string branch_gid { get; set; }


    }
    public class MdlImsTrnDeliveryorder : result
    {
        public string deliveryorder_date { get; set; }
        public string directorder_refno { get; set; }
        public string customerid { get; set; }
        public string branch_gid { get; set; }
        public string stock_gid { get; set; }
        public string customer_branch { get; set; }
        public string customer_name { get; set; }
        public string customercontact_names { get; set; }
        public string customer_mobile { get; set; }
        public string customer_address { get; set; } 
        public string template_content { get; set; }
        public string dc_no { get; set; }
        public string customer_mode { get; set; }
        public string customer_city { get; set; }
        public string salesorder_date { get; set; }
        public string customer_address_so { get; set; }
        public string customer_email { get; set; } 
        public string salesorder_gid { get; set; }
        public string salesorderdtl_gid { get; set; }
        public string customer_gid { get; set; }
        public string grandtotalamount { get; set; }
        public string product_delivered { get; set; }
        public string product_gid { get; set; }
        public string display_field { get; set; }
        public string productgroup_gid { get; set; }
        public string productgroup_name { get; set; }
        public string available_quantity { get; set; }
        public string price { get; set; }
        public string discount_percentage { get; set; }
        public string discount_amount { get; set; }
        public string tax_name { get; set; }
        public string tax_name2 { get; set; }
        public string tax_name3 { get; set; }
        public string product_total { get; set; }
        public string tax_amount { get; set; }
        public string tax_amount2 { get; set; }
        public string tax_amount3 { get; set; }
        public string tax1_gid  { get; set; }
        public string tax2_gid { get; set; }
        public string tax3_gid { get; set; }


    }
    public class deliveryorder_list : result
    {
        public string directorder_date { get; set; }
        public string directorder_refno { get; set; }
        public string so_referenceno1 { get; set; }
        public string customer_name { get; set; }
        public string contact { get; set; }
        public string directorder_gid { get; set; }
        public string branch_name { get; set; }
        public string user_firstname { get; set; }
        public string delivery_status { get; set; }
        public string dc_no { get; set; }
  
       
        
            
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
    public class OutstandingQty_list : result
    {
        public string outstanding_qty { get; set; }
        public string product_name { get; set; }
        public string productuom_name { get; set; }
        public string uom_name { get; set; }
        public string display_field { get; set; }
        public string display { get; set; }
        public string product_gid { get; set; }
        public string uom_gid { get; set; }
        public string branch_gid { get; set; }
        public string created_date { get; set; }
        public string stock_gid { get; set; }
        public string reference_gid { get; set; }
        public string stock_qty { get; set; }
        public string salesorder_gid { get; set; }
        public string total_amount { get; set; }
        public string producttotalamount { get; set; }
        public string mrdtl_gid { get; set; }
     
      


    }
    public class raisedelivery_list : result
    {
        
        public string customer_address_so { get; set; }
        public string customer_mobile { get; set; }
        public string product_gid { get; set; }
        public string uom_gid { get; set;}
        public string stock_gid { get; set; }
        public string customercontact_names { get; set; }
        public string salesorder_date { get; set; }
        public string customer_email { get; set; }    
        public string customer_address{ get; set; }
        public string so_referencenumber { get; set; }
        public string salesorder_gid { get; set; }
        public string delivery_quantity {  get; set; }
        public string customer_name { get; set; }
        public string customer_branch { get; set; }  
        public string customer_branchname { get; set; }  
        public string salesorderdtl_gid { get; set; } 
        public string productgroup_name { get; set; } 
        public string customerproduct_code { get; set; } 
        public string product_code { get; set; }
        public string product_name { get; set; }
        public string display_field { get; set; }
        public string uom_name { get; set; }
        public string available_quantity { get; set; }
        public string qty_quoted { get; set; }
        public string product_delivered { get; set; }
        public string product_requireddate { get; set; }

    



    }
    public class IssuedQty_list : result
    {


        public string txtstocktotal { get; set; }
        public string lbloutstanding_qty { get; set; }
        public double txtissuedqty { get; set; }
        public double lblstock_qty { get; set; }
        public string stock_gid { get; set; }
        public double salesorderdtl_gid { get; set; }
        public double mrdtl_gid { get; set; }
        public double display_field { get; set; }
        public string product_gid { get; set; }
        public string uom_gid { get; set; }
        public string branch_gid { get; set; }
        public string salesorder_gid { get; set; }
        public string available_quantity { get; set; }

        public string created_date { get; set; }
       
        public string display { get; set; }
        public string reference_gid { get; set; }
        public string stock_qty { get; set; }
        public string product_name { get; set; }
        public string uom_name { get; set; }
        public string total_amount { get; set; }
        public string outstanding_qty { get; set; }
        public string producttotalamount { get; set; }
       
        public List<OutstandingQty_list> OutstandingQty_list { get; set; }
        public List<raisedelivery_list> raisedelivery_list {  get; set; }


    }
    public class deliveryorderview_list : result
    {
        public string directorder_date { get; set; }
        public string directorder_refno { get; set; }
        public string customer_name { get; set; }
        public string mobile { get; set; }
        public string customer_address { get; set; }
        public string shipping_to { get; set; }
        public string customer_emailid { get; set; }
        public string tracker_id { get; set; }
        public string directorder_remarks { get; set; }
        public string dc_no { get; set; }
        public string customer_contactperson { get; set; }
        public string mode_of_despatch { get; set; } 
        public string terms_condition { get; set; } 
       


    }

    public class deliveryorderview_list1 : result
    {

        public string product_name { get; set; }
        public string product_code { get; set; }
        public string productuom_name { get; set; }
        public string total_qty_delivered { get; set; }
        public string product_qty { get; set; }
        public string product_qtydelivered { get; set; }
        public string qty_return { get; set; }
    }
}