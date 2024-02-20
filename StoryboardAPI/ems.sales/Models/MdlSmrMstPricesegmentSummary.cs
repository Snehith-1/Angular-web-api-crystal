using ems.sales.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using StoryboardAPI.Models;
using System.Web.Management;

namespace ems.sales.Models
{
    public class MdlSmrMstPricesegmentSummary : result
    {
        public List<campaignassign_list> campaignassign_list { get; set; }

        public List<Getpricesegment_list> pricesegment_list { get; set; }
        public List<Getpricesegmentgrid_list> pricesegmentgrid_list { get; set; }
        public List<GetProductGroupDropdown> GetSmrGroupDtl { get; set; }
        public List<GetProductNameDropdown> GetSmrProductDtl { get; set; }
        public List<GetProductUnitDropdown> GetSmrUnitDtl { get; set; }
        public List<GetProduct> productgroup_list { get; set; }
        public List<GetProductName> OnChangeProductName { get; set; }
        public List<Getproduct_list> products_list { get; set; }
        public List<GetUnassignedlists> GetUnassignedlists { get; set; }
        public List<GetAssignedlists> GetAssignedlists { get; set; }
        public List<GetUnassigned> GetUnassigned { get; set; }



    }
    public class GetUnassigned : result
    {

        public string pricesegment_gid { get; set; }
        public string employee_gid1 { get; set; }

        public string employee_name { get; set; }
        public string employee_gid { get; set; }
    }

    public class GetAssignedlists : result
    {
        public string customer_gid { get; set; }
        public string employee_gid1 { get; set; }
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
    }

    public class GetUnassignedlists : result
    {

        public string pricesegment_gid { get; set; }
        public string employee_gid1 { get; set; }
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
    }

    public class Getpricesegment_list : result
        {

            public string pricesegment_gid { get; set; }
            public string pricesegment_code { get; set; }
            public string pricesegment_name { get; set; }
            public string pricesegmentedit_code { get; set; }
            public string pricesegmentedit_name { get; set; }
            public string created_by { get; set; }
            public string created_date { get; set; }


        }
        public class Getpricesegmentgrid_list : result
        {

            public string pricesegment_gid { get; set; }
            public string customer_gid { get; set; }
            public string customer_name { get; set; }
            public string customer_id { get; set; }
            public string contact_details { get; set; }
            public string region_name { get; set; }
            public string created_by { get; set; }
            public string created_date { get; set; }


        }

    public class GetProductGroupDropdown : result
    {
        public string productgroup_gid { get; set; }
        public string productgroup_name { get; set; }
    }

    public class GetProductNameDropdown : result
    {
        public string product_gid { get; set; }
        public string product_name { get; set; }
    }
    public class GetProductUnitDropdown : result
    {
        public string productuom_gid { get; set; }
        public string productuom_name { get; set; }
    }
    public class GetProduct : result
    {
        public string pricesegment_gid { get; set; }
        public string pricesegment_name { get; set; }

        public string stock_gid { get; set; }
        public string productgroup_name { get; set; }
        public string product_name { get; set; }
        public string productuom_name { get; set; }
        public string product_price { get; set; }
        public string customerproduct_code { get; set; }
        public string created_date { get; set; }

    }
    public class GetProductName : result
    {
        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string productgroup_name { get; set; }
        public string productuom_name { get; set; }
        public string productuom_gid { get; set; }
        public string productgroup_gid { get; set; }
    

    }
    public class Getproduct_list : result
    {

        public string pricesegment_gid { get; set; }
        public string product_gid { get; set; }
        public string stock_gid { get; set; }
        public string product_code { get; set; }
        public string product_name { get; set; }
        public string pricesegment_name { get; set; }
        public string productuom_gid { get; set; }
        public string productuom_name { get; set; }
        public string productgroup_gid { get; set; }
        public string productgroup_name { get; set; }
        public string customerproduct_code { get; set; }
        public string stocktype_gid { get; set; }
        public string remarks { get; set; }
        public string product_price { get; set; }
        public string stock_qty { get; set; }
        public string grn_qty { get; set; }

        public string created_by { get; set; }
        public string created_date { get; set; }
        public string branch_gid { get; set; }


    }
    public class campaignassign_list : result
    {
        public string pricesegment_gid { get; set; }
        public string pricesegment_name { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string employee_gid { get; set; }

        public campaignassign[] campaignassign;
    }
    public class campaignassign : result
    {
        public string _id { get; set; }
        public string _key1 { get; set; }
        public string _key3 { get; set; }
        public string _name { get; set; }
        
    }
}