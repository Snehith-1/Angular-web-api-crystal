using System;
using System.Collections.Generic;

namespace ems.einvoice.Models
{
    public class Mdlinvoicesummary : result
    {
        public List<invoicesummary_list> invoicesummary_list { get; set; }
        public List<Gettaxnamedropdown> Gettaxnamedropdown { get; set; }
        public List<invoiceproductsummary_list> invoiceproductsummarylist { get; set; }
        public List<Getproductnamedropdown> Getproductnamedropdown { get; set; }
        public List<GetCustomernamedropdown> GetCustomernamedropdown { get; set; }
        public List<GetBranchnamedropdown> GetBranchnamedropdown { get; set; }
        public List<Getcurrencycodedropdown> Getcurrencycodedropdown { get; set; }
        public List<editinvoiceproductsummary_list> editinvoiceproductsummarylist { get; set; }
        public List<InvoiceData> invoiceData { get; set; }
        public List<EinvoiceAddField> EinvoiceAddField { get; set; }
        public List<Getcustomeronchangedetails> Getcustomeronchangedetails { get; set; }
        public List<Getproductonchangedetails> Getproductonchangedetails { get; set; }
        public List<GetOnChangeCurrency> GetOnChangeCurrency { get; set; }
        public List<GetOnChangeBranch> GetOnChangeBranch { get; set; }
        public List<salesinvoicesummary_list> salesinvoicesummary_list { get; set; }
        public List<SalesInvoicelist> SalesInvoicelist { get; set; }
        public List<salesinvoiceproduct_list> salesinvoiceproduct_list { get; set; }
        public List<salesinvoice_list> salesinvoice_list { get; set; }
        public List<viewinvoice_list> viewinvoice_list { get; set; }
        public List<salesproduct_list> salesproduct_list { get; set; }
        public List<GetTermsDropdown> terms_lists { get; set; }
        public double grand_total { get; set; }
    }
    public class viewinvoice_list : result
    {
        public string invoice_gid { get; set; }
        public string customer_name { get; set; }
        public string customer_branch { get; set; }
        public string customer_contactperson { get; set; }
        public string customer_contactnumber { get; set; }
        public string customer_email { get; set; }
        public string customer_address { get; set; }
        public string branch_name { get; set; }
        public string gst_no { get; set; }
        public string currency_code { get; set; }
        public string exchange_rate { get; set; }
        public string invoice_remarks { get; set; }
        public string invoice_refno { get; set; }
        public string invoice_date { get; set; }
        public string payment_term { get; set; }
        public string payment_date { get; set; }
        public string product_name { get; set; }
        public string productgroup_name { get; set; }
        public string hsn_details { get; set; }
        public string total_amount { get; set; }
        public string additionalcharges_amount { get; set; }
        public string discount_amount { get; set; }
        public string freight_charges { get; set; }
        public string buyback_charges { get; set; }
        public string packing_charges { get; set; }
        public string insurance_charges { get; set; }
        public string roundoff { get; set; }
        public string invoice_amount { get; set; }
        public string termsandconditions { get; set; }
        public string product_code { get; set; }
        public string hsn_code { get; set; }
        public string hsn_description { get; set; }
        public string productuom_name { get; set; }
        public string vendor_price { get; set; }
        public string qty_invoice { get; set; }
        public string product_price { get; set; }
        public string discount_percentage { get; set; }
        public string tax_name { get; set; }
        public string tax_name2 { get; set; }
        public string tax_name3 { get; set; }
        public string tax_amount { get; set; }
        public string tax_amount2 { get; set; }
        public string tax_amount3 { get; set; }
        public string price { get; set; }
        public string price_total { get; set; }

    }
    public class Gettaxnamedropdown : result
    {
        public string tax_name { get; set; }
        public string tax_gid { get; set; }
        public string tax_percentage { get; set; }
    }
    public class Getcustomeronchangedetails : result
    {
        public string customerbranchname { get; set; }
        public string customercontactname { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string address { get; set; }
    }
    public class GetOnChangeCurrency : result
    {
        public string exchange_rate { get; set; }
        public string currency_code { get; set; }
    }
    public class GetOnChangeBranch : result
    {
        public string GST { get; set; }
    }
    public class editinvoiceproductsummary_list : result
    {
        public string product_name { get; set; }
        public string invoicedtl_gid { get; set; }
        public string discount { get; set; }
        public string tax { get; set; }
        public string hsn { get; set; }
        public string product_code { get; set; }
        public string productgroup_name { get; set; }
        public string customerproduct_code { get; set; }
        public string uom_name { get; set; }
        public string qty_invoice { get; set; }
        public string product_price { get; set; }
        public string discount_amount { get; set; }
        public string selling_price { get; set; }
        public string product_total { get; set; }
        public string tax_name { get; set; }
    }
    public class Getproductonchangedetails : result
    {
        public string productgroup_name { get; set; }
        public string product_code { get; set; }
        public string hsn_code { get; set; }
        public string hsn_description { get; set; }
        public string productuom_name { get; set; }
        public string product_price { get; set; }
    }
    public class EinvoiceAddField : result
    {
        public string invoice_date { get; set; }
        public string invoice_refno { get; set; }
        public string invoicerefno { get; set; }
        public string paymentterms { get; set; }
        public string customeremail { get; set; }
        public string paymentduedate { get; set; }
        public string invoice_gid { get; set; }
        public string documentDetails_payment_term { get; set; }
        public string documentDetails_due_date { get; set; }
        public string customer_contactperson { get; set; }
        public string sellercontact_no { get; set; }
        public string selleremail_address { get; set; }
        public string sellerDetails_Gstin { get; set; }
        public string sellerDetails_LglNm { get; set; }
        public string sellerDetails_TrdNm { get; set; }
        public string sellerDetails_Address { get; set; }
        public string sellerDetails_Loc { get; set; }
        public int sellerDetails_Pin { get; set; }
        public string sellerDetails_cont_num { get; set; }
        public string roundoff { get; set; }
        public string frieghtcharges { get; set; }
        public string buybackcharges { get; set; }
        public string forwardingCharges { get; set; }
        public string insurancecharges { get; set; }
        public string sellerDetails_Stcd { get; set; }
        public string sellerDetails_email { get; set; }
        public string transactionDetails_TaxSch { get; set; }
        public string transactionDetails_ecomm_gstin { get; set; }
        public string transactionDetails_SupTyp { get; set; }
        public string transactionDetails_RegRev { get; set; }
        public string transactionDetails_IgstOnIntra { get; set; }
        public string transactionDetails_trans_category { get; set; }
        public string transactionDetails_reverse_charge { get; set; }
        public string transactionDetails_tranaction_type { get; set; }
        public string documentDetails_Typ { get; set; }
        public string documentDetails_No { get; set; }
        public string documentDetails_Dt { get; set; }
        public string buyerDetails_Gstin { get; set; }
        public string buyerDetails_comp_name { get; set; }
        public string buyerDetails_cont_num { get; set; }
        public string buyerDetails_LglNm { get; set; }
        public string buyerDetails_TrdNm { get; set; }
        public string buyerDetails_Pos { get; set; }
        public string buyerDetails_cont_person { get; set; }
        public string buyerDetails_email { get; set; }
        public string buyerDetails_Address { get; set; }
        public string buyerDetails_Loc { get; set; }
        public int buyerDetails_Pin { get; set; }
        public int buyerDetails_remarks { get; set; }
        public string buyerDetails_Stcd { get; set; }
        public string dispatchDetails_Nm { get; set; }
        public string dispatchDetails_Address { get; set; }
        public string dispatchDetails_Loc { get; set; }
        public int dispatchDetails_Pin { get; set; }
        public string dispatchDetails_Stcd { get; set; }
        public string shipDetails_Gstin { get; set; }
        public string shipDetails_LglNm { get; set; }
        public string shipDetails_TrdNm { get; set; }
        public string shipDetails_Address { get; set; }
        public string shipDetails_Loc { get; set; }
        public int shipDetails_Pin { get; set; }
        public string shipDetails_Stcd { get; set; }
        public string item_SlNo { get; set; }
        public string item_PrdDesc { get; set; }
        public string item_IsServc { get; set; }
        public string item_HsnCd { get; set; }
        public double item_Qty { get; set; }
        public string item_Unit { get; set; }
        public double item_UnitPrice { get; set; }
        public double item_TotAmt { get; set; }
        public double item_Discount { get; set; }
        public double item_AssAmt { get; set; }
        public double item_GstRt { get; set; }
        public double item_IgstAmt { get; set; }
        public double item_SgstAmt { get; set; }
        public double item_CgstAmt { get; set; }
        public double item_TotItemVal { get; set; }
        public string lblroundoff { get; set; }
        public string remarks { get; set; }
        public string vendorEmail { get; set; }
        public string vendorPhoneNo { get; set; }
        public string addoncharges { get; set; }
        public string additionaldiscount { get; set; }
        public string discount_amount { get; set; }
        public string extracharges { get; set; }
        public string extradiscount { get; set; }
        public double grandtotal_amountvalue { get; set; }
        public string currencycode_additionaldiscount { get; set; }
        public string currencycode_addon { get; set; }
        public string currencycode_total { get; set; }
        public string currencycode_grandtotal { get; set; }
        public string tax_code { get; set; }
        public string finaltotal_code { get; set; }
        public string extraadditional { get; set; }
        public string cbodiscountamount { get; set; }
        public string additionalamountvalue { get; set; }
        public string extradiscountamountvalue { get; set; }
        public string overalltaxt { get; set; }
        public string finaltotal { get; set; }
        public string cbo_overallTax { get; set; }
        public string producttotalprice { get; set; }
        public string hdnexchange_rate { get; set; }
        public string freightcharges { get; set; }
        public string buyback_charges { get; set; }
        public string packing_charges { get; set; }
        public string insurance_charges { get; set; }
        public string freight_currency_code { get; set; }
        public string buyback_currency_code { get; set; }
        public string packing_currency_code { get; set; }
        public string insurance_currency_code { get; set; }
        public string extracharges_currencycode { get; set; }
        public string extradiscount_currencycode { get; set; }
    }   
    public class InvoiceData
    {
        public string Version { get; set; }
        public TransactionDetails TranDtls { get; set; }
        public DocumentDetails DocDtls { get; set; }
        public SellerDetails SellerDtls { get; set; }
        public BuyerDetails BuyerDtls { get; set; }
        public DispatchDetails DispDtls { get; set; }
        public ShipDetails ShipDtls { get; set; }
        public List<ItemDetails> ItemList { get; set; }
        public ValueDetails ValDtls { get; set; }
    }
    public class TransactionDetails
    {
        public string TaxSch { get; set; }
        public string SupTyp { get; set; }
        public string RegRev { get; set; }
        public string IgstOnIntra { get; set; }
    }
    public class ValueDetails
    {
        public double AssVal { get; set; }
        public double IgstVal { get; set; }
        public double SgstVal { get; set; }
        public double CgstVal { get; set; }
        public double Discount { get; set; }
        public double OthChrg { get; set; }
        public double RndOffAmt { get; set; }
        public double TotInvVal { get; set; }
    }
    public class DocumentDetails
    {
        public string Typ { get; set; }
        public string No { get; set; }
        public string Dt { get; set; }
    }
    public class SellerDetails
    {
        public string Gstin { get; set; }
        public string LglNm { get; set; }
        public string TrdNm { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Loc { get; set; }
        public int Pin { get; set; }
        public string Stcd { get; set; }
        public string Ph { get; set; }
        public string Em { get; set; }
    }
    public class BuyerDetails
    {
        public string Gstin { get; set; }
        public string LglNm { get; set; }
        public string TrdNm { get; set; }
        public string Pos { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Loc { get; set; }
        public int Pin { get; set; }
        public string Stcd { get; set; }
        public string Ph { get; set; }
        public string Em { get; set; }
    }
    public class DispatchDetails
    {
        public string Nm { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Loc { get; set; }
        public int Pin { get; set; }
        public string Stcd { get; set; }
    }
    public class ShipDetails
    {
        public string Gstin { get; set; }
        public string LglNm { get; set; }
        public string TrdNm { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Loc { get; set; }
        public int Pin { get; set; }
        public string Stcd { get; set; }
    }
    public class ItemDetails
    {
        public string SlNo { get; set; }
        public string PrdDesc { get; set; }
        public string IsServc { get; set; }
        public string HsnCd { get; set; }
        public double Qty { get; set; }
        public string Unit { get; set; }
        public double UnitPrice { get; set; }
        public double TotAmt { get; set; }
        public double Discount { get; set; }
        public double AssAmt { get; set; }
        public double GstRt { get; set; }
        public double IgstAmt { get; set; }
        public double SgstAmt { get; set; }
        public double CgstAmt { get; set; }
        public double TotItemVal { get; set; }
    }
    public class ApiResponse
    {
        public bool success { get; set; }
        public string qr_code { get; set; }
        public string request_id { get; set; }
        public string errorMessage { get; set; }
    }
    public class ResponseData
    {
        public Boolean success { get; set; }
        public string message { get; set; }
        public ResultItem result { get; set; }
    }
    public class qrDownloadResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class AuthResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string scope { get; set; }
        public string jti { get; set; }
    }
    public class ResultItem
    {
        public string AckNo { get; set; }
        public DateTime AckDt { get; set; }
        public string Irn { get; set; }
        public string SignedInvoice { get; set; }
        public string SignedQRCode { get; set; }
        public string Status { get; set; }
        public string EwbNo { get; set; }
        public string EwbDt { get; set; }
        public string EwbValidTill { get; set; }
        public string Remarks { get; set; }
    }
    public class qr_request
    {
        public string data { get; set; }
    }
    public class invoiceproductsummary_list : result
    {
        public string product_name { get; set; }
        public string invoicedtl_gid { get; set; }
        public string discount { get; set; }
        public string tax { get; set; }
        public string hsn { get; set; }
        public string product_code { get; set; }
        public string productgroup_name { get; set; }
        public string customerproduct_code { get; set; }
        public string uom_name { get; set; }
        public string qty_invoice { get; set; }
        public string product_price { get; set; }
        public string discount_amount { get; set; }
        public string selling_price { get; set; }
        public string product_total { get; set; }
        public string tax_name { get; set; }
    }
    public class invoicesummary_list : result
    {
        public string so_referencenumber { get; set; }
        public string irn { get; set; }
        public string invoice_gid { get; set; }
        public DateTime invoice_date { get; set; }
        public string invoice_refno { get; set; }
        public string customer_name { get; set; }
        public string invoice_reference { get; set; }
        public string invoice_from { get; set; }
        public string invoice_status { get; set; }
        public string mail_status { get; set; }
        public string invoice_amount { get; set; }
        public string customer_contactperson { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
    }
    public class invoiceproductlist : result
    {
        public double quantity { get; set; }
        public string mrp { get; set; }
        public string productgid { get; set; }
        public string hsncode { get; set; }
        public string totalamount { get; set; }
        public string hsndescription { get; set; }
        public double unitprice { get; set; }
        public double discountpercentage { get; set; }
        public string discountamount { get; set; }
        public string taxamount1 { get; set; }
        public string totalamount1 { get; set; }
        public string productuom_gid { get; set; }
        public string productuom_name { get; set; }
        public string taxamount2 { get; set; }        
        public string taxname1 { get; set; }
        public string taxname2 { get; set; }        
        public string productdescription { get; set; }
        public string tax_gid1 { get; set; }
        public string tax_gid2 { get; set; }        
        public string product_name { get; set; }
        public string productgroup_gid { get; set; }
        public string productgroup_name { get; set; }
        public string costprice { get; set; }
        public string productcode { get; set; }
        public string customerproductcode { get; set; }
        public string taxpercentage1 { get; set; }
        public string taxpercentage2 { get; set; }        
        public string vendorprice { get; set; }
    }
    public class invoicelist : result
    {
        public string customergid { get; set; }
        public string invoice_gid { get; set; }
        public string grandtotal { get; set; }
        public string currencygid { get; set; }
        public string invoiceref_no { get; set; }
        public string invoicedate { get; set; }
        public string paymentterm { get; set; }
        public DateTime duedate { get; set; }
        public string customercontactperson { get; set; }
        public string customercontactnumber { get; set; }
        public string customeraddress { get; set; }
        public string customeremailaddress { get; set; }
        public string invoicetotalamount { get; set; }
        public string GrandTotalAmount { get; set; }
        public string invoicediscountamount { get; set; }
        public string addoncharges { get; set; }
        public string remarks { get; set; }
        public string termsandconditions { get; set; }
        public string exchangerate { get; set; }
        public string branchgid { get; set; }
        public string roundoff { get; set; }
        public string frieghtcharges { get; set; }
        public string buybackcharges { get; set; }
        public string forwardingCharges { get; set; }
        public string insurancecharges { get; set; }
    }
    public class einvoicelist : result
    {
        public string invoice_gid { get; set; }
        public string customer_email { get; set; }       
        public string company_name { get; set; }
        public string postalcode { get; set; }
        public string gst_no { get; set; }
        public string buyerDetails_Pos { get; set; }
        public string sellercontact_number { get; set; }
        public string dispatchDetails_Nm { get; set; }
        public string dispatchDetails_Address { get; set; }
        public string dispatchDetails_Loc { get; set; }
        public string dispatchDetails_Pin { get; set; }
        public string dispatchDetails_Stcd { get; set; }
        public string shipDetails_Gstin { get; set; }
        public string shipDetails_LglNm { get; set; }
        public string shipDetails_TrdNm { get; set; }
        public string shipDetails_Address { get; set; }
        public string shipDetails_Loc { get; set; }
        public string shipDetails_Pin { get; set; }
        public string shipDetails_Stcd { get; set; }
        public string invoice_refno { get; set; }
        public string invoice_date { get; set; }
        public string payment_term { get; set; }
        public string payment_date { get; set; }
        public string customer_address { get; set; }
        public string customer_name { get; set; }
        public string mobile { get; set; }
        public string gst_number { get; set; }
        public string customer_contactperson { get; set; }
        public string customer_pin { get; set; }
        public string customer_state { get; set; }
        public string customer_city { get; set; }
        public string company_address { get; set; }
        public string state { get; set; }
        public string company_mail { get; set; }
        public string city { get; set; }
        public string remarks { get; set; }
        public string termsandconditions { get; set; }
        public string exchangerate { get; set; }
        public string branch { get; set; }
        public double netamount { get; set; }
        public string addoncharges { get; set; }
        public string invoicediscountamount { get; set; }
        public string frieghtcharges { get; set; }
        public string buybackcharges { get; set; }
        public double packing_charges { get; set; }
        public string forwardingCharges { get; set; }
        public string insurancecharges { get; set; }
        public string roundoff { get; set; }
        public string invoice_amount { get; set; }
    }
    public class Getproductnamedropdown : result
    {
        public string productname { get; set; }
        public string productcode { get; set; }
        public string productgid { get; set; }
    }
    public class GetCustomernamedropdown : result
    {
        public string customergid { get; set; }
        public string customername { get; set; }
        public string leadbank_gid { get; set; }
        public string leadbank_name { get; set; }
    }
    public class Getcurrencycodedropdown : result
    {
        public string currency_code { get; set; }
        public string currencyexchange_gid { get; set; }
    }
    public class GetBranchnamedropdown : result
    {
        public string branch_name { get; set; }
        public string branch_gid { get; set; }
    }
    public class GetIRNDetails : result
    {
        public string invoice_refno { get; set; }
        public string customer_name { get; set; }
        public string invoice_amount { get; set; }
        public string invoice_date { get; set; }
        public string irn { get; set; }
    }
    public class creditnote_list : result
    {
        public string invoice_refno_creditnote { get; set; }
        public string invoice_date_creditnote { get; set; }
        public string invoice_custname_creditnote { get; set; }
        public string invoice_amount_creditnote { get; set; }
        public string irn_no_creditnote { get; set; }
        public string invoice_gid { get; set; }
    }
    public class cancelIrn_list : result
    {
        public string invoice_refno_cancel { get; set; }
        public string invoice_date_cancel { get; set; }
        public string invoice_custname_cancel { get; set; }
        public string invoice_amount_cancel { get; set; }
        public string irn_no_cancel { get; set; }
        public string invoice_gid { get; set; }
    }
    public class Cancelirn_ResultItem
    {
        public string Irn { get; set; }
        public DateTime CancelDate { get; set; }
    }
    public class ResponseDatacredit
    {
        public bool success { get; set; }
        public string message { get; set; }
        public ResultItem result { get; set; }
    }
    public class CancelIRN_ResponseData
    {
        public Boolean success { get; set; }
        public string message { get; set; }
        public Cancelirn_ResultItem result { get; set; }
    }
    public class CancelIRN
    {
        public string irn { get; set; }
        public string cnlrsn { get; set; }
        public string cnlrem { get; set; }
    }

    public class salesinvoicesummary_list : result
    {
        public DateTime directorder_date { get; set; }
        public string directorder_gid { get; set; }
        public string so_referenceno1 { get; set; }
        public string customer_name { get; set; }
        public string mobile { get; set; }
        public string order_type { get; set; }
        public string currency_code { get; set; }
        public string grandtotal { get; set; }
        public string invoice_amout { get; set; }
        public string outstanding_amount { get; set; }
        public string status { get; set; }
        public string leadbank_gid { get; set; }
        public string lead2campaign_gid { get; set; }
        public string customer_gid { get; set; }

    }

    public class SalesInvoicelist : result
    {
        public string serviceorder_gid { get; set; }
        public string tax_amount { get; set; }
        public string so_reference { get; set; }
        public string serviceorder_date { get; set; }
        public string addon_amount { get; set; }
        public string grand_total { get; set; }
        public string customer_name { get; set; }
        public string customer_address { get; set; }
        public string customer_mobile { get; set; }
        public string branch_name { get; set; }
        public string email { get; set; }
        public string customercontact_name { get; set; }
        public string discount_amount { get; set; }
        public string customer_gid { get; set; }
        public string customer_code { get; set; }
        public string termsandconditions { get; set; }
        public string gst_amount { get; set; }
        public string freight_charges { get; set; }
        public string packing_charges { get; set; }
        public string buyback_charges { get; set; }
        public string insurance_charges { get; set; }
        public string invoice_no { get; set; }
        public string description { get; set; }
        public string currency_code { get; set; }
        public string exchange_rate { get; set; }
        public string invoice_amount { get; set; }
        public string invoiceaccounting_branch { get; set; }
    }

    public class salesinvoiceproduct_list : result
    {
        public string customerproduct_code { get; set; }
        public string product_name { get; set; }
        public string total_amount { get; set; }
        public string description { get; set; }
        public string qty_quoted { get; set; }
        public string vendoramount { get; set; }
        public string discount_amount { get; set; }
        public string amount1 { get; set; }
        public string product_price { get; set; }
        public string final_amount { get; set; }
        public string tax { get; set; }

        public string tax1 { get; set; }


    }

    public class salesinvoice_list : result
    {
        public DateTime invoice_date { get; set; }
        public string invoiceaccounting_branch { get; set; }
        public string branch_name { get; set; }
        public string invoiceaccounting_payterm { get; set; }
        public DateTime invoiceaccounting_orderdate { get; set; }
        public DateTime invoiceaccounting_duedate { get; set; }
        public string invoiceaccounting_orderrefno { get; set; }
        public string invoiceaccounting_salestype { get; set; }
        public string invoiceaccounting_contactperson { get; set; }
        public string invoiceaccounting_customername { get; set; }
        public string invoiceaccounting_contactnumber { get; set; }
        public string invoiceaccounting_customeraddress { get; set; }
        public string invoiceaccounting_email { get; set; }
        public string invoiceaccounting_grandtotal { get; set; }
        public string invoice_refno { get; set; }
        public string invoiceaccounting_refno { get; set; }
        public string employee_gid { get; set; }
        public string customer_gid { get; set; }
        public string invoiceaccounting_currency { get; set; }
        public string discount_amount { get; set; }
        public string invoiceaccounting_addonamount { get; set; }
        public string invoiceaccounting_remarks { get; set; }
        public string invoiceaccounting_termsandconditions { get; set; }
        public string invoiceaccounting_exchangerate { get; set; }
        public string invoiceaccounting_roundoff { get; set; }
        public string invoiceaccounting_insurancecharges { get; set; }
        public string invoiceaccounting_freightcharges { get; set; }
        public string invoiceaccounting_packingcharges { get; set; }
        public string currency_gid { get; set; }
        public string invoiceaccounting_salesorder_gid { get; set; }
    }

    public class salesproduct_list : result
    {
        public string product_code { get; set; }
        public string product_name { get; set; }
        public string qty_quoted { get; set; }
    }

    public class GetTermsDropdown : result
    {
        public string template_gid { get; set; }
        public string template_name { get; set; }
        public string template_content { get; set; }

    }

}