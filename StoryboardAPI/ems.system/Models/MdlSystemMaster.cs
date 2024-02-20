using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.system.Models
{
    public class MdlSystemMaster : result
    {
        public string user2oneapi_gid { get; set; }
        public string externalsystem_name { get; set; }
        public string externalsystem_ownername { get; set; }
        public string externalsystem_gid { get; set; }
        public string externaluser_code { get; set; }
        public string externaluser_password { get; set; }
        public string externaluser_status { get; set; }
        public string employee_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public List<master_list> master_list { get; set; }
        public List<location_list> location_list { get; set; }
        public List<employee_list> employee_list { get; set; }
        public List<externaluser_lists> externaluser_list { get; set; }
    }
    public class exportexcel : result
    {
        public string lspath { get; set; }
        public string lsname { get; set; }
    }
    public class location_list
    {
        public string baselocation_gid { get; set; }
        public string baselocation_name { get; set; }
    }
    public class master_list
    {
        public string remarks { get; set; }
        public string bloodgroup_gid { get; set; }
        public string bloodgroup_name { get; set; }
        public string physicalstatus_gid { get; set; }
        public string physicalstatus_name { get; set; }
        public string baselocation_gid { get; set; }
        public string baselocation_name { get; set; }
        public string calendargroup_gid { get; set; }
        public string calendargroup_name { get; set; }
        public string subfunction_gid { get; set; }
        public string subfunction_name { get; set; }
        public string salutation_gid { get; set; }
        public string salutation_name { get; set; }
        public string project_gid { get; set; }
        public string project_name { get; set; }
        public string project { get; set; }
        public string task_gid { get; set; }
        public string task_name { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string status { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string deleted_date { get; set; }
        public string deleted_by { get; set; }

        public string cluster_gid { get; set; }
        public string region_gid { get; set; }
        public string zone_gid { get; set; }
        public string regionhead_gid { get; set; }
        public string businesshead_gid { get; set; }
        public string groupbusinesshead_gid { get; set; }
        public string clusterhead_gid { get; set; }
        public string zonalhead_gid { get; set; }
        public string producthead_gid { get; set; }
        public string application_name { get; set; }
        public string hrnotification_gid { get; set; }
        public string hrnotification_code { get; set; }

        public string branch_code { get; set; }
        public string branch_name { get; set; }
        public string branch_prefix { get; set; }
        public string branchmanager_gid { get; set; }
        public string branch_location { get; set; }
        public string branch_gid { get; set; }
        public string api_code { get; set; }
        public string department_manager { get; set; }
        public string department_gid { get; set; }
        public string department_code { get; set; }
        public string department_prefix { get; set; }
        public string department_name { get; set; }

        public string state_gid { get; set; }
        public string state_code { get; set; }
        public string diaplay_order { get; set; }
        public string state_name { get; set; }
    }
    public class master : result
    {
        public string remarks { get; set; }
        public string Status { get; set; }
        public string bloodgroup_gid { get; set; }
        public string bloodgroup_name { get; set; }
        public string baselocation_gid { get; set; }
        public string baselocation_name { get; set; }
        public string physicalstatus_gid { get; set; }
        public string physicalstatus_name { get; set; }
        public string calendargroup_gid { get; set; }
        public string calendargroup_name { get; set; }
        public string subfunction_gid { get; set; }
        public string subfunction_name { get; set; }
        public string salutation_gid { get; set; }
        public string salutation_name { get; set; }
        public string project_gid { get; set; }
        public string project_name { get; set; }
        public string project { get; set; }
        public string task_gid { get; set; }
        public string task_name { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string created_date { get; set; }
        public string created_by { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public char rbo_status { get; set; }
        public char delete_flag { get; set; }
        public string updated_date { get; set; }
        public string updated_by { get; set; }
        public string deleted_date { get; set; }
        public string deleted_by { get; set; }
        public List<master_list> master_list { get; set; }
        public List<employee_list> employee_list { get; set; }
        public string status_bloodgroup { get; set; }
        public string status_physicalstatus { get; set; }
        public string status_baselocation { get; set; }
        public string status_calendargroup { get; set; }
        public string status_clientrole { get; set; }
        public string status_salutation { get; set; }
        public string status_project { get; set; }
        public string hrnotification_gid { get; set; }
        public string application_name { get; set; }

    }

    public class cluster : result
    {
        public string cluster_gid { get; set; }
        public string cluster_name { get; set; }
        public string cluster_status { get; set; }
        public string remarks { get; set; }
        public char rbo_status { get; set; }
        public string pending_count { get; set; }
        public string ocs_pendingcount { get; set; }
        public string agrbyr_pendingcount { get; set; }
        public string agrsupr_pendingcount { get; set; }
        public List<locationlist> locationlist { get; set; }
        public List<cluster_list> cluster_list { get; set; }
        public List<clusterhead_list> clusterhead_list { get; set; }
        public List<zonalhead_list> zonalhead_list { get; set; }
    }
    public class locationlist
    {
        public string baselocation_gid { get; set; }
        public string baselocation_name { get; set; }
        public string cluster2baselocation_gid { get; set; }
    }
    public class cluster_list
    {
        public string cluster_gid { get; set; }
        public string cluster_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string status { get; set; }
        public string region2cluster_gid { get; set; }
        public string baselocation_name { get; set; }
        public string api_code { get; set; }
    }
    public class region : result
    {
        public string region_gid { get; set; }
        public string region_name { get; set; }
        public string region_status { get; set; }
        public string remarks { get; set; }
        public char rbo_status { get; set; }
        public string ocs_pendingcount { get; set; }
        public string agrbyr_pendingcount { get; set; }
        public string agrsupr_pendingcount { get; set; }
        public List<region_list> region_list { get; set; }
        public List<cluster_list> cluster_list { get; set; }
        public List<regionhead_list> regionhead_list { get; set; }
        public List<clusterhead_list> clusterhead_list { get; set; }
        public List<zonalhead_list> zonalhead_list { get; set; }
    }

    public class region_list
    {
        public string region_gid { get; set; }
        public string region_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string status { get; set; }
        public string zone2region_gid { get; set; }
        public string api_code { get; set; }

    }

    public class zone : result
    {
        public string zone_gid { get; set; }
        public string zone_name { get; set; }
        public string zone_status { get; set; }
        public string remarks { get; set; }
        public char rbo_status { get; set; }
        public string ocs_pendingcount { get; set; }
        public string agrbyr_pendingcount { get; set; }
        public string agrsupr_pendingcount { get; set; }
        public List<zone_list> zone_list { get; set; }
        public List<region_list> region_list { get; set; }
        public List<zonalhead_list> zonalhead_list { get; set; }
        public List<producthead_list> producthead_list { get; set; }
    }

    public class zone_list
    {
        public string zone_gid { get; set; }
        public string zone_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string status { get; set; }
        public string api_code { get; set; }
    }
    public class mdlvertical : result
    {
        public List<vertical_list> vertical_list { get; set; }
    }
    public class vertical_list
    {
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
    }
    public class mdlemployee : result
    {
        public List<employeelist> employeelist { get; set; }
    }
    public class employeelist
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class mdlregionhead : result
    {
        public string regionhead_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string region_gid { get; set; }
        public string region_name { get; set; }
        public string region_status { get; set; }
        public string remarks { get; set; }
        public string program_gid { get; set; }
        public string program_name { get; set; }
        public char rbo_status { get; set; }
        public string regionhead_status { get; set; }
        public List<employeelist> employeelist { get; set; }
        public List<vertical_list> vertical_list { get; set; }
        public List<region_list> region_list { get; set; }
    }

    public class regionhead_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string status { get; set; }
        public string regionhead_gid { get; set; }
        public string vertical_name { get; set; }
        public string region_name { get; set; }
        public string program_name { get; set; }
        public string api_code { get; set; }

    }
    public class mdlbusinesshead : result
    {
        public string groupbusinesshead_gid { get; set; }
        public string businesshead_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string zone_gid { get; set; }
        public string zone_name { get; set; }
        public string remarks { get; set; }
        public string program_gid { get; set; }
        public string program_name { get; set; }
        public char rbo_status { get; set; }
        public string businesshead_status { get; set; }
        public List<businesshead_list> businesshead_list { get; set; }
        public List<employeelist> employeelist { get; set; }
        public List<vertical_list> vertical_list { get; set; }
        public List<zone_list> zone_list { get; set; }
    }
    public class businesshead_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string status { get; set; }
        public string businesshead_gid { get; set; }
        public string vertical_name { get; set; }
        public string program_gid { get; set; }
        public string program_name { get; set; }
        public string zone_name { get; set; }
        public string groupbusinesshead_gid { get; set; }
        public string api_code { get; set; }
    }
    //Cluster Head codes
    public class clusterhead_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string vertical_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string status { get; set; }
        public string cluster_name { get; set; }
        public string clusterhead_gid { get; set; }
        public string program_name { get; set; }
        public string api_code { get; set; }
    }

    public class mdlclusterhead : result
    {
        public string clusterhead_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string cluster_gid { get; set; }
        public string cluster_name { get; set; }
        public string clusterhead_status { get; set; }
        public string remarks { get; set; }
        public string program_gid { get; set; }
        public string program_name { get; set; }
        public char rbo_status { get; set; }
        public List<cluster_list> cluster_list { get; set; }
        public List<employeelist> employeelist { get; set; }
        public List<vertical_list> vertical_list { get; set; }
    }
    // Zonal Head Codes
    public class mdlzonalhead : result
    {
        public string zonalhead_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string vertical_gid { get; set; }
        public string vertical_name { get; set; }
        public string zonal_gid { get; set; }
        public string zonal_name { get; set; }
        public string zonalhead_status { get; set; }
        public string remarks { get; set; }
        public char rbo_status { get; set; }
        public string program_gid { get; set; }
        public string program_name { get; set; }
        public List<employeelist> employeelist { get; set; }
        public List<vertical_list> vertical_list { get; set; }
        public List<zone_list> zone_list { get; set; }
    }

    public class zonalhead_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string status { get; set; }
        public string zonal_name { get; set; }
        public string zonalhead_gid { get; set; }
        public string vertical_name { get; set; }
        public string program_name { get; set; }
        public string api_code { get; set; }
    }
    // Product Head Codes
    public class mdlproducthead : result
    {
        public string producthead_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string product_gid { get; set; }
        public string product_name { get; set; }
        public string producthead_status { get; set; }
        public string remarks { get; set; }
        public char rbo_status { get; set; }
        public List<employeelist> employeelist { get; set; }
        public List<zone_list> zone_list { get; set; }
    }
    public class producthead_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string status { get; set; }
        public string product_name { get; set; }
        public string producthead_gid { get; set; }
        public string api_code { get; set; }
    }
    //Task
    public class MdlTask : result
    {
        public string task_gid { get; set; }
        public string task_code { get; set; }
        public string task_name { get; set; }
        public string lms_code { get; set; }
        public string bureau_code { get; set; }
        public string task_description { get; set; }
        public string tat { get; set; }
        public string Status { get; set; }
        public string assignedto_name { get; set; }
        public string escalationmailto_name { get; set; }


        public List<taskassigned_to> taskassigned_to { get; set; }
        public List<assignedto_list> assigned_to { get; set; }
        public List<assignedto_list> assignedto_general { get; set; }

        public List<escalationmailto_list> escalationmail_to { get; set; }
        public List<escalationmailto_list> escalationmailto_general { get; set; }

    }

    public class assignedto_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }

    public class escalationmailto_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }


    public class taskassigned_to
    {
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
        public string supportteam2member_gid { get; set; }
    }
    public class menu : result
    {
        public string module_gid { get; set; }
        public string module_name { get; set; }
        public string menu_gid { get; set; }
        public string Status { get; set; }
        public string remarks { get; set; }
        public char rbo_status { get; set; }
        public string module_gid_parent { get; set; }
        public string employee_gid { get; set; }
        public string employeereporting_to { get; set; }
        public List<menu_list> menu_list { get; set; }
        public List<menusummary_list> menusummary_list { get; set; }
        public List<MstUserList> MstUserList { get; set; }
        public List<MstUserList1> MstUserList1 { get; set; }
        public List<MstUserList2> MstUserList2 { get; set; }
        public List<MstUserList3> MstUserList3 { get; set; }
        public List<MstEmployeeList> MstEmployeeList { get; set; }
        public List<MstEmployeeList5> MstEmployeeList5 { get; set; }
    }
    public class MstEmployeeList
    {
        public string user_gid { get; set; }
        public string user_name { get; set; }
        public string employee_gid { get; set; }
    }
    public class MstEmployeeList5
    {
        public string user_gid { get; set; }
        public string user_name { get; set; }
        public string employeereporting_to { get; set; }
        public string employee_gid { get; set; }
    }
    public class MstUserList
    {
        public string user_gid { get; set; }
        public string user_name { get; set; }
    }
    public class MstUserList1
    {
        public string user_gid { get; set; }
        public string user_name { get; set; }
    }
    public class MstUserList2
    {
        public string user_gid { get; set; }
        public string user_name { get; set; }
    }
    public class MstUserList3
    {
        public string user_gid { get; set; }
        public string user_name { get; set; }
    }
    public class menu_list
    {
        public string module_gid { get; set; }
        public string module_name { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class menusummary_list
    {
        public string menu_gid { get; set; }
        public string module_gid { get; set; }
        public string module_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public char rbo_status { get; set; }
        public string api_code { get; set; }
    }

    public class verticalprogram_list
    {
        public List<program_list> program_list { get; set; }
    }

    public class program_list
    {
        public string program_gid { get; set; }
        public string program_name { get; set; }
    }

    //HR Notification
    public class MdlHRNotification : result
    {
        public string hrnotification_gid { get; set; }
        public string hrnotification_code { get; set; }
        public string application_name { get; set; }
        public string notifyto_name { get; set; }
        public string Status { get; set; }
        public List<hrnotificationnotify_to> hrnotificationnotify_to { get; set; }
        public List<notifyto_list> notify_to { get; set; }
        public List<notifyto_list> notifyto_general { get; set; }
    }

    public class notifyto_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class hrnotificationnotify_to
    {
        public string employee_name { get; set; }
        public string employee_gid { get; set; }
        public string supportteam2member_gid { get; set; }
    }
    public class systemasssigned_list
    {

        public string user2oneapi_gid { get; set; }
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
        public string externalsystem_name { get; set; }
        public string externalsystem_ownername { get; set; }

    }
    public class employeeem_list
    {
        public string employee_gid { get; set; }
        public string employee_name { get; set; }
    }
    public class userinactivelog_list
    {

        public string user2oneapi_gid { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public string previous_password { get; set; }
        public string current_password { get; set; }
        public string user_code { get; set; }

    }
    public class userpurpose : result
    {

        public List<userinactivelog_list> userinactivelog_list { get; set; }
        public List<employeeem_list> employeeem_list { get; set; }
        public List<systemasssigned_list> systemasssigned_list { get; set; }
        public List<externalsystem_ownernameList> externalsystem_ownernameList { get; set; }
        public string user2oneapi_gid { get; set; }
        public string externalsystem_name { get; set; }
        public string email_id { get; set; }
        public string externalsystem_ownername { get; set; }
        public string externalsystem_gid { get; set; }
        public string externaluser_code { get; set; }
        public string externalsystem_password { get; set; }
        public string externaluser_status { get; set; }
        public string employee_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public char rbo_status { get; set; }
        public string remarks { get; set; }
        public string web_active { get; set; }
        public string user_active { get; set; }
        public string user_code { get; set; }
        public string user_status { get; set; }
        public string externaluser_password { get; set; }
        public string user_password { get; set; }

    }
    public class externalsystem_ownernameList
    {
        public string employee_name { get; set; }
        public string employee_gid { get; set; }

    }

    public class externaluser_lists : result
    {
        public string user2oneapi_gid { get; set; }
        public string externalsystem_name { get; set; }
        public string externalsystem_ownername { get; set; }
        public string externalsystem_gid { get; set; }
        public string externaluser_code { get; set; }
        public string externaluser_password { get; set; }
        public string externaluser_status { get; set; }
        public string employee_gid { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }
        public string updated_by { get; set; }
        public string updated_date { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
        public string web_active { get; set; }
        public string email_id { get; set; }

    }

    public class MdlFPOCity
    {
        public List<getcity_list> getcity_list { get; set; }

    }
    public class getcity_list
    {
        public string City_Code { get; set; }
        public string City_Gid { get; set; }
        public string City_Name { get; set; }
        public string api_code { get; set; }
    }






}
