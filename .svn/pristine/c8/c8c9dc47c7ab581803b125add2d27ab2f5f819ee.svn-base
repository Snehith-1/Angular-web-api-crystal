using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.pmr.Models
{
    public class MdlPmrTrnPurchaseRequisition : result
    {
        public List<request_list> purchaserequest_list { get; set; }
       
        public List<GetBranch1> GetBranch1 { get; set; }       
        public List<Getuserdtl> Getuserdtl { get; set; }
        public List<Getcostcenter> Getcostcenter { get; set; }
        public List<GetProductCode1> GetProductCode1 { get; set; }
        public List<GetProduct1> GetProduct1 { get; set; }
        public List<productsummary_list1> productsummary_list1 { get; set; }
        public List<PostAllPR> PostAllPR { get; set; }
        public List<productlistdetailspr> productlistdetailspr { get; set; }
        public List<purchaserequestitionviewlist> purchaserequestitionview_list { get; set; }
     

    }

   
    public class request_list : result
    {
        public string purchaserequisition_gid { get; set; }
        public string purchaserequisition_date { get; set; }
        public string costcenter_name { get; set; }
        public string purchaserequisition_referencenumber { get; set; }
        public string user_firstname { get; set; }
        public string purchaserequisition_remarks { get; set; }
        public string overall_status { get; set; }
        public string branch_name { get; set; }
    }
    //branch
    public class GetBranch1
    {
        public string branch_gid { get; set; }
        public string branch_name { get; set; }
        public string address1 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postal_code { get; set; }
    }
    //depatmentand name 
    public class Getuserdtl : result
    {
        public string department_name { get; set; }
        public string employee_name { get; set; }

    }

    //costcenter
    public class Getcostcenter : result
    {
        public string costcenter_gid { get; set; }
        public string costcenter_name { get; set; }
        public string available_amount { get; set; }

    }
    public class GetProduct1 : result
    {
        public string product_gid { get; set; }
        public string product_name { get; set; }

    }
    public class GetProductCode1 : result
    {
        public string product_gid { get; set; }
        public string product_code { get; set; }
        public string productgroup_name { get; set; }
        public string productuom_name { get; set; }
        public string productuom_gid { get; set; }
        public string productgroup_gid { get; set; }
        public string product_name { get; set; }
        public string display_field { get; set; }

    }

    public class productlist1 : result
    {
       
        public string qty_requested { get; set; }
        public string product_name { get; set; }
        public string product_gid { get; set; }      
        public string productuom_gid { get; set; }
        public string productuom_name { get; set; }     
        public string productgroup_gid { get; set; }
        public string productgroup_name { get; set; }   
        public string product_code { get; set; }
        public string display_field { get; set; }

    }

    public class productsummary_list1 : result
    {
        public string tmppurchaserequisition_gid { get; set; }
        public string qty_requested { get; set; }
        public string product_name { get; set; }
        public string product_gid { get; set; }
        public string productuom_gid { get; set; }
        public string productuom_name { get; set; }
        public string productgroup_gid { get; set; }
        public string productgroup_name { get; set; }
        public string product_code { get; set; }
        public string display_field { get; set; }

    }
    public class PostAllPR : result
    {
        public string tmppurchaserequisition_gid { get; set; }
        public string qty_requested { get; set; }
        public string product_name { get; set; }
        public string product_gid { get; set; }
        public string productuom_gid { get; set; }
        public string productuom_name { get; set; }
        public string productgroup_gid { get; set; }
        public string productgroup_name { get; set; }
        public string product_code { get; set; }
        public string purchaserequisition_gid { get; set; }
        public string purchaserequisition_date { get; set; }
        public string costcenter_name { get; set; }
        public string purchaserequisition_referencenumber { get; set; }
        public string user_firstname { get; set; }
        public string purchaserequisition_remarks { get; set; }
        public string priority_remarks { get; set; }
        public string branch_name { get; set; }
        public string branch_gid { get; set; }
        public string address1 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postal_code { get; set; }
        public string costcenter_gid { get; set; }
        public string available_amount { get; set; }



    }
    public class productlistdetailspr : result
    {
        public string customerproduct_code { get; set; }
        public string product_code { get; set; }
        public string product_name { get; set; }
        public string qty_requested { get; set; }
        public string pr_originalqty { get; set; }
    }

    public class purchaserequestitionviewlist : result 
    {
        public string purchaserequisition_gid { get; set; }
        public string product_name { get; set; }
        public string qty_requested { get; set; }
        public string product_code { get; set; }
        public string purchaserequisition_date { get; set; }
        public string purchaserequisition_remarks { get; set; }
        public string purchaserequisition_referencenumber { get; set; }
        public string branch_name { get; set; }
        public string user_firstname { get; set; }
        public string department_name { get; set; }
        public string productgroup_name { get; set; }
        public string productuom_name { get; set; }
        public string display_field { get; set; }
    }
}

