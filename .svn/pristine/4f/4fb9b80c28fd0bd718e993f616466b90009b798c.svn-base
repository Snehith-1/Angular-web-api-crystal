using StoryboardAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace ems.sales.Models
{
    public class MdlSmrTrnCustomerSummary
    {
        public List<smrcustomer_list> smrcustomer_list { get; set; }
        public List<document_list1> document_list1 { get; set; }
        public List<postcustomer_list> postcustomer_list { get; set; }
        public List<Getcountry> Getcountry { get; set; } 
        public List<GetOnchangecuontry> GetOnchangecuontry { get; set; } 

        public List<branch_list> branch_list { get; set; }
        public List<Getcurency> Getcurency { get; set; }
        public List<Gettax> Gettax { get; set; }
        public List<Getregion> Getregion { get; set; }
        public List<Getcustomercity> Getcustomercity { get; set; }
        public List<customercount_list> customercount_list { get; set; }
        public List<customerexport_list> customerexport_list { get; set; }
        public List<Getproductlist> product_list { get; set; }
        public List<GetCustomerlist> GetCustomerList { get; set; }
        public List<customerbranch_list> customerbranch_list { get; set; }
        public List<documentdtl_list> documentdtl_list { get; set; }
        public List<smrcustomerbranch_list> smrcustomerbranch_list { get; set; }
        public List<customercontact_list> customercontact_list { get; set; }
        public List<customercontact_list1> customercontact_list1 { get; set; }
        public List<customertype_list1> customertype_list1 { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
        public string customer_id { get; set; }
    }
    public class documentdtl_list : result
    {
        public string customername { get; set; }
        public string customercode { get; set; }
        public string remarks { get; set; }
    }
    public class customertype_list1 : result
    {
        public string customertype_gid1 { get; set; }
        public string customer_type1 { get; set; }

    }
    public class document_list1 : result
    {
        public string document_name { get; set; }
        public string updated_by { get; set; }
        public string uploaded_date { get; set; }
        public string importcount { get; set; }
    }
    public class smrcustomer_list : result
    {
        public string customer_gid { get; set; }
        public string customer_id { get; set; }
        public string customer_name { get; set; }
        public string customer_type { get; set; }
        public string contact_details { get; set; }
        public string region_name { get; set; }
        public string customer_state { get; set; }
        public string last_order_date { get; set; }
        public string first_order_date { get; set; }
        public string customer_since { get; set; }
        public string status { get; set; }


    }
    public class customercount_list : result
    {
        public string total_count { get; set; }
        public string corporate_count { get; set; }
        public string retailer_counts { get; set; }
        public string distributor_count { get; set; }



    }
    public class Getcountry : result
    {
        public string country_name { get; set; }
        public string country_gid { get; set; }

    }
    public class Getcurency : result
     {
        public string currency_code { get; set; }
        public string currencyexchange_gid { get; set; }

    } 
    
    public class branch_list : result
     {
        public string customerbranch_name { get; set; }
        public string customer_gid { get; set; } 
        public string customercontact_gid { get; set; }

    }
    public class Gettax : result
    {
        public string tax_name { get; set; }
        public string tax_gid { get; set; }

    }
    public class Getcustomercity : result
    {
        public string customer_gid { get; set; }
        public string customer_city { get; set; }

    }
    public class Getregion : result
    {
        public string region_name { get; set; }
        public string region_gid { get; set; }

    }
    public class postcustomer_list : result
    {
        public string customer_code { get; set; }
        public string customer_id { get; set; }
        public string company_website { get; set; }
        public string customer_name { get; set; }
        public string customer_address { get; set; }
        public string customer_address2 { get; set; }
        public string customer_city { get; set; }
        public string gst_number { get; set; }
        public string countryname { get; set; }
        public string region { get; set; }
        public string customer_state { get; set; }
        public string phone { get; set; }
        public string state { get; set; }
        public string customercontact_name { get; set; }
        public string taxname { get; set; }
        public string designation { get; set; }
        public string email { get; set; }
        public string postal_code { get; set; }
        public string fax { get; set; }
        public string customer_region { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string fax_area_code { get; set; }
        public string area_code { get; set; }
        public string fax_country_code { get; set; }
        public string country_code { get; set; }
        public string address1 { get; set; }
        public string phone1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string currency_code { get; set; }
        public string region_name { get; set; }
        public string customerbranch_name { get; set; }
        public string customer_type { get; set; }
        public string country_gid { get; set; }
        public string country_name { get; set; } 
        public string currencyexchange_gid { get; set; }
        public string customer_pin { get; set; }
        public string mobile { get; set; }

        public getmobile mobiles { get; set; }



    }



    public class getmobile
    {
        public string e164Number { get; set; }

    }

    public class Getproductlist : result
    {


        public string producttype_name { get; set; }
        public string selling_price { get; set; }
        public string productgroup_name { get; set; }
        public string productgroup_code { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string product_price { get; set; }
        public string stock_gid { get; set; }

        public string mrp_price { get; set; }
       
        public string cost_price { get; set; }
        public string productuomclass_code { get; set; }
        public string productuom_code { get; set; }
        public string productuomclass_name { get; set; }
        public string stockable { get; set; }
        public string productuom_gid { get; set; }
        public string productuom_name { get; set; }
        public string product_type { get; set; }
        public string Status { get; set; }
        public string serial_flag { get; set; }
        public string lead_time { get; set; }

        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string product_code { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }

        public string product_desc { get; set; }
        public string currency_code { get; set; }
        public string avg_lead_time { get; set; }
        public string purchasewarrenty_flag { get; set; }
        public string expirytracking_flag { get; set; }
        public string batch_flag { get; set; }
        public string productgroupname { get; set; }
        public string producttypename { get; set; }
        public string productuomclassname { get; set; }
        public string productuomname { get; set; }
        public string pricesegment2product_gid { get; set; }

        public List<Customerproduct_list> salesproduct_list { get; set; }


    }

    public class GetCustomerlist : result
        {
        public string customer_id { get; set; }
        public string customer_name { get; set;}
        public string customer_gid { get; set;}
        public string customercontact_name { get; set; }
        public string customer_type { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string postal_code { get; set; }
        public string countryname { get; set; }
        public string email { get; set; }
        public string currencyname { get; set; }
        public string region_name { get; set; }
        public string gst_number { get; set; }
        public string company_website { get; set; }
        public string country_code { get; set; }
        public string area_code { get; set; }
        public string fax_number { get; set;}
        public string designation { get; set; }
        public string customer_state { get; set; }
        public string leadbaank_gid { get; set; }
        public string currencyexchange_gid { get; set; }
        public string customercontact_gid { get; set; }
        public string country_name { get; set; }
        public string mobile_number { get; set; }


        public string mobiles { get; set; }

    }
    public class getmobile3
    {
        public string e164Number { get; set; }

    }

    public class Customerproduct_list : result
    {
        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string product_code { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string productuom_gid { get; set; }
        public string productuom_name { get; set; }
        public string customer_id { get; set; }
        public string customer_name { get; set; }
        public string customer_gid { get; set; }
        public string mrp_price { get; set; }
        public string product_price { get; set; }
        public string producttype_name { get; set; }
        public string selling_price { get; set; }
        public string cost_price { get; set; }
        public string productgroup_code { get; set; }
        public string productgroup { get; set; }
        public string customerproduct_code { get; set; }
        public string productgroup_name { get; set; }
        public string branch_gid { get; set; }
        public string stock_gid { get; set; }
        public string pricesegment2product_gid { get; set; }
    }
    public class customerbranch_list : result
    {
        public string customer_gid { get; set; }
        public string customercontact_gid { get; set; }
        public string customer_city { get; set; }
        public string region_name { get; set; }
        public string customer_state { get; set; }
        public string customercontact_name { get; set; }
        public string designation { get; set; }
        public string email { get; set; }
        public string customer_pin { get; set; }
        public string country_name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public getmobile2 mobiles { get; set; }
        public string customerbranch_name { get; set; }
        public string contactperson_name { get; set; }
        public string mobile { get; set; }

    }



    public class getmobile2
    {
        public string e164Number { get; set; }

    }
    public class smrcustomerbranch_list : result
    {
        public string customer_gid { get; set; }
        public string customerbranch_name { get; set; }
        public string customer_city { get; set; }
        public string customer_pin { get; set; }
        public string customer_state { get; set; }
        public string country_name { get; set; }
        public string customercontact_name { get; set; }
        public string mobile { get; set; }
        public string designation { get; set; }
        public string customer_address { get; set; }

    }
    public class customercontact_list : result
    {
        public string customer_gid { get; set; }
        public string country_gid { get; set; }
        public string customercontact_gid { get; set; }
        public string customercontact_name { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country_name { get; set; }
        public string designation { get; set; }
        public string address1 { get; set; }  
        public string zip_code { get; set; }
         public string address2 { get; set; }
        public string email { get; set; }
        public string user_gid { get; set; } 
        public string customerbranch_name { get; set; }
        public getmobile1 mobiles { get; set; }

    }


    public class getmobile1 
    {
        public string e164Number { get; set; }

    }


    public class customercontact_list1 : result
    {
        public string customer_gid { get; set; }
        public string customerbranch_name { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip_code { get; set; }
        public string designation { get; set; }
        public string country_name { get; set; }
        public string customercontact_name { get; set; }
        public string mobiles { get; set; }
        public string address1 { get; set; } 
        public string address2 { get; set; }


    }




    public class customerexport_list : result
    {
        public string lspath1 { get; set; }
        public string lsname2 { get; set; }
    }

    public class GetOnchangecuontry : result
    {
        public string currency_code { get; set; }
        public string country_name { get; set; }
    }




}


