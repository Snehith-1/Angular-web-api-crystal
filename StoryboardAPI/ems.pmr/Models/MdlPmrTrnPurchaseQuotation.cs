using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.pmr.Models
{
    public class MdlPmrTrnPurchaseQuotation : result
    {
        public List<quotation_list> quotation_list { get; set; }
        public List<GetVendordtl> GetVendordtl { get; set; }
        public List<Vendor_list1> Vendor_list { get; set; }
        public List<GetOnchangecurrency> GetOnchangecurrency { get; set; }

        public List<GetCurrencyCodeDropdown> GetCurrencyCodeDropdown { get; set; }
        public List<summaryprod_list> summaryprod_list { get; set; }
        public List<summarys_lists> summarys_lists { get; set; }
        public List<tempsummary_list> prodsummary_list { get; set; } 
        public List<post_list> post_list { get; set; } 
        public List<quotationPO_list> quotationPO_list { get; set; }
        public List<GetTandCDropdown> GetTermsandConditions { get; set; }
        public List<GetTermDropdown> terms_list { get; set; }
        public double grand_total { get; set; }
        public double grandtotal { get; set; }
        public double total_amount { get; set; }
        public double ltotalamount { get; set; }

    }

    public class GetTandCDropdown : result
    {
        public string template_gid { get; set; }
        public string template_name { get; set; }
        public string termsandconditions { get; set; }
        public string payment_terms { get; set; }
    }

    public class GetTermDropdown : result
    {
        public string template_gid { get; set; }
        public string template_name { get; set; }
        public string termsandconditions { get; set; }
        public string payment_terms { get; set; }

    }



    public class quotation_list : result
    {

        public string quotation_gid { get; set; }
        public string quotation_date { get; set; }
        public string quotation_referenceno1 { get; set; }
        public string vendor_companyname { get; set; }
        public string contact { get; set; }
        public string qo_type { get; set; }
        public string created_by { get; set; }
        public string grandtotal_l { get; set; }
        public string quotation_status { get; set; }


    }
    public class GetVendordtl : result
    {

        public string address1 { get; set; }
        public string address2 { get; set; }
        public string contactperson_name { get; set; }
        public string vendor_companyname { get; set; }
        public string contact_telephonenumber { get; set; }
        public string email_id { get; set; }
        public string vendorregister_gid { get; set; }
        public string address_gid { get; set; }


    }
    public class Vendor_list1 : result
    {

        public string vendor_companyname { get; set; }
        public string vendor_gid { get; set; }



    }
    public class GetOnchangecurrency : result
    {

        public string currency_code { get; set; }
        public string exchange_rate { get; set; }



    }
    public class GetCurrencyCodeDropdown : result
    {
        public string currencyexchange_gid { get; set; }
        public string currency_code { get; set; }

    }
    public class summaryprod_list : result
    {
        public string quotationdtl_gid { get; set; }
        public string vendor_gid { get; set; }
        public string product_code { get; set; }
        public string quotation_gid { get; set; }
        public string product_gid { get; set; }
        public string productgroup_gid { get; set; }
        public string customerproduct_code { get; set; }
        public string product_name { get; set; }
        public string product_price { get; set; }
        public string qty_quoted { get; set; }
        public string discount_percentage { get; set; }
        public string discount_amount { get; set; }
        public string productuom_gid { get; set; }
        public string productuom_name { get; set; }
        public string price { get; set; }
        public string selling_price { get; set; }
        public string tax_name { get; set; }
        public string tax_name2 { get; set; }
        public string tax_name3 { get; set; }
        public string slno { get; set; }
        public string product_requireddate { get; set; }
        public string product_requireddateremarks { get; set; }
        public string tax_amount { get; set; }
        public string tax_amount2 { get; set; }
        public string tax_amount3 { get; set; }
        public string productgroup_name { get; set; }
        public string unitprice { get; set; }

        public string display_field { get; set; }
        public string quantity { get; set; }
        public string totalamount { get; set; }
        public string tax_gid { get; set; }
        public string tax_gid2 { get; set; }
        public string tax_gid3 { get; set; }
        public string lstmpquotationgid { get; set; }
        public string lsproductgroup_gid { get; set; }
        public string lsproductgroup { get; set; }
        public string lsproductname_gid { get; set; }
        public string lsproductname { get; set; }
        public string lsuom_gid { get; set; }
        public string lsvendor_gid { get; set; }
        public string lsuom { get; set; }
        public string lsuom_name { get; set; }
        public string lsunitprice { get; set; }
        public string lsrequired_date { get; set; }
        public string lsdiscountpercentage { get; set; }
        public string lsdiscountamount { get; set; }
        public string lstax_name1 { get; set; }
        public string lscustomerproduct_code { get; set; }
        public string lstax_name2 { get; set; }
        public string lstax_name3 { get; set; }
        public string lstaxamount_1 { get; set; }
        public string lstaxamount_2 { get; set; }
        public string lstaxamount_3 { get; set; }
        public string lstotalamount { get; set; }
        public string lssono { get; set; }
        public string lsquantity { get; set; }
        public string lsdisplay_field { get; set; }
        public string lslocalmarginpercentage { get; set; }
        public string lslocalsellingprice { get; set; }

        public string lsreqdate_remarks { get; set; }
    }

    public class summarys_lists : result
    {
        public string vendor_gid { get; set; }
        public string product_code { get; set; }
        public string quotation_gid { get; set; }
        public string product_gid { get; set; }
        public string productgroup_gid { get; set; }
        public string customerproduct_code { get; set; }
        public string product_name { get; set; }
        public string product_price { get; set; }
        public string qty_quoted { get; set; }
        public string discountpercentage { get; set; }
        public string discountamount { get; set; }
        public string productuom_gid { get; set; }
        public string productuom_name { get; set; }
        public string price { get; set; }
        public string selling_price { get; set; }
        public string tax_name { get; set; }
        public string tax_name2 { get; set; }
        public string tax_name3 { get; set; }
        public string slno { get; set; }
        public string product_requireddate { get; set; }
        public string product_requireddateremarks { get; set; }
        public string tax_amount { get; set; }
        public string tax_amount2 { get; set; }
        public string tax_amount3 { get; set; }
        public string productgroup_name { get; set; }
        public string unitprice { get; set; }

        public string display_field { get; set; }
        public string quantity { get; set; }
        public string totalamount { get; set; }
        public string tax_gid { get; set; }
        public string tax_gid2 { get; set; }
        public string tax_gid3 { get; set; }
        public string lstmpquotationgid { get; set; }
        public string lsproductgroup_gid { get; set; }
        public string lsproductgroup { get; set; }
        public string lsproductname_gid { get; set; }
        public string lsproductname { get; set; }
        public string lsuom_gid { get; set; }
        public string lsvendor_gid { get; set; }
        public string lsuom { get; set; }
        public string lsuom_name { get; set; }
        public string lsunitprice { get; set; }
        public string lsrequired_date { get; set; }
        public string lsdiscountpercentage { get; set; }
        public string lsdiscountamount { get; set; }
        public string lstax_name1 { get; set; }
        public string lscustomerproduct_code { get; set; }
        public string lstax_name2 { get; set; }
        public string lstax_name3 { get; set; }
        public string lstaxamount_1 { get; set; }
        public string lstaxamount_2 { get; set; }
        public string lstaxamount_3 { get; set; }
        public string lstotalamount { get; set; }
        public string lssono { get; set; }
        public string lsquantity { get; set; }
        public string lsdisplay_field { get; set; }
        public string lslocalmarginpercentage { get; set; }
        public string lslocalsellingprice { get; set; }
        public string lsreqdate_remarks { get; set; } 
        public string lscurrency_code { get; set; }

    }
    public class tempsummary_list : result
    {
        public string quotationdtl_gid { get; set; }
        public string quotation_gid { get; set; }
        public string customerproduct_code { get; set; }
        public string qty_quoted { get; set; }
        public string product_requireddate { get; set; }
        public string productgroup_name { get; set; }
        public string product_name { get; set; }
        public string product_code { get; set; }
        public string uom_name { get; set; }
        public string product_gid { get; set; }
        public string product_requireddateremarks { get; set; }
        public string tax_gid { get; set; }
        public string tax2_gid { get; set; }
        public string tax3_gid { get; set; }
        public string tax_name { get; set; }
        public string tax_amount { get; set; }
        public string tax_name2 { get; set; }
        public string tax_amount2 { get; set; }
        public string tax_name3 { get; set; }
        public string tax_amount3 { get; set; }
        public string selling_price { get; set; }
        public string slno { get; set; }
        public string discount_amount { get; set; }
        public string discount_percentage { get; set; }
        public string product_price { get; set; }
        public string price { get; set; }
        public string grand_total { get; set; }
        public string grandtotal { get; set; }
    }
    public class post_list : result
    {
        public string quotationdtl_gid { get; set; }
        public string quotation_gid { get; set; }
        public string product_gid { get; set; }
        public string productgroup_gid { get; set; }
        public string customerproduct_code { get; set; }
        public string product_name { get; set; }
        public string product_price { get; set; }
        public string quantity { get; set; }
        public string discountpercentage { get; set; }
        public string discountamount { get; set; }
        public string productuom_gid { get; set; }
        public string productuom_name { get; set; }
        public string totalamount { get; set; }
        public string selling_price { get; set; }
        public string tax_name { get; set; }
        public string tax_name2 { get; set; }
        public string tax_name3 { get; set; }
        public string slno { get; set; }
        public string lscurrency_code { get; set; }
        public string lscurrencyexchange_gid { get; set; }
        public string cuscontact_gid { get; set; }
        public string created_by { get; set; }
        public string product_requireddate { get; set; }
        public string productrequireddate_remarks { get; set; }
        public string tax_amount { get; set; }
        public string tax_amount2 { get; set; }
        public string tax_amount3 { get; set; }
        public string grand_total { get; set; }
        public string qty_quoted { get; set; }
        public string margin_percentage { get; set; }
        public string margin_amount { get; set; }
        public string price { get; set; }
        public string productgroup_name { get; set; }
        public string display_field { get; set; }
        public string uom_gid { get; set; }
        public string uom_name { get; set; }
        public string tax1_gid { get; set; }
        public string tax2_gid { get; set; }
        public string tax3_gid { get; set; }
        public string tax_percentage { get; set; }
        public string tax_percentage2 { get; set; }
        public string tax_percentage3 { get; set; }
        public string vendor_gid { get; set; }
        public string approved_by { get; set; }
        public string branch_name { get; set; }
        public string quotation_date { get; set; }
        public string quotation_remarks { get; set; }
        public string mobile { get; set; }
        public string address1 { get; set; }
        public string email { get; set; }
        public string addoncharge { get; set; }
        public string customer_gid { get; set; }
        public string customer_name { get; set; }
        public string customerbranch_name { get; set; }
        public string customercontact_gid { get; set; }
        public string so_remarks { get; set; }
        public string customercontact_names { get; set; }
        public string quotation_referenceno1 { get; set; }
        public string payment_days { get; set; }
        public string delivery_days { get; set; }
        public string grandtotal { get; set; }
        public string termsandconditions { get; set; }
        public string customer_mobile { get; set; }
        public string customer_address { get; set; }
        public string customer_email { get; set; }
        public string addon_charge { get; set; }
        public string additional_discount { get; set; }
        public string addon_charge_l { get; set; }
        public string additional_discount_l { get; set; }
        public string grandtotal_l { get; set; }
        public string currencyexchange_gid { get; set; }
        public string exchange_rate { get; set; }
        public string currency_code { get; set; }
        public string total_amount { get; set; }
        public string pricingsheet_gid { get; set; }
        public string tax4_gid { get; set; }
        public string user_name { get; set; }
        public string vessel_name { get; set; }
        public string freight_terms { get; set; }
        public string payment_terms { get; set; }
        public string tax_name4 { get; set; }
        public string pricingsheet_refno { get; set; }
        public string roundoff { get; set; }
        public string producttotalamount { get; set; }
        public string freight_charges { get; set; }
        public string buyback_charges { get; set; }
        public string packing_charges { get; set; }
        public string freightcharges { get; set; }
        public string buybackcharges { get; set; }
        public string insurance_charges { get; set; }
        public string template_gid { get; set; }
        public string template_name { get; set; }
        public string lsproductcode { get; set; }
        public string email_id { get; set; }
        public string vendor_address { get; set; }
        public string contactperson_name { get; set; }
        public string shipping_to { get; set; }
        public string currency_gid { get; set; }
        public string quotation_referencenumber { get; set; }
        public string contact_telephonenumber { get; set; }
        public string vendor_companyname { get; set; }
        public string discount_amount { get; set; }
        public string discount_percentage { get; set; }
        public string tax_name1 { get; set; }
        public string taxamount_1 { get; set; }
        public string unitprice { get; set; }
        public string localtaxamount1 { get; set; }
        public string taxamount_3 { get; set; }
        public string taxamount_2 { get; set; }
        public string productname { get; set; }
        public string productcode { get; set; }
        public string productgroup { get; set; }
        public string branch_gid { get; set; }

    }

    public class quotationPO_list : result
    {
        public string quotationdtl_gid { get; set; }
        public string vendor_gid { get; set; }
        public string product_code { get; set; }
        public string quotation_gid { get; set; }
        public string product_gid { get; set; }
        public string productgroup_gid { get; set; }
        public string customerproduct_code { get; set; }
        public string product_name { get; set; }
        public string product_price { get; set; }
        public string qty_quoted { get; set; }
        public string discount_percentage { get; set; }
        public string discount_amount { get; set; }
        public string productuom_gid { get; set; }
        public string productuom_name { get; set; }
        public string price { get; set; }
        public string selling_price { get; set; }
        public string tax_name { get; set; }
        public string tax_name2 { get; set; }
        public string tax_name3 { get; set; }
        public string slno { get; set; }
        public string product_requireddate { get; set; }
        public string product_requireddateremarks { get; set; }
        public string tax_amount { get; set; }
        public string tax_amount2 { get; set; }
        public string tax_amount3 { get; set; }
        public string productgroup_name { get; set; }
        public string unitprice { get; set; }

        public string display_field { get; set; }
        public string quantity { get; set; }
        public string totalamount { get; set; }
        public string tax_gid { get; set; }
        public string tax_gid2 { get; set; }
        public string tax_gid3 { get; set; }

    }


}