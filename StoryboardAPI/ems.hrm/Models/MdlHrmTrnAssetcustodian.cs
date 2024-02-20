
using StoryboardAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hrm.Models
{
    public class MdlHrmTrnAssetcustodian : result
    {
        public List<GetBranch> GetBranch { get; set; }
        public List<custodian_list> custodian_list { get; set; }
        public List<custodian_list1> custodian_list1 { get; set; }
        public List<GetDepartment> GetDepartment { get; set; }
        public List<GetAddCustodian> GetAddCustodian { get; set; }
        public List<Assetcustodian> Assetcustodian { get; set; }
        public List<DownloadFile> DownloadFile { get; set; }
        public List<CompanyPoliies> CompanyPoliies { get; set; }


    }

    public class CompanyPoliies : result
    {
        public string policy_name { get; set; }
        public string policy_desc { get; set; }

    }
    public class DownloadFile : result
    {
        public string document_path { get; set; }
        public string document_name { get; set; }

    }

        public class Assetcustodian : result
    {
        public string asset_gid { get; set; }
        public string asset_id { get; set; }
        public string assetref_no { get; set; }
        public string asset_name { get; set; }
        public string custodian_date { get; set; }
        public string custodian_enddate { get; set; }
        public string remarks { get; set; }
        public string employee_gid { get; set; }
        public string flag { get; set; }
        public string document_gid { get; set; }
        public string document_path { get; set; }

    }



        public class GetAddCustodian : result
    {
        public string asset_gid { get; set; }
        public string asset_id { get; set; }
        public string assetref_no { get; set; }
        public string asset_name { get; set; }
        public string custodian_date { get; set; }
        public string custodian_enddate { get; set; }
        public string remarks { get; set; }
        public string user_code { get; set; }
        public string user_name { get; set; }
        public string document_gid { get; set; }
        public string document_path { get; set; }
        public string employee_gid { get; set; }


    }
    public class custodian_list1 : result
    {
        public string department_name { get; set; }
        public string branch_name { get; set; }

    }


        public class custodian_list : result
    {
        public string user_gid { get; set; }
        public string useraccess { get; set; }
        public string user_code { get; set; }
        public string user_name { get; set; }
        public string employee_joiningdate { get; set; }
        public string employee_gender { get; set; }
        public string emp_address { get; set; }
        public string designation_name { get; set; }
        public string designation_gid { get; set; }
        public string employee_gid { get; set; }
        public string branch_name { get; set; }
        public string user_status { get; set; }
        public string department_gid { get; set; }
        public string branch_gid { get; set; }
        public string department_name { get; set; }

    }

    public class GetBranch : result
    {
        public string branch_name { get; set; }
        public string branch_gid { get; set; }

    }
    public class GetDepartment : result
    {
        public string department_name { get; set; }
        public string department_gid { get; set; }

    }
}