using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoryboardAPI.Models;

namespace ems.sales.Models
{
    public class MdlSmrRptEnquiryReport : result
    {
        public List<GetEnquiryForLastSixMonths_List> GetEnquiryForLastSixMonths_List { get; set; }
        public List<GetEnquiryDetailSummary> GetEnquiryDetailSummary { get; set; }

        public List<GetMonthwiseOrderReport_List> GetMonthwiseOrderReport_List { get; set; }
       
      
        public string month_wise { get; set; }
        public string total_invoice { get; set; }
        public string total_payment { get; set; }
    }
    public class GetEnquiryForLastSixMonths_List : result
    {
        public string month { get; set; }
        public string year { get; set; }
        public string amount { get; set; }
        public string ordercount { get; set; }
        public string salesorder_date { get; set; }        
        public string contact_details { get; set; }
        public string salesorder_status { get; set; }
        public string salesperson_name { get; set; }
        public string grandtotal_l { get; set; }
        public string customer_name { get; set; }
        
    }
    public class GetEnquiryDetailSummary : result
    {
        public string salesorder_date { get; set; }
        public string customer_name { get; set; }
        public string contact_details { get; set; }
        public string salesorder_status { get; set; }
        public string salesperson_name { get; set; }
        public string grandtotal_l { get; set; }
        public string enquiry_status {  get; set; }
        public string potential_value { get; set; }
        public string enquiry_refno { get; set; }



    }
    public class GetMonthwiseEnquiryReport_List : result
    {
        public string month_wise { get; set; }
        public string so_total { get; set; }
        public string total { get; set; }
        public string total_invoice { get; set; }
        public string total_payment { get; set; }
    }
    public class GetEnquirywiseEnquiryReport_List : result
    {
        public string month_wise { get; set; }
        public string salesorder_gid { get; set; }
        public string so_total { get; set; }
        public string total { get; set; }
        public string total_invoice { get; set; }
        public string total_payment { get; set; }
    }

  

}