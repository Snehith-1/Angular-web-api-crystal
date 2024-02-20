using System.Collections.Generic;

namespace ems.sales.Models
{
    public class MdlSmrMstSalesteamSummary:result
    {
        public List<salesteam_list> salesteam_list { get; set; }
        public List<salesteamgrid_list> salesteamgrid_list { get; set; }
        public List<Getemployee> Getemployee { get; set; }
        public List<editsalesteam_list> editsalesteam_list { get; set; }
        public List<GetUnassignedEmplist> GetUnassignedEmplist { get; set; }
        public List<GetAssignedEmplist> GetAssignedEmplist { get; set; }
        public List<GetUnassignedlist> GetUnassignedlist { get; set; }
        public List<campaignassignemp_list> campaignassignemp_list { get; set; }
        public List<GetUnassignedManagerlist> GetUnassignedManagerlist { get; set; }
        public List<GetAssignedManagerlist> GetAssignedManagerlist { get; set; }
        public List<GetUnassignedManager> GetUnassignedManager { get; set; }
        public List<campaignassignmanager_list> campaignassignmanager_list { get; set; }
    }
    public class editsalesteam_list : result
    {
        public string campaign_gid { get; set; }
        public string campaign_title { get; set; }
        public string campaign_location { get; set; }
        public string campaign_description { get; set; }
        public string campaign_mailid { get; set; }
    }
    public class salesteam_list : result
    {
        public string branch_name { get; set; }
        public string assigned_total { get; set; }
        public string campaign_description { get; set; }
        public string campaign_gid { get; set; }
        public string campaign_title { get; set; }
        public string team_name { get; set; }
        public string campaign_location { get; set; }
        public string email { get; set; }
        public string employee_gid { get; set; }
    }
    public class salesteamgrid_list : result
    {
        public string total { get; set; }
        public string drop_status { get; set; }
        public string prospect { get; set; }
        public string campaign_gid { get; set; }
        public string completed { get; set; }
        public string potential { get; set; }
        public string user { get; set; }
        public string employee_gid { get; set; }
        public string team_name { get; set; }
        public string description { get; set; }
        public string branch_name { get; set; }
        public string branch { get; set; }
        public string team_manager { get; set; }
        public string employee_name { get; set; }
        public string mail_id { get; set; }
    }
    public class Getemployee : result
    {
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
    }
    public class GetUnassignedEmplist : result
    {
        public string campaign_gid { get; set; }
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
    }
    public class GetAssignedEmplist : result
    {
        public string campaign_gid { get; set; }
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
    }
    public class GetUnassignedlist : result
    {
        public string campaign_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class campaignassignemp_list : result
    {
        public string campaign_gid { get; set; }

        public campaignassignemp[] campaignassignemp;
    }
    public class campaignassignemp : result
    {
        public string _id { get; set; }
        public string _name { get; set; }
        public string _key1 { get; set; }
        public string _key3 { get; set; }
    }
    public class GetUnassignedManagerlist : result
    {
        public string campaign_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class GetAssignedManagerlist : result
    {
        public string campaign_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class GetUnassignedManager : result
    {
        public string campaign_gid { get; set; }        
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class campaignassignmanager_list : result
    {
        public string campaign_gid { get; set; }

        public campaignassignmanager[] campaignassignmanager;
    }
    public class campaignassignmanager : result
    {
        public string _id { get; set; }
        public string _name { get; set; }
        public string _key1 { get; set; }
        public string _key3 { get; set; }
    }
}