using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.crm.Models
{
    public class MdlCrmDashboard : result
    {
        public List<getDashboardCount_List> getDashboardCount_List { get; set; }
        public List<getDashboardQuotationAmt_List> getDashboardQuotationAmt_List { get; set; }
        public List<getleadbasedonemployee_List> getleadbasedonemployee_List { get; set; }
        public List<socialmedialeadcount> socialmedialeadcount { get; set; }

    }
    public class getDashboardCount_List : result
    {
        public string mycalls_count { get; set; }
        public string myleads_count { get; set; }
        public string myappointments_count { get; set; }
        public string assignvisit_count { get; set; }
        public string completedvisit_count { get; set; }
        public string shared_proposal { get; set; }
        public string completedorder_count { get; set; }
        public string totalorder_count { get; set; }
    }
    public class getDashboardQuotationAmt_List : result
    {
        public string year { get; set; }
        public string month_name { get; set; }
        public string total_amount { get; set; }
    }
    public class getleadbasedonemployee_List : result
    {
        public string lead_year { get; set; }
        public string lead_monthname { get; set; }
        public string lead_count { get; set; }
    }
    public class socialmedialeadcount
    {
        public string whatsapp_count { get; set; }
        public string mail_count { get; set; }
        public string shopify_count { get; set; }
    }
}