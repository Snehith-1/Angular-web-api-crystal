using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ems.hrm.Models
{
    public class rolelist : role
    {
        public List<role> role { get; set; }
    }
    public class role : result
    {
        public string role_gid { get; set; }
        public string role_code { get; set; }
        public string role_name { get; set; }
        public string reporting_to { get; set; }
        public string reportingto_gid { get; set; }
        public string job_description { get; set; }
        public string role_responsible { get; set; }
        public string probation_period { get; set; }
        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_date { get; set; }
    }
    public class rolereporting_to_list : rolereporting_to
    {
        public List<rolereporting_to> rolereporting_to { get; set; }
    }
    public class rolereporting_to : result
    {
        public string role_gid { get; set; }
        public string role_code { get; set; }
        public string role_name { get; set; }
    }
    public class rolereporting_to_listEdit : rolereporting_toEdit
    {
        public List<rolereporting_toEdit> rolereporting_toEdit { get; set; }
    }
    public class rolereporting_toEdit : result
    {
        public string rolereporting_to_gid { get; set; }
        public string rolereporting_to_name { get; set; }
    }
}
