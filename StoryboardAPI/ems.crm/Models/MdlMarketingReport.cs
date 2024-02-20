using ems.system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.crm.Models
{
    public class MdlMarketingReport: result
    {
    public List <activitylog_list> activitylog_list { get; set; }
    public List<getenquirychart_List> getenquirychart_List { get; set; }
        public List<EnquiryReportSummary_list> EnquiryReportSummary_list { get; set; }
        public List<EnquiryReportMainSummary_list> EnquiryReportMainSummary_list { get; set; }
        public List<EnquirysubReportSummary_list>EnquirysubReportSummary_list { get; set; }
        public List<getselectenquirychart_List> getselectenquirychart_List { get; set; }
        public List<getCustomerToLeadChartchart_List> getCustomerToLeadChartchart_List { get; set; }

    }

    public class activitylog_list : result
    {
        public string campaign_title { get; set; }
        public string leadbank_name { get; set; }
        public string log_type { get; set; }
        public string log_date { get; set; }
        public string created_date { get; set; }
        public string log_desc { get; set; }
        public string created_name { get; set; }

    }

    public class getenquirychart_List : result
    {
        public string lead_year { get; set; }
        public string lead_monthname { get; set; }
        public string lead_count { get; set; }
    }

    public class EnquiryReportSummary_list : result
    {
        public string month { get; set; }
        public string Month { get; set; }
        public string year { get; set; }
        public string Year { get; set; }
        public string enquirycount { get; set; }
        public string Enquirycount { get; set; }
    }

    public class EnquiryReportMainSummary_list : result
    {
       
        public string Month { get; set; }
        
        public string Year { get; set; }
      
        public string Enquirycount { get; set; }
    }
    public class EnquirysubReportSummary_list: result
    {
        public string enquiry_date { get; set; }
        public string enquiry_refno { get; set; }
        public string customer_name { get; set; }
        public string contact_details { get; set; }
        public string source_name { get; set; }
        public string potorder_value { get; set; }
        public string enquiry_status { get; set; }
        public string user_firstname { get; set; }

    }

    public class getselectenquirychart_List : result
    {
        public string lead_year { get; set; }
        public string lead_monthname { get; set; }
        public string lead_count { get; set; }
    }
    public class getCustomerToLeadChartchart_List : result
    {
        public string month { get; set; }
        public string year { get; set; }
        public string customercount { get; set; }
        public string leadcount { get; set; }

    }
}
