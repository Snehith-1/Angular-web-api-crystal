using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ems.pmr.Models
{
    public class MdlPmrTrnOpeningInvoice  : result
    {
   
        public List<openinginvoice_list> openinginvoice_list { get; set; }
        public List<Getvendor> Getvendor { get; set; }
        public List<GetvendorAddress> GetvendorAddress { get; set; }
        public List<Getbranch> Getbranch { get; set; }
        public List<Getcurrency> Getcurrency { get; set; }
        public List<GetOnChangeCurrency> GetOnChangeCurrency { get; set; }
        public List<OpeningIvoicedtl> OpeningIvoicedtl {  get; set; }

       public string currency_code { get; set; }
       public string exchange_rate { get; set; }

    }

    public class openinginvoice_list : result
    {
        public string invoice_refno { get; set; }
        public string invoice_date { get; set; }
        public string vendor_companyname { get; set; }

        public string invoice_amount { get; set; }

        public string overall_status { get; set; }
        public string vendor_gid { get; set; }
    }
 
    public class Getvendor : result 
    {
        public string vendor_gid { get; set; }
        public string vendor_companyname { get; set; }
        public string vendor_code { get; set; }
        public string contact_telephonenumber { get; set; }
        public string contactperson_name { get; set; }
        
    }
    public class GetvendorAddress : result
    {
        public string address { get; set; }
        public string fax { get; set; }
    }

    public class Getbranch : result
    {
        public string branch_gid { get; set; }
        public string branch_name { get; set; }
        public string branch_code { get; set; }
    }

    public class Getcurrency : result
    {
        public string currencyexchange_gid { get; set; }
        public string currency_code { get; set; }
        public string exchange_rate { get; set; }
    }

    public class GetOnChangeCurrency : result
    {
        public string exchange_rate { get; set; }
        public string currency_code { get; set; }
    }

    public class OpeningIvoicedtl : result
    {
        public string invoice_refno { get; set; }
        public string Vendor_Contact_Person { get; set; }
        public string address { get; set; }
        public string invoice_date { get; set; }
        public string invoice_remarks { get; set; }
        public string currency_code { get; set; }
        public string exchange_rate { get; set; }
        public string Order_Total { get; set; }
        public string received_amount { get; set; }
        public string received_year { get; set; }
        //public string invoice_refno { get; set; }
    }
}