
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;




namespace ems.pmr.Models
{ 
    public class MdlPmrTrnRaiseEnquiry : result
    {
        public List<GetCusEnquiry> cusenquiry_list { get; set; }
        public List<GetProductGrp> GetProductGrp { get; set; }
        public List<GetProducts> GetProducts { get; set; }
        public List<GetProductUnits> GetProductUnits { get; set; }
        public List<GetAssignDropdown> GetEmployeePerson { get; set; }
        public List<viewvendorenquiry_list> viewvendorenquiry_list { get; set; }
        public List<DirecteditenquiryList> directeditenquiry_list { get; set; }

        public List<GetProductsName> GetProductsName { get; set; }
        public List<productsummarys_list> productsummarys_list { get; set; }
        public List<productslist> productslist { get; set; }
        public List<GetCurrencyDetsDropdown> GetCurrencyDets { get; set; }
        public List<GetProductDetDropdown> GetProductDets { get; set; }
        public List<GetProductNameDetsDropdown> GetProductNameDets { get; set; }
        public List<GetFirsttaxDropdown> GetFirstTax { get; set; }
        public List<GetSecondtaxDropdown> GetSecondTax { get; set; }
        public List<GetThirdtaxDropdown> GetThirdTax { get; set; }
        public List<GetFourthtaxDropdown> GetFourthTax { get; set; }
        public List<GetTermsDropdown> terms_lists { get; set; }
        public List<GetBranchDetsDropdown> GetBranchDet { get; set; }
        public List<GetVendorraise> GetVendorlist { get; set; }
        public List<GetVendorname> GetVendor { get; set; }
        public double grand_total { get; set; }
        public double producttotalamount { get; set; }
        public double grandtotal { get; set; }

        public string product_name { get; set; }
        public string productuom_name { get; set; }
        public string productgroup_name { get; set; }
        public string product_code { get; set; }
        public string qty_requested { get; set; }
        public string potential_value { get; set; }

    }

    public class GetVendorname : result
    {
        public string vendor_companyname { get; set; }
        public string vendor_gid { get; set; }
    }

        public class productslist : result
    {
        public string product_name { get; set; }
        public string productgroup_name { get; set; }
        public string productuom_name { get; set; }
        public string productuom_gid { get; set; }
        public string productgroup_gid { get; set; }
        public string product_code { get; set; }
        public string tmpsalesenquiry_gid { get; set; }
        public string vendorproduct_code { get; set; }
        public string qty_requested { get; set; }
        public string product_requireddate { get; set; }
        public string potential_value { get; set; }
        public string product_requireddateremarks { get; set; }
        public string product_gid { get; set; }


    }
    public class DirecteditenquiryList : result
    {
        public string tmpsalesenquiry_gid { get; set; }
        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string product_code { get; set; }
        public string productuom_name { get; set; }
        public string qty_requested { get; set; }
        public string potential_value { get; set; }
        public string product_requireddate { get; set; }
        public string productgroup_name { get; set; }
        public string productgroup_gid { get; set; }

    }

    public class productsummarys_list : result
    {
        public string enquiry_type { get; set; }
        public string display_field { get; set; }

        public string product_name { get; set; }
        public string productgroup_name { get; set; }
        public string productuom_name { get; set; }
        public string productuom_gid { get; set; }
        public string productgroup_gid { get; set; }
        public string product_code { get; set; }
        public string tmpsalesenquiry_gid { get; set; }
        public string vendorproduct_code { get; set; }
        public string qty_requested { get; set; }
        public string product_requireddate { get; set; }
        public string potential_value { get; set; }
        public string product_requireddateremarks { get; set; }
        public string product_gid { get; set; }
        public string stockable { get; set; }
        public string BOM { get; set; }
        public string product_type { get; set; }
        public string tmpquotationdtl_gid { get; set; }




    }
    public class GetProductsName : result
    {
        public string product_name { get; set; }
        public string productgroup_name { get; set; }
        public string productuom_name { get; set; }
        public string productuom_gid { get; set; }
        public string productgroup_gid { get; set; }
        public string product_code { get; set; }


    }
    public class viewvendorenquiry_list : result
    {
        public string vendor_gid { get; set; }
        public string enquiry_gid { get; set; }
        public string enquiry_date { get; set; }
        public string branch_name { get; set; }
        public string enquiry_referencenumber { get; set; }
        public string vendor_companyname { get; set; }
        public string vendor_contact_person { get; set; }
        public string contact_email { get; set; }
        public string vendor_address { get; set; }
        public string closure_date { get; set; }
        public string contact_number { get; set; }
        public string branch_gid { get; set; }
        public string productgroup_name { get; set; }
        public string productuom_name { get; set; }
        public string product_requireddate { get; set; }
        public string potential_value { get; set; }
        public string vendorproduct_code { get; set; }
        public string qty_enquired { get; set; }
        public string product_name { get; set; }
        public string product_code{ get; set; }








    }
    public class GetTermsDropdown : result
    {
        public string template_gid { get; set; }
        public string template_name { get; set; }
        public string template_content { get; set; }

    }

    public class GetProductGrp : result
    {
        public string productgroup_name { get; set; }
        public string productgroup_gid { get; set; }

    }
    public class GetProducts : result
    {
        public string product_name { get; set; }
        public string product_gid { get; set; }

    }
    public class GetProductUnits : result
    {
        public string productuom_name { get; set; }
        public string uom_gid { get; set; }

    }

    public class GetCurrencyDetsDropdown : result
    {
        public string currencyexchange_gid { get; set; }
        public string currency_code { get; set; }
    }

    public class GetProductDetDropdown : result
    {
        public string product_gid { get; set; }
        public string product_name { get; set; }
    }

    public class GetProductNameDetsDropdown : result
    {
        public string product_name { get; set; }
        public string productgroup_name { get; set; }
        public string productuom_name { get; set; }
        public string productuom_gid { get; set; }
        public string productgroup_gid { get; set; }
        public string product_code { get; set; }
    }

    public class GetFirsttaxDropdown : result
    {
        public string tax_gid { get; set; }
        public string tax_name { get; set; }
        public string percentage { get; set; }
    }

    public class GetSecondtaxDropdown : result
    {
        public string tax_gid2 { get; set; }
        public string tax_name2 { get; set; }
        public string percentage { get; set; }

    }
    public class GetThirdtaxDropdown : result
    {
        public string tax_gid3 { get; set; }
        public string tax_name3 { get; set; }
        public string percentage { get; set; }

    }

    public class GetFourthtaxDropdown : result
    {
        public string tax_gid4 { get; set; }
        public string tax_name4 { get; set; }
        public string percentage { get; set; }
    }
    public class GetBranchDetsDropdown : result
    {
        public string branch_gid { get; set; }
        public string branch_name { get; set; }
    }
    public class GetVendorraise : result
    {
        public string vendor_name { get; set; }
        public string vendor_gid { get; set; }
        public string vendor_companyname { get; set; }
        public string vendorbranch_name { get; set; }
        public string postal_code { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string address2 { get; set; }
        public string country_name { get; set; }
        public string address1 { get; set; }
        public string contact_telephonenumber { get; set; }
        public string email_id { get; set; }
        public string country_gid { get; set; }
        public string contactperson_name { get; set; }
    }
  
        public class GetAssignDropdown : result
    {
        public string campaign_gid { get; set; }
        public string user_firstname { get; set; }
        public string campaign_title { get; set; }
        public string employee_gid { get; set; }
    }

    public class PostAll : result
    {
        public string leadbank_gid { get; set; }
        public string enquiry_gid { get; set; }
        public string enquiry_date { get; set; }
        public string enquiry_refno { get; set; }
        public string branch_name { get; set; }
        public string vendor_companyname { get; set; }
        public string enquiry_referencenumber { get; set; }
        public string contact_details { get; set; }
        public string enquiry_status { get; set; }
        public string customer_rating { get; set; }
        public string user_firstname { get; set; }
        public string customer_gid { get; set; }
        public string branch_gid { get; set; }
        public string contact_number { get; set; }
        public string customercontact_name { get; set; }
        public string contact_email { get; set; }
        public string customerbranch_name { get; set; }
        public string contact_address { get; set; }
        public string enquiry_remarks { get; set; }
        public string customer_requirement { get; set; }
        public string landmark { get; set; }
        public string closure_date { get; set; }
        public string contact_person { get; set; }
        public string employee_gid { get; set; }
        public string i { get; set; }
        public string campaign_gid { get; set; }
        public string product_name { get; set; }
        public string productgroup_name { get; set; }
        public string productuom_name { get; set; }
        public string productuom_gid { get; set; }
        public string productgroup_gid { get; set; }
        public string product_code { get; set; }
        public string product_requireddateremarks { get; set; }
        public string product_requireddate { get; set; }
        public string customerproduct_code { get; set; }
        public string qty_requested { get; set; }
        public string potential_value { get; set; }
        public string display_field { get; set; }
        public string product_gid { get; set; }

        public string tmpsalesenquiry_gid { get; set; }

        public string vendor_requirement { get; set; }
        public string address1 { get; set; }
        public string contactperson_name { get; set; }
        public string vendor_gid { get; set; }
        public string contact_telephonenumber { get; set; }
        public string vendorproduct_code { get; set; }
        public string email_id { get; set; }

        public string vendorbranch_name { get; set; }

    }

    public class GetCusEnquiry : result
    {
        public string vendor_rating { get; set; }
        public string vendor_name { get; set; }
        public string assigned_by { get; set; }
        public string enquiry_gid { get; set; }
        public string enquiry_date { get; set; }
        public string enquiry_refno { get; set; }
        public string branch_name { get; set; }
        public string customer_name { get; set; }
        public string created_date { get; set; }
        public string enquiry_referencenumber { get; set; }
        public string contact_details { get; set; }
        public string campaign { get; set; }
        public string potorder_value { get; set; }
        public string lead_status { get; set; }
        public string enquiry_status { get; set; }
        public string customer_rating { get; set; }
        public string user_firstname { get; set; }
        public string leadstage_gid { get; set; }
        public string internal_notes { get; set; }
        public string leadstage_name { get; set; }
        public string campaign_gid { get; set; }
        public string campaign_title { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string product_name { get; set; }
        public string productgroup_name { get; set; }
        public string productuom_name { get; set; }
        public string productuom_gid { get; set; }
        public string productgroup_gid { get; set; }
        public string product_code { get; set; }
        public string product_requireddateremarks { get; set; }
        public string product_requireddate { get; set; }
        public string customerproduct_code { get; set; }
        public string qty_requested { get; set; }
        public string potential_value { get; set; }
        public string display_field { get; set; }
        public string product_gid { get; set; }
        public string customer_gid { get; set; }
        public string branch_gid { get; set; }
        public string contact_number { get; set; }
        public string customercontact_name { get; set; }
        public string contact_email { get; set; }
        public string customerbranch_name { get; set; }
        public string contact_address { get; set; }
        public string enquiry_remarks { get; set; }
        public string customer_requirement { get; set; }
        public string landmark { get; set; }
        public string closure_date { get; set; }
        public string contact_person { get; set; }
        public string i { get; set; }
        public string currency_gid { get; set; }
        public string created_by { get; set; }
        public string quotationdate { get; set; }

        public string customercontactperson { get; set; }

        public string remarks { get; set; }
        public string grandtotal { get; set; }
        public string termsandconditions { get; set; }
        public string payment_days { get; set; }
        public string delivery_days { get; set; }
        public string addon_charge { get; set; }
        public string contact_no { get; set; }
        public string contact_mail { get; set; }
        public string freight_terms { get; set; }
        public string payment_terms { get; set; }
        public string addon_charge_l { get; set; }
        public string additional_discount_l { get; set; }
        public string grandtotal_l { get; set; }
        public string currency_code { get; set; }
        public string exchange_rate { get; set; }
        public string pricingsheet_gid { get; set; }
        public string pricingsheet_refno { get; set; }
        public string customer_address { get; set; }
        public string total_price { get; set; }
        public string total_amount { get; set; }
        public string gst_percentage { get; set; }
        public string tax_gid { get; set; }
        public string tax_name { get; set; }
        public string salesperson_gid { get; set; }
        public string roundoff { get; set; }
        public string customerenquiryref_number { get; set; }
        public string additional_discount { get; set; }
        public string freight_charges { get; set; }
        public string buyback_charges { get; set; }
        public string packing_charges { get; set; }
        public string insurance_charges { get; set; }

    }

}