using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.crm.Models
{
    public class MdlSmsCampaign
    {
        public List<smscampaign_list> smscampaign_list { get; set; }
        public List<smspostcampaign_list> smspostcampaign_list { get; set; }
        public List<smsleadcustomerdetails_list> smsleadcustomerdetails_list { get; set; }
        public List<smscampaigncount_list> smscampaigncount_list { get; set; }
        


    }
    //code by snehith///////////////
    public class smscampaigncount_list : result
    {
        public string campaign_count { get; set; }
    }
        public class smssendtolead : result
    {

        public string id  { get; set; }
   

        public smsleadcustomerdetails_list[] smsleadcustomerdetails_list;
    }
    public class smsleadcustomerdetails_list : result
    {
        public string id { get; set; }
       public string leadbank_gid { get; set; }
        public string email { get; set; }
        public string created_date { get; set; }
        public string default_phone { get; set; }
        public string customer_type { get; set; }
        public string names { get; set; }
        public string address1 { get; set; }
        public string date { get; set; }
        public string to_mail { get; set; }
        public string status_delivery { get; set; }
        public string status_open { get; set; }
        public string status_click { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }



       


    }
   
    public class smspostcampaign_list : result
    {
        public string campaign_title { get; set; }
        public string campaign_message { get; set; }
        public string campaign_titleedit { get; set; }
        public string campaign_messageedit { get; set; }
        public string id { get; set; }
    }
    public class smscampaign_list : result
    {
        public string id { get; set; }
        public string created_by { get; set; }
        public string campagin_title { get; set; }
        public string created_date { get; set; }
        public string campagin_message { get; set; }
    }
}