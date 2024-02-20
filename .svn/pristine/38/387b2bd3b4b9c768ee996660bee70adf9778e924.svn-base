using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.sales.Models
{
    public class Mdlcustomerledgerdetail : result
    {
        public List<customerledgersalesorder_list> customerledgersalesorder_list { get; set; }
        public List<customerledgersalesorderdetail_list> customerledgersalesorderdetail_list { get; set; }
        public List<customerledgerinvoice_list> customerledgerinvoice_list { get; set; }
        public List<customerledgerinvoicedetail_list> customerledgerinvoicedetail_list { get; set; }
        public List<customerledgerpayment_list> customerledgerpayment_list { get; set; }
        public List<customerledgerpaymentdetail_list> customerledgerpaymentdetail_list { get; set; }
        public List<customerledgeroutstanding_list> customerledgeroutstanding_list { get; set; }
        public List<customerledgercount_list> customerledgercount_list { get; set; }
    }
    public class customerledgersalesorder_list : result
    {
        public string customer_gid { get; set; }
        public string salesorder_gid { get; set; }
        public string so_date { get; set; }
        public string so_referenceno1 { get; set; }
        public string customer_name { get; set; }
        public string contact_details { get; set; }
        public string type { get; set; }
        public string amount { get; set; }
        public string created_by { get; set; }
        public string so_status { get; set; }


    }
    public class customerledgersalesorderdetail_list : result
    {
        public string product { get; set; }
        public string qty_quoted { get; set; }
    }
    public class customerledgerinvoice_list : result
    {
        public string invoice_date { get; set; }
        public string customer_gid { get; set; }
        public string invoice_referenceno1 { get; set; }
        public string customer_name { get; set; }
        public string contact_details { get; set; }
        public string type { get; set; }
        public string amount { get; set; }
        public string invoice_status { get; set; }
        public string invoice_gid {  get; set; }
    }
    public class customerledgerinvoicedetail_list : result 
    { 
        public string product { get; set; }
        public string qty_invoice { get; set; }

    }
    public class customerledgerpayment_list : result
    {
        public string payment_date { get; set; }
        public string payment_gid {  get; set; }
        public string customer_gid { get; set; }

        public string payment_referenceno1 { get; set; }
        public string customer_name { get; set; }
        public string contact_details { get; set; }
        public string type { get; set; }
        public string amount { get; set; }
        public string payment_status { get; set; }
    }
    public class customerledgerpaymentdetail_list : result
    {
        public string product { get; set; }
        public string qty_invoice { get; set; }

    }
    public class customerledgeroutstanding_list : result
    {
        public string invoice_date { get; set; }
        public string invoice_referenceno1 { get; set; }
        public string customer_details { get; set; }
        public string invoice_amount { get; set; }
        public string paid_amount { get; set; }
        public string outstanding_amount { get; set; }
        public string due_date { get; set; }
        public string days_expired { get; set; }
        public string outstanding_status { get; set; }
        public string customer_gid {  get; set; }
    }
    public class customerledgercount_list : result
    {
        public string customer_gid { get; set; }
        public string salesorder_count { get; set; }
        public string invoice_count { get; set; }
        public string payment_count { get; set; }
        public string outstanding_count { get; set; }
    }
}