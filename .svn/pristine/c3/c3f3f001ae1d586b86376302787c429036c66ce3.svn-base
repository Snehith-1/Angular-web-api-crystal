using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoryboardAPI.Models;

namespace ems.sales.Models
{
    public class MdlSmrRptQuotationReport : result
    {

        public List<GetQuotationForLastSixMonths_List> GetQuotationForLastSixMonths_List { get; set; }
        public List<GetMonthwiseQuotationReport_List> GetMonthwiseQuotationReport_List { get; set; }
        public List<GetQuotationSummary> GetQuotationSummary { get; set; }

        public string from_date { get; set; }
        public string to_date { get; set; }
        public string month_wise { get; set; }
        public string total_invoice { get; set; }
        public string total_payment { get; set; }
    }
    public class GetQuotationForLastSixMonths_List : result
    {
        public string month { get; set; }
        public string year { get; set; }
        public string amount { get; set; }
        public string quotationcount { get; set; }
        public string quotation_date { get; set; }
        public string customer_name { get; set; }
        public string contact_details { get; set; }
        public string quotation_status { get; set; }
        public string created_by { get; set; }
        public string grandtotal_l { get; set; }
       


    }
    public class GetMonthwiseQuotationReport_List : result
    {
        public string month_wise { get; set; }
        public string so_total { get; set; }
        public string total { get; set; }
        public string total_invoice { get; set; }
        public string total_payment { get; set; }
    }

    public class GetQuotationSummary : result
    {

       
        public string quotation_date { get; set; }
        public string customer_name { get; set; }
        public string contact_details { get; set; }
        public string quotation_status { get; set; }
        public string created_by { get; set; }
        public string grandtotal_l { get; set; }

    }
}
